using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using TMPro;
using TestMulti.Game.SOArchitecture.Variables;
using TestMulti.Game.SOArchitecture.GameEvents;

namespace TestMulti.Game.UI
{
    public class RoomMenuUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Button backButton = null;
        [SerializeField] private Button startButton = null;
        [SerializeField] private PlayersListUI playersListUI = null;

        [Header("Events")]
        [SerializeField] private GameEvent openRoomMenuEvent = null;
        [SerializeField] private GameEvent openLobbyMenuEvent = null;

        [Header("Variables")]
        [SerializeField] private LobbyManagerVariable lobbyManager = null;
        [SerializeField] private SceneManagerVariable sceneManager = null;

        private Lobby lobby;

        private void Awake()
        {
            openRoomMenuEvent.AddListener(OpenRoomMenu);
            backButton.onClick.AddListener(BackToLobby);
            startButton.onClick.AddListener(StartGame);
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            openRoomMenuEvent.RemoveListener(OpenRoomMenu);
            backButton.onClick.RemoveListener(BackToLobby);
            startButton.onClick.RemoveListener(StartGame);
        }

        private void OpenRoomMenu()
        {
            gameObject.SetActive(true);
            lobbyManager.Value.OnLobbyUpdated += UpdateRoomView;
            sceneManager.Value.OnSceneLoadCompleted += () => gameObject.SetActive(false);
        }

        private async void BackToLobby()
        {
            lobbyManager.Value.OnLobbyUpdated -= UpdateRoomView;
            await lobbyManager.Value.LeaveLobby();
            openLobbyMenuEvent.Raise();
            gameObject.SetActive(false);
        }

        private void StartGame()
        {
            lobbyManager.Value.StartGame();
        }

        private void UpdateRoomView(Lobby newLobby)
        {
            lobby = newLobby;
            if (lobby == null)
                BackToLobby();

            if (lobby.HostId == AuthenticationService.Instance.PlayerId) startButton.interactable = true;
            else startButton.interactable = false;

            playersListUI.ResetPlayers();

            foreach (var player in lobby.Players)
                playersListUI.AddPlayer(player.Data["Username"].Value);
        }
    }
}