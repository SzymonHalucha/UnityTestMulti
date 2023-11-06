using UnityEngine;
using TestMulti.Game.Player;
using Sirenix.OdinInspector;

namespace TestMulti.Game.Effects
{
    [System.Serializable]
    public class EffectInstance
    {
        [field: SerializeField] public Effect Effect { get; private set; }
        [field: SerializeField] public int ElapsedTicks { get; private set; }

        [ShowInInspector, ReadOnly] private float timer = 0;
        [ShowInInspector, ReadOnly] private bool begined = false;
        [ShowInInspector, ReadOnly] private bool ended = false;

        public bool IsFinished => begined && ended;

        public EffectInstance(Effect effect)
        {
            this.Effect = effect;
            this.ElapsedTicks = effect.TickAmount;
            this.timer = effect.GetTickInterval(ElapsedTicks);
        }

        public void Tick(PlayerContainer player)
        {
            if (!begined)
                begined = Effect.Begin(player);
            else
                timer -= Time.deltaTime;

            if (timer <= 0 && Effect.Tick(player))
            {
                timer = Effect.GetTickInterval(ElapsedTicks);
                ElapsedTicks--;
            }

            if (ElapsedTicks <= 0 && !ended)
                ended = Effect.End(player);
        }

        public void AddElapsedTicks(int ticks)
        {
            ElapsedTicks += ticks;
        }

        public void SubtractElapsedTicks(int ticks)
        {
            ElapsedTicks -= ticks;
        }

        public void SetElapsedTicks(int ticks)
        {
            ElapsedTicks = ticks;
        }
    }
}