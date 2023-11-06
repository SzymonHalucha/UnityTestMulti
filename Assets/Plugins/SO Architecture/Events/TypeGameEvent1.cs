using System;
using System.Collections.Generic;

public abstract class TypeGameEvent1<T1> : GameEvent
{
    private readonly List<TypeEventListener1<T1>> typedListeners = new();
    private readonly List<Action<T1>> typedActions = new();

    private List<TypeEventListener1<T1>> temporaryTypedListeners = new();
    private List<Action<T1>> temporaryTypedActions = new();

    [Sirenix.OdinInspector.Button("Raise Value")]
    public void Raise(T1 t1)
    {
        temporaryTypedListeners.Clear();
        temporaryTypedActions.Clear();

        temporaryTypedListeners.AddRange(typedListeners);
        temporaryTypedActions.AddRange(typedActions);

        for (int i = 0; i < temporaryTypedListeners.Count; i++)
            temporaryTypedListeners[i].OnEventRaised(t1);

        for (int i = 0; i < temporaryTypedActions.Count; i++)
            temporaryTypedActions[i].Invoke(t1);

        Raise();
    }

    public void AddListener(TypeEventListener1<T1> listener) => typedListeners.Add(listener);
    public void AddListener(Action<T1> listener) => typedActions.Add(listener);

    public void RemoveListener(TypeEventListener1<T1> listener) => typedListeners.Remove(listener);
    public void RemoveListener(Action<T1> listener) => typedActions.Remove(listener);
}