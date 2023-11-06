using UnityEngine;
using Sirenix.OdinInspector;

namespace TestMulti.Game.Stats
{
    [System.Serializable]
    public class DynamicStat
    {
        [SerializeField] private StatType type = null;
        [SerializeField] private float startValue = 100f;
        [SerializeField] private float currentValue = 100f;
        [SerializeField] private float minValue = 0;
        [SerializeField] private float maxValue = 100f;
        [ShowInInspector, ReadOnly] private float modifierValue = 0;

        public StatType Type => type;
        public float StartValue => startValue;
        public float CurrentValue => currentValue + modifierValue;
        public float MinValue => minValue;
        public float MaxValue => maxValue;

        public event System.Action<float> OnValueChanged;

        public void Subtract(float value)
        {
            currentValue = Mathf.Max(currentValue - value, MinValue);
            OnValueChanged?.Invoke(CurrentValue);
        }

        public void Add(float value)
        {
            currentValue = Mathf.Min(currentValue + value, MaxValue);
            OnValueChanged?.Invoke(CurrentValue);
        }

        public void Set(float value)
        {
            currentValue = Mathf.Clamp(value, MinValue, MaxValue);
            OnValueChanged?.Invoke(CurrentValue);
        }

        public void SetModifier(float value)
        {
            modifierValue = value;
            OnValueChanged?.Invoke(CurrentValue);
        }

        public void SetModifierPercentage(float value)
        {
            modifierValue = (value) * (MaxValue - MinValue);
            OnValueChanged?.Invoke(CurrentValue);
        }

        public void Reset()
        {
            currentValue = startValue;
            modifierValue = 0;
            OnValueChanged?.Invoke(CurrentValue);
        }
    }
}