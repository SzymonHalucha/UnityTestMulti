using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.Services.Lobbies.Models;

namespace TestMulti.Game.UI
{
    public class LobbiesListUI : MonoBehaviour
    {
        [SerializeField] private Transform content = null;
        [SerializeField] private SingleLobbyUI singleLobbyPrefab = null;

        private List<SingleLobbyUI> activeLobbyUIs = new List<SingleLobbyUI>();
        private List<SingleLobbyUI> inactiveLobbyUIs = new List<SingleLobbyUI>();

        public void ResetLobbies()
        {
            foreach (SingleLobbyUI lobbyUI in activeLobbyUIs)
            {
                lobbyUI.gameObject.SetActive(false);
                inactiveLobbyUIs.Add(lobbyUI);
            }

            activeLobbyUIs.Clear();
        }

        public void SetLobbies(List<Lobby> lobbies)
        {
            RefreshUIList(lobbies);
            foreach (Lobby lobby in lobbies)
            {
                if (activeLobbyUIs.Any(x => x.LobbyId == lobby.Id))
                    continue;

                CreateLobbyUI(lobby.Id, lobby.Name, lobby.Players.Count, lobby.MaxPlayers);
            }
        }

        private void RefreshUIList(List<Lobby> lobbies)
        {
            for (int i = 0; i < activeLobbyUIs.Count; i++)
            {
                if (lobbies.Any(x => x.Id != activeLobbyUIs[i].LobbyId))
                {
                    activeLobbyUIs[i].gameObject.SetActive(false);
                    inactiveLobbyUIs.Add(activeLobbyUIs[i]);
                    activeLobbyUIs.Remove(activeLobbyUIs[i]);
                    i--;
                }
            }
        }

        private void CreateLobbyUI(string lobbyId, string lobbyName, int currentPlayers, int maxPlayers)
        {
            if (inactiveLobbyUIs.Count > 0)
            {
                SingleLobbyUI lobbyUI = inactiveLobbyUIs.First();
                lobbyUI.gameObject.SetActive(true);
                lobbyUI.SetLobbyId(lobbyId);
                lobbyUI.SetLobbyName(lobbyName);
                lobbyUI.SetLobbyPlayers(currentPlayers, maxPlayers);
                inactiveLobbyUIs.Remove(lobbyUI);
                activeLobbyUIs.Add(lobbyUI);
            }
            else
            {
                SingleLobbyUI newLobbyUI = Instantiate(singleLobbyPrefab, content);
                newLobbyUI.SetLobbyId(lobbyId);
                newLobbyUI.SetLobbyName(lobbyName);
                newLobbyUI.SetLobbyPlayers(currentPlayers, maxPlayers);
                activeLobbyUIs.Add(newLobbyUI);
            }
        }
    }
}