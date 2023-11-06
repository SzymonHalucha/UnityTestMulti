using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using TestMulti.Game.SOArchitecture.Variables;

namespace TestMulti.Game.Managers
{
    public class LobbyManager : BaseManager
    {
        [Header("Variables")]
        [SerializeField] private ServicesManagerVariable servicesManager = null;
        [SerializeField] private SceneManagerVariable sceneManager = null;
        [SerializeField] private RelayManagerVariable relayManager = null;

        [Header("Settings")]
        [SerializeField, Range(8f, 32f)] private float heartbeatInterval = 15f;
        [SerializeField, Range(1f, 16f)] private float lobbySyncInterval = 1.5f;

        private const string UsernameKey = "Username";
        private const string GameStatusKey = "GameStatus";
        private const string JoinCodeKey = "JoinCode";
        private const string GameStartedValue = "Started";
        private const string GameNotStartedValue = "NotStarted";

        private Lobby lobby;
        private float lobbyHeartbeatTimer;
        private float lobbySyncTimer;

        public event System.Action<Lobby> OnLobbyUpdated;

        public async Task CreateLobby(string lobbyName, string maxPlayers)
        {
            if (!CheckIfInputsAreValid(lobbyName, maxPlayers))
                return;

            try
            {
                int players = int.Parse(maxPlayers);
                CreateLobbyOptions options = new CreateLobbyOptions
                {
                    Data = GetLobbyDataObject("0", GameNotStartedValue),
                    Player = new Unity.Services.Lobbies.Models.Player
                    {
                        Data = GetPlayerDataObject(servicesManager.Value.Username)
                    }
                };
                lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, players, options);
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
            }

            OnLobbyUpdated?.Invoke(lobby);
        }

        public async Task DeleteLobby()
        {
            try
            {
                await LobbyService.Instance.DeleteLobbyAsync(lobby.Id);
                lobby = null;
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
            }

            OnLobbyUpdated?.Invoke(lobby);
        }

        public async Task JoinLobby(string lobbyId)
        {
            try
            {
                JoinLobbyByIdOptions options = new JoinLobbyByIdOptions
                {
                    Player = new Unity.Services.Lobbies.Models.Player
                    {
                        Data = GetPlayerDataObject(servicesManager.Value.Username)
                    }
                };
                lobby = await Lobbies.Instance.JoinLobbyByIdAsync(lobbyId, options);
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
            }

            OnLobbyUpdated?.Invoke(lobby);
        }

        public async Task LeaveLobby()
        {
            try
            {
                await LobbyService.Instance.RemovePlayerAsync(lobby.Id, AuthenticationService.Instance.PlayerId);
                lobby = null;
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
            }

            OnLobbyUpdated?.Invoke(lobby);
        }

        public async void StartGame()
        {
            if (lobby == null || lobby.HostId != AuthenticationService.Instance.PlayerId)
                return;

            string joinCode = await relayManager.Value.CreateRelay(lobby.MaxPlayers - 1);
            lobby = await Lobbies.Instance.UpdateLobbyAsync(lobby.Id, new UpdateLobbyOptions { Data = GetLobbyDataObject(joinCode, GameStartedValue) });
            await LobbyService.Instance.UpdatePlayerAsync(
                lobby.Id,
                AuthenticationService.Instance.PlayerId,
                new UpdatePlayerOptions { Data = GetPlayerDataObject(servicesManager.Value.Username) }
            );

            sceneManager.Value.LoadSceneFromMenu();
        }

        public async Task<List<Lobby>> SearchForLobbies()
        {
            QueryResponse response = await LobbyService.Instance.QueryLobbiesAsync();
            return response.Results;
        }

        private void Update()
        {
            if (lobby == null)
                return;

            if (lobby.HostId == AuthenticationService.Instance.PlayerId && (lobbyHeartbeatTimer += Time.deltaTime) >= heartbeatInterval)
            {
                lobbyHeartbeatTimer = 0f;
                SendHeartbeatPing();
            }

            if (!relayManager.Value.IsInGame && (lobbySyncTimer += Time.deltaTime) >= lobbySyncInterval)
            {
                lobbySyncTimer = 0f;
                SyncLobby();
            }
        }

        private async void SendHeartbeatPing()
        {
            await LobbyService.Instance.SendHeartbeatPingAsync(lobby.Id);
        }

        private async void SyncLobby()
        {
            try
            {
                lobby = await LobbyService.Instance.GetLobbyAsync(lobby.Id);
                JoinGame();
            }
            catch (System.Exception e)
            {
                lobby = null;
                Debug.Log(e);
            }

            OnLobbyUpdated?.Invoke(lobby);
        }

        private async void JoinGame()
        {
            if (lobby.HostId == AuthenticationService.Instance.PlayerId)
                return;

            try
            {
                if (lobby.Data[JoinCodeKey].Value != "0")
                {
                    await relayManager.Value.JoinRelay(lobby.Data[JoinCodeKey].Value);
                    await LobbyService.Instance.UpdatePlayerAsync(
                        lobby.Id,
                        AuthenticationService.Instance.PlayerId,
                        new UpdatePlayerOptions { Data = GetPlayerDataObject(servicesManager.Value.Username) }
                    );
                }
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
            }
        }

        private bool CheckIfInputsAreValid(string lobbyName, string maxPlayers)
        {
            if (string.IsNullOrEmpty(lobbyName) || string.IsNullOrWhiteSpace(lobbyName))
                return false;

            if (!int.TryParse(maxPlayers, out int maxPlayersInt))
                return false;

            if (maxPlayersInt < 2)
                return false;

            return true;
        }

        private Dictionary<string, PlayerDataObject> GetPlayerDataObject(string username)
        {
            Dictionary<string, PlayerDataObject> player = new Dictionary<string, PlayerDataObject>
            {
                {UsernameKey, new PlayerDataObject(PlayerDataObject.VisibilityOptions.Public, username)},
            };

            return player;
        }

        private Dictionary<string, DataObject> GetLobbyDataObject(string joinCode, string gameStatus)
        {
            Dictionary<string, DataObject> dataObject = new Dictionary<string, DataObject>
            {
                {GameStatusKey, new DataObject(DataObject.VisibilityOptions.Public, gameStatus)},
                {JoinCodeKey, new DataObject(DataObject.VisibilityOptions.Member, joinCode)}
            };

            return dataObject;
        }
    }
}