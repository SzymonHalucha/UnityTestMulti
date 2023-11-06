using UnityEngine;
using UnityEngine.InputSystem;
using TestMulti.Game.Stats;

namespace TestMulti.Game.Player.StateMachine
{
    [CreateAssetMenu(menuName = "Test Multi/State Machine/Normal Player State", fileName = "New Normal Player State")]
    public class NormalPlayerState : BasePlayerState
    {
        [SerializeField] private StatType speed = null;

        private PlayerContainer player;

        public override void OnEnter(PlayerContainer player)
        {
            this.player = player;
            this.player.Inputs.Jump.performed += Jump;
        }

        public override void OnExit(PlayerContainer player)
        {
            player.Inputs.Jump.performed -= Jump;
        }

        public override void OnFixedUpdate(PlayerContainer player)
        {
            if (player.Inputs.Left.IsPressed())
                player.Rigidbody2D.AddForce(Vector2.left * player.Stats.GetConstStat(speed).Value * Time.fixedDeltaTime);

            if (player.Inputs.Right.IsPressed())
                player.Rigidbody2D.AddForce(Vector2.right * player.Stats.GetConstStat(speed).Value * Time.fixedDeltaTime);
        }

        private void Jump(InputAction.CallbackContext context)
        {
            player.StateMachine.ChangeState(typeof(JumpPlayerState));
        }
    }
}