using System;
using UltEvents;
using UnityEngine;

public class ConditionCaller : MonoBehaviour
{
    [Serializable]
    private abstract class Condition
    {
        [SerializeField] private UltEvent onTrue = new UltEvent();
        [SerializeField] private bool breakIfTrue = true;

        protected abstract bool CheckCondition();
        public bool CallEvent()
        {
            if (CheckCondition())
            {
                onTrue.Invoke();
                return breakIfTrue;
            }

            return false;
        }
    }

    private class BoolCondition : Condition
    {
        [SerializeReference] private BoolReference boolValue = new BoolReference();
        [SerializeReference] private BoolReference toCompare = new BoolReference();

        protected override bool CheckCondition() => boolValue.Value == toCompare.Value;
    }

    private class IntCondition : Condition
    {
        private enum Function { Less, LessOrEqual, Equal, MoreOrEqual, More }

        [SerializeReference] private IntReference intValue = new IntReference();
        [SerializeField] private Function compareMethod = Function.Equal;
        [SerializeReference] private IntReference toCompare = new IntReference();

        private readonly Func<int, int, bool>[] compareMethods =
        {
            (int i, int j) => i < j,
            (int i, int j) => i <= j,
            (int i, int j) => i == j,
            (int i, int j) => i >= j,
            (int i, int j) => i > j
        };

        protected override bool CheckCondition() => compareMethods[(int)compareMethod].Invoke(intValue.Value, toCompare.Value);
    }

    private class FloatCondition : Condition
    {
        private enum Function { Less, LessOrEqual, MoreOrEqual, More }

        [SerializeReference] private FloatReference floatValue = new FloatReference();
        [SerializeField] private Function compareMethod = Function.Less;
        [SerializeReference] private FloatReference toCompare = new FloatReference();

        private readonly Func<float, float, bool>[] compareMethods =
        {
            (float i, float j) => i < j,
            (float i, float j) => i <= j,
            (float i, float j) => i >= j,
            (float i, float j) => i > j
        };

        protected override bool CheckCondition() => compareMethods[(int)compareMethod].Invoke(floatValue.Value, toCompare.Value);
    }

    private class NullCondition : Condition
    {
        [SerializeField] private UnityObjectInterfaceReference<INullableValue> variable = new UnityObjectInterfaceReference<INullableValue>();
        [SerializeReference] private BoolReference toCompare = new BoolReference();

        protected override bool CheckCondition() => variable.Value.IsNull() == toCompare.Value;
    }

    private class CollectionSizeCondition : Condition
    {
        [SerializeReference] private UnityObjectInterfaceReference<ICollectionCount> collection = new UnityObjectInterfaceReference<ICollectionCount>();
        [SerializeReference] private IntReference toCompare = new IntReference();

        protected override bool CheckCondition() => collection.Value.Count == toCompare.Value;
    }

    [SerializeReference, SelectType, Sirenix.OdinInspector.DrawWithUnity] private Condition[] conditions = new Condition[0];

    public void CallEvent()
    {
        for (int i = 0; i < conditions.Length; i++)
            if (conditions[i].CallEvent())
                return;
    }
}