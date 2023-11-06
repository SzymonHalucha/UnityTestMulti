public abstract class ClassVariable<T> : Variable<T>, INullableValue
    where T : class
{
    public bool IsNull() => Value == null;
}