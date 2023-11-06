using UnityEngine;
using Unity.Netcode;
using Unity.Collections;
using TestMulti.Game.SOArchitecture.Variables;
using TestMulti.Game.Network.PlayerComponents;
using TestMulti.Game.Teams;
using TestMulti.Game.Spawn;

namespace TestMulti.Game.Player
{
    public class PlayerTeam : PlayerBaseComponent
    {
        [SerializeField] private PlayersManagerVariable playersManager = null;
        [SerializeField] private TeamType teamType = null;

        private NetworkVariable<int> index = new NetworkVariable<int>();
        private NetworkVariable<Vector2> spawnPoint = new NetworkVariable<Vector2>();
        private NetworkVariable<FixedString128Bytes> username = new NetworkVariable<FixedString128Bytes>();

        public NetworkVariable<int> Index => index;
        public NetworkVariable<Vector2> SpawnPoint => spawnPoint;
        public NetworkVariable<FixedString128Bytes> Username => username;
        public TeamType TeamType => teamType;

        protected override void OnSpawnOwner()
        {
            SetTeam(Index.Value);
            SetPlayerOnSpawnPoint(SpawnPoint.Value);
        }

        protected override void OnSpawnOther()
        {
            SetTeam(Index.Value);
        }

        protected override void OnDespawnOwner() { }
        protected override void OnDespawnOther() { }

        private void SetTeam(int teamIndex)
        {
            teamType = playersManager.Value.Teams[teamIndex];
            Player.Art.SpriteRenderer.color = teamType.Color;
        }

        private void SetPlayerOnSpawnPoint(Vector2 position)
        {
            Player.Transform.position = position;
        }
    }
}