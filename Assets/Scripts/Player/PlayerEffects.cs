using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TestMulti.Game.Effects;

namespace TestMulti.Game.Player
{
    public class PlayerEffects : PlayerBaseComponent
    {
        [SerializeField, Header("Effects")] private List<EffectInstance> effectInstances = new();

        protected override void OnSpawnOwner()
        {

        }

        protected override void OnDespawnOwner()
        {

        }

        private void Update()
        {
            for (int i = 0; i < effectInstances.Count; i++)
            {
                effectInstances[i].Tick(Player);
                if (effectInstances[i].IsFinished)
                    effectInstances.Remove(effectInstances[i]);
            }
        }

        public void AddEffect(Effect effect)
        {
            if (!effectInstances.Any(e => e.Effect == effect))
                effectInstances.Add(new EffectInstance(effect));
            else
                effectInstances.First(e => e.Effect == effect).AddElapsedTicks(effect.TickAmount);
        }

        public void RemoveEffect(Effect effect)
        {
            if (effectInstances.Any(e => e.Effect == effect))
                effectInstances.First(e => e.Effect == effect).SetElapsedTicks(0);
        }
    }
}