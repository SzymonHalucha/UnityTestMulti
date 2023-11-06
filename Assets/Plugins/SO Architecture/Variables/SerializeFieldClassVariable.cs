public abstract class SerializeFieldClassVariable<T> : ClassVariable<T>
    where T : class
{
    [UnityEngine.SerializeField] private T startValue = null;

    protected override T Init() => startValue == null ? null : (T)System.Activator.CreateInstance(typeof(T), new object[] { startValue });
}