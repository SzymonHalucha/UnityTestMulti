using UnityEngine;
using TestMulti.Game.StateMachine;

namespace TestMulti.Game.Player.StateMachine
{
    public class PlayerStateMachine : BaseStateMachine<PlayerContainer>
    {
        [SerializeField] private PlayerContainer playerContainer = null;
        [SerializeField] private BasePlayerState[] states = new BasePlayerState[0];

        public override void OnNetworkSpawn()
        {
            if (!IsOwner)
                return;

            Init(playerContainer, states);
            ChangeState(typeof(NormalPlayerState));
        }

        public override void OnNetworkDespawn()
        {
            if (!IsOwner)
                return;

            DeInit();
        }
    }
}