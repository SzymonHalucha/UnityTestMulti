using UnityEngine;
using TestMulti.Game.Stats;

namespace TestMulti.Game.Player.StateMachine
{
    [CreateAssetMenu(menuName = "Test Multi/State Machine/Jump Player State", fileName = "New Jump Player State")]
    public class JumpPlayerState : BasePlayerState
    {
        [SerializeField] private StatType speedStat = null;
        [SerializeField] private StatType jumpForceStat = null;

        private PlayerContainer player;

        public override void OnEnter(PlayerContainer player)
        {
            this.player = player;
            player.Rigidbody2D.AddForce(Vector2.up * player.Stats.GetConstStat(jumpForceStat).Value, ForceMode2D.Impulse);
            player.GroundChecker.OnGroundEnter += ChangeStateToNormal;
        }

        public override void OnExit(PlayerContainer player)
        {
            player.GroundChecker.OnGroundEnter -= ChangeStateToNormal;
        }

        public override void OnFixedUpdate(PlayerContainer player)
        {
            if (player.Inputs.Left.IsPressed())
                player.Rigidbody2D.AddForce(Vector2.left * player.Stats.GetConstStat(speedStat).Value * Time.fixedDeltaTime);

            if (player.Inputs.Right.IsPressed())
                player.Rigidbody2D.AddForce(Vector2.right * player.Stats.GetConstStat(speedStat).Value * Time.fixedDeltaTime);
        }

        private void ChangeStateToNormal()
        {
            player.StateMachine.ChangeState(typeof(NormalPlayerState));
        }
    }
}