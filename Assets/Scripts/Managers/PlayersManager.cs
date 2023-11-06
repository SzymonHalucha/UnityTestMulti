using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TestMulti.Game.Player;
using TestMulti.Game.Teams;
using TestMulti.Game.Spawn;
using TestMulti.Game.SOArchitecture.Variables;

namespace TestMulti.Game.Managers
{
    public class PlayersManager : BaseManager
    {
        [SerializeField, Header("Settings")] private GameObject playerPrefab = null;
        [SerializeField] private SceneManagerVariable sceneManager = null;
        [SerializeField] private ServicesManagerVariable servicesManager = null;
        [SerializeField, Header("Teams")] private TeamType[] teams = new TeamType[0];
        [SerializeField, Header("Spawn Points")] private SpawnPoint[] spawnPoints = new SpawnPoint[0];

        private int[] playersPerTeams;
        private Dictionary<ulong, TeamType> players;
        private HashSet<SpawnPoint> usedSpawnPoints;

        public TeamType[] Teams => teams;
        public SpawnPoint[] SpawnPoints => spawnPoints;

        public override void Init()
        {
            sceneManager.Value.OnSceneLoadCompleted += OnSceneLoaded;
        }

        public override void OnSpawn()
        {
            if (NetworkManager.Singleton.IsServer)
            {
                playersPerTeams = new int[teams.Length];
                players = new Dictionary<ulong, TeamType>();
                usedSpawnPoints = new HashSet<SpawnPoint>();
                NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;
            }
        }

        public override void OnDespawn()
        {
            if (!NetworkManager.Singleton.IsServer)
                return;

            NetworkManager.Singleton.OnClientDisconnectCallback -= OnClientDisconnected;

            players = null;
            playersPerTeams = null;
            usedSpawnPoints = null;
        }

        public int CreatePlayerTeamIndexForClient(ulong clientId)
        {
            int teamIndex = Array.IndexOf(playersPerTeams, playersPerTeams.Min());
            playersPerTeams[teamIndex]++;
            players.Add(clientId, teams[teamIndex]);
            return teamIndex;
        }

        public int GetExistingPlayerTeamIndexForClient(ulong clientId)
        {
            return Array.IndexOf(teams, players[clientId]);
        }

        public SpawnPoint GetUniqueRandomSpawnPointForTeam(int index)
        {
            TeamType team = teams[index];
            SpawnPoint[] spawnPointsForTeam = GetSpawnPointsForTeam(team);
            SpawnPoint spawnPoint = spawnPointsForTeam[UnityEngine.Random.Range(0, spawnPointsForTeam.Length)];

            int counter = 0;
            int maxAttempts = 16;
            while (usedSpawnPoints.Contains(spawnPoint) && counter++ <= maxAttempts)
                spawnPoint = spawnPointsForTeam[UnityEngine.Random.Range(0, spawnPointsForTeam.Length)];

            usedSpawnPoints.Add(spawnPoint);
            return spawnPoint;
        }

        private SpawnPoint[] GetSpawnPointsForTeam(TeamType team)
        {
            return spawnPoints.Where(spawnPoint => spawnPoint.Team == team).ToArray();
        }

        private void OnClientDisconnected(ulong clientId)
        {
            if (NetworkManager.ConnectedClients.TryGetValue(clientId, out NetworkClient client))
                client.PlayerObject?.Despawn(true);

            int teamIndex = Array.IndexOf(teams, players[clientId]);
            playersPerTeams[teamIndex]--;
            players.Remove(clientId);
        }

        private void OnSceneLoaded()
        {
            sceneManager.Value.OnSceneLoadCompleted -= OnSceneLoaded;

            if (NetworkManager.Singleton.IsClient)
                GetPlayerForClientServerRpc(servicesManager.Value.Username);
        }

        [ServerRpc(RequireOwnership = false)]
        private void GetPlayerForClientServerRpc(string username, ServerRpcParams serverRpcParams = default)
        {
            GameObject player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            PlayerContainer container = player.GetComponent<PlayerContainer>();
            container.Team.Index.Value = CreatePlayerTeamIndexForClient(serverRpcParams.Receive.SenderClientId);
            container.Team.SpawnPoint.Value = GetUniqueRandomSpawnPointForTeam(container.Team.Index.Value).Position;
            container.Team.Username.Value = username;
            container.NetworkObject.SpawnAsPlayerObject(serverRpcParams.Receive.SenderClientId);
        }
    }
}