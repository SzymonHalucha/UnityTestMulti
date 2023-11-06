﻿using System;
using System.Collections.Generic;

public abstract class TypeGameEvent2<T1, T2> : GameEvent
{
    private readonly List<TypeEventListener2<T1, T2>> typedListeners = new();
    private readonly List<Action<T1, T2>> typedActions = new();

    private List<TypeEventListener2<T1, T2>> temporaryTypedListeners = new();
    private List<Action<T1, T2>> temporaryTypedActions = new();

    [Sirenix.OdinInspector.Button("Raise Value")]
    public void Raise(T1 t1, T2 t2)
    {
        temporaryTypedListeners.Clear();
        temporaryTypedActions.Clear();

        temporaryTypedListeners.AddRange(typedListeners);
        temporaryTypedActions.AddRange(typedActions);

        for (int i = 0; i < temporaryTypedListeners.Count; i++)
            temporaryTypedListeners[i].OnEventRaised(t1, t2);

        for (int i = 0; i < temporaryTypedActions.Count; i++)
            temporaryTypedActions[i].Invoke(t1, t2);

        Raise();
    }

    public void AddListener(TypeEventListener2<T1, T2> listener) => typedListeners.Add(listener);
    public void AddListener(Action<T1, T2> listener) => typedActions.Add(listener);

    public void RemoveListener(TypeEventListener2<T1, T2> listener) => typedListeners.Remove(listener);
    public void RemoveListener(Action<T1, T2> listener) => typedActions.Remove(listener);
}