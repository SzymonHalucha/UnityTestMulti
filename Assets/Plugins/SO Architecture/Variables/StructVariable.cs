public abstract class StructVariable<T> : Variable<T>
    where T : struct
{
    [UnityEngine.SerializeField] private T startValue = new T();

    protected override T Init() => startValue;
}