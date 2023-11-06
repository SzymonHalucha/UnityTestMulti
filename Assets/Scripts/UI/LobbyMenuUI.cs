using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Lobbies.Models;
using TestMulti.Game.SOArchitecture.Variables;
using TestMulti.Game.SOArchitecture.GameEvents;

namespace TestMulti.Game.UI
{
    public class LobbyMenuUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Button searchButton = null;
        [SerializeField] private Button hostButton = null;
        [SerializeField] private Button joinButton = null;
        [SerializeField] private Button backButton = null;
        [SerializeField] private LobbiesListUI lobbiesList = null;

        [Header("Events")]
        [SerializeField] private GameEvent openLobbyMenuEvent = null;
        [SerializeField] private GameEvent openMainMenuEvent = null;
        [SerializeField] private GameEvent openHostMenuEvent = null;
        [SerializeField] private GameEvent openRoomMenuEvent = null;
        [SerializeField] private StringEvent onRoomSelectEvent = null;

        [Header("Variables")]
        [SerializeField] private LobbyManagerVariable lobbyManager = null;
        [SerializeField] private RelayManagerVariable relayManager = null;

        private string selectedLobbyId;

        private void Awake()
        {
            onRoomSelectEvent.AddListener(OnRoomSelected);
            searchButton.onClick.AddListener(SearchForLobbies);
            hostButton.onClick.AddListener(HostGame);
            joinButton.onClick.AddListener(JoinGame);
            backButton.onClick.AddListener(OpenMainMenu);
            openLobbyMenuEvent.AddListener(OpenLobbyMenu);
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            onRoomSelectEvent.RemoveListener(OnRoomSelected);
            searchButton.onClick.RemoveListener(SearchForLobbies);
            hostButton.onClick.RemoveListener(HostGame);
            joinButton.onClick.RemoveListener(JoinGame);
            backButton.onClick.RemoveListener(OpenMainMenu);
            openLobbyMenuEvent.RemoveListener(OpenLobbyMenu);
        }

        private async void SearchForLobbies()
        {
            List<Lobby> lobbies = await lobbyManager.Value.SearchForLobbies();
            lobbiesList.SetLobbies(lobbies);
        }

        private void HostGame()
        {
            openHostMenuEvent.Raise();
            gameObject.SetActive(false);
        }

        private async void JoinGame()
        {
            await lobbyManager.Value.JoinLobby(selectedLobbyId);
            openRoomMenuEvent.Raise();
            gameObject.SetActive(false);
        }

        private void OpenLobbyMenu()
        {
            lobbiesList.ResetLobbies();
            gameObject.SetActive(true);
        }

        private void OpenMainMenu()
        {
            openMainMenuEvent.Raise();
            gameObject.SetActive(false);
        }

        private void OnRoomSelected(string lobbyId)
        {
            selectedLobbyId = lobbyId;
        }
    }
}