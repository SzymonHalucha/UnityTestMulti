using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TestMulti.Game.Player;
using TestMulti.Game.Effects.Action;

namespace TestMulti.Game.Effects
{
    [CreateAssetMenu(menuName = "Test Multi/Effects/Effect", fileName = "New Effect")]
    public class Effect : ScriptableObject
    {
        [SerializeReference] private List<BaseEffectAction> actions = new();
        [SerializeField] private bool useAnimationCurve = false;
        [SerializeField, HideIf("useAnimationCurve"), Range(0, 8f)] private float tickInterval = 0.1f;
        [SerializeField, ShowIf("useAnimationCurve")] private AnimationCurve tickCurve = AnimationCurve.Linear(0, 0, 4f, 1f);
        [SerializeField, Range(0, 64)] private int tickAmount = 4;

        public int TickAmount => tickAmount;

        public bool Begin(PlayerContainer player)
        {
            bool done = true;
            foreach (BaseEffectAction action in actions)
                done &= action.OnBegin(player);

            return done;
        }

        public bool Tick(PlayerContainer player)
        {
            bool done = true;
            foreach (BaseEffectAction action in actions)
                done &= action.OnTick(player);

            return done;
        }

        public bool End(PlayerContainer player)
        {
            bool done = true;
            foreach (BaseEffectAction action in actions)
                done &= action.OnEnd(player);

            return done;
        }

        public float GetTickInterval(int elapsedTicks)
        {
            if (useAnimationCurve)
                return tickCurve.Evaluate(Mathf.Clamp(elapsedTicks, 0, tickAmount));

            return tickInterval;
        }
    }
}