using UnityEngine;
using UnityEngine.Events;
using Unity.Netcode;
using TestMulti.Game.Player.StateMachine;

namespace TestMulti.Game.Player
{
    public class PlayerContainer : NetworkBehaviour
    {
        [SerializeField] private UnityEvent onSpawn = new();
        [SerializeField] private UnityEvent onDespawn = new();

        [field: SerializeField] public Transform Transform { get; private set; } = null;
        [field: SerializeField] public BoxCollider2D BoxCollider2D { get; private set; } = null;
        [field: SerializeField] public Rigidbody2D Rigidbody2D { get; private set; } = null;
        [field: SerializeField] public PlayerStateMachine StateMachine { get; private set; } = null;
        [field: SerializeField] public PlayerAnimations Animations { get; private set; } = null;
        [field: SerializeField] public PlayerArt Art { get; private set; } = null;
        [field: SerializeField] public PlayerEffects Effects { get; private set; } = null;
        [field: SerializeField] public PlayerGroundChecker GroundChecker { get; private set; } = null;
        [field: SerializeField] public PlayerHealth Health { get; private set; } = null;
        [field: SerializeField] public PlayerInputs Inputs { get; private set; } = null;
        [field: SerializeField] public PlayerInventory Inventory { get; private set; } = null;
        [field: SerializeField] public PlayerSounds Sounds { get; private set; } = null;
        [field: SerializeField] public PlayerStats Stats { get; private set; } = null;
        [field: SerializeField] public PlayerTeam Team { get; private set; } = null;
        [field: SerializeField] public PlayerNetwork Network { get; private set; } = null;

        public override void OnNetworkSpawn()
        {
            if (!IsOwner)
                return;

            onSpawn?.Invoke();
        }

        public override void OnNetworkDespawn()
        {
            if (!IsOwner)
                return;

            onDespawn?.Invoke();
        }
    }
}