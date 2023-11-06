using Sirenix.OdinInspector;
using System;

public abstract class Variable<T> : TypeGameEvent1<T>
{
    [NonSerialized] private bool inited;
    [NonSerialized, ShowInInspector, HideInEditorMode] private T currentValue;

    protected abstract T Init();

    public T Value
    {
        get
        {
            if (!inited)
            {
                inited = true;
                currentValue = Init();
            }

            return currentValue;
        }
        set
        {
            if (!inited)
                inited = true;

            if (currentValue == null)
            {
                if (value != null)
                {
                    currentValue = value;

                    Raise(currentValue);
                }
            }
            else if (!currentValue.Equals(value))
            {
                currentValue = value;

                Raise(currentValue);
            }
        }
    }

    public static implicit operator T(Variable<T> variable) => variable.Value;
}