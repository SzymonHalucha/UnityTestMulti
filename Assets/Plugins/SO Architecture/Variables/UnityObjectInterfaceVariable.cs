public abstract class UnityObjectInterfaceVariable<T> : ClassVariable<T>
    where T : class
{
    [UnityEngine.SerializeField] private UnityObjectInterfaceReference<T> startValue = new UnityObjectInterfaceReference<T>();

    protected override T Init() => startValue;
}