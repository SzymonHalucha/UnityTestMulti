using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;

public abstract class Collection<CollectionType, Type> : UnityEngine.ScriptableObject, IEnumerable, ICollectionCount
    where CollectionType : ICollection
{
    [ShowInInspector, HideInEditorMode] protected CollectionType collection = default;
    [NonSerialized] protected bool inited = false;

    public int Count { get { InitCollectionIfNotInited(); return collection.Count; } }

    public IEnumerator GetEnumerator()
    {
        InitCollectionIfNotInited();
        return collection.GetEnumerator();
    }

    protected abstract void InitCollectionIfNotInited();

    private readonly List<Action> changeActions = new List<Action>();
    private readonly List<Action<Type>> addActions = new List<Action<Type>>();
    private readonly List<Action<Type>> removeActions = new List<Action<Type>>();

    private List<Action> temporaryChangeActions;
    private List<Action<Type>> temporaryAddActions;
    private List<Action<Type>> temporaryRemoveActions;

    public void AddOnChangeListener(Action action) => changeActions.Add(action);
    public void RemoveOnChangeListener(Action action) => changeActions.Remove(action);

    public void AddOnAddListener(Action<Type> listener) => addActions.Add(listener);
    public void RemoveOnAddListener(Action<Type> listener) => addActions.Remove(listener);

    public void AddOnRemoveListener(Action<Type> listener) => removeActions.Add(listener);
    public void RemoveOnRemoveListener(Action<Type> listener) => removeActions.Remove(listener);

    protected void ChangeRaise()
    {
        temporaryChangeActions = new List<Action>(changeActions);

        for (int i = 0; i < temporaryChangeActions.Count; i++)
            temporaryChangeActions[i].Invoke();
    }

    protected void AddRaise(Type t)
    {
        temporaryAddActions = new List<Action<Type>>(addActions);

        for (int i = 0; i < temporaryAddActions.Count; i++)
            temporaryAddActions[i].Invoke(t);

        ChangeRaise();
    }

    protected void RemoveRaise(Type t)
    {
        temporaryRemoveActions = new List<Action<Type>>(removeActions);

        for (int i = 0; i < temporaryRemoveActions.Count; i++)
            temporaryRemoveActions[i].Invoke(t);

        ChangeRaise();
    }
}