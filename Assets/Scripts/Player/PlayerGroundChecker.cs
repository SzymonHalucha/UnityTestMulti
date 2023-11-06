using UnityEngine;
using Sirenix.OdinInspector;

namespace TestMulti.Game.Player
{
    public class PlayerGroundChecker : PlayerBaseComponent
    {
        [field: ShowInInspector, ReadOnly] public bool IsGrounded { get; private set; } = false;

        public event System.Action OnGroundEnter;
        public event System.Action OnGroundExit;

        protected override void OnSpawnOwner()
        {
            this.gameObject.SetActive(true);
        }

        protected override void OnDespawnOwner()
        {
            this.gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            IsGrounded = true;
            OnGroundEnter?.Invoke();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            IsGrounded = false;
            OnGroundExit?.Invoke();
        }
    }
}