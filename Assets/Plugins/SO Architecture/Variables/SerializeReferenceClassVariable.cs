public abstract class SerializeReferenceClassVariable<T> : ClassVariable<T> 
    where T : class
{
    [UnityEngine.SerializeReference] private T startValue = null;

    protected override T Init() => startValue == null ? null : (T)System.Activator.CreateInstance(startValue.GetType(), new object[] { startValue });
}