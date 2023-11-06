using UnityEngine;
using Unity.Netcode;

namespace TestMulti.Game.Player
{
    public abstract class PlayerBaseComponent : NetworkBehaviour
    {
        [SerializeField] protected PlayerContainer Player = null;

        protected abstract void OnSpawnOwner();
        protected abstract void OnDespawnOwner();
        protected virtual void OnSpawnOther() { }
        protected virtual void OnDespawnOther() { }

        public override void OnNetworkSpawn()
        {
            if (IsOwner)
                OnSpawnOwner();
            else
                OnSpawnOther();
        }

        public override void OnNetworkDespawn()
        {
            if (IsOwner)
                OnDespawnOwner();
            else
                OnDespawnOther();
        }
    }
}