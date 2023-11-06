using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Lobbies.Models;
using TMPro;
using TestMulti.Game.SOArchitecture.Variables;
using TestMulti.Game.SOArchitecture.GameEvents;

namespace TestMulti.Game.UI
{
    public class HostMenuUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TMP_InputField roomNameInput = null;
        [SerializeField] private TMP_InputField maxPlayersInput = null;
        [SerializeField] private Button createRoomButton = null;
        [SerializeField] private Button backButton = null;

        [Header("Events")]
        [SerializeField] private GameEvent openHostMenuEvent = null;
        [SerializeField] private GameEvent openLobbyMenuEvent = null;
        [SerializeField] private GameEvent openRoomMenuEvent = null;

        [Header("Variables")]
        [SerializeField] private LobbyManagerVariable lobbyManager = null;

        private void Awake()
        {
            createRoomButton.onClick.AddListener(CreateRoom);
            backButton.onClick.AddListener(OpenLobbyMenu);
            openHostMenuEvent.AddListener(OpenHostMenu);
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            createRoomButton.onClick.RemoveListener(CreateRoom);
            backButton.onClick.RemoveListener(OpenLobbyMenu);
            openHostMenuEvent.RemoveListener(OpenHostMenu);
        }

        private async void CreateRoom()
        {
            await lobbyManager.Value.CreateLobby(roomNameInput.text, maxPlayersInput.text);
            openRoomMenuEvent.Raise();
            gameObject.SetActive(false);
        }

        private void OpenLobbyMenu()
        {
            openLobbyMenuEvent.Raise();
            gameObject.SetActive(false);
        }

        private void OpenHostMenu()
        {
            gameObject.SetActive(true);
        }
    }
}