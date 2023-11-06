using UnityEngine;

namespace TestMulti.Game.Player
{
    public class PlayerArt : PlayerBaseComponent
    {
        [field: SerializeField] public Transform Transform { get; private set; } = null;
        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; } = null;

        protected override void OnSpawnOwner()
        {

        }

        protected override void OnDespawnOwner()
        {

        }
    }
}