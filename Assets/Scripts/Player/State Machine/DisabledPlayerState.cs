using UnityEngine;

namespace TestMulti.Game.Player.StateMachine
{
    [CreateAssetMenu(menuName = "Test Multi/State Machine/Disabled Player State", fileName = "New Disabled Player State")]
    public class DisabledPlayerState : BasePlayerState
    {
        public override void OnEnter(PlayerContainer player)
        {
            player.Inputs.DisableControls();
        }

        public override void OnExit(PlayerContainer player)
        {
            player.Inputs.EnableControls();
        }
    }
}