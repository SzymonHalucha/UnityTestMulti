using UnityEngine;
using Sirenix.OdinInspector;

namespace TestMulti.Game.Stats
{
    [System.Serializable]
    public class ConstStat
    {
        [SerializeField] private StatType type = null;
        [SerializeField] private float value = 100f;
        [ShowInInspector, ReadOnly] private float modifierValue = 0;

        public StatType Type => type;
        public float Value => value + modifierValue;

        public event System.Action<float> OnValueChanged;

        public void SetModifier(float value)
        {
            modifierValue = value;
            OnValueChanged?.Invoke(Value);
        }

        public void Reset()
        {
            modifierValue = 0;
            OnValueChanged?.Invoke(Value);
        }
    }
}