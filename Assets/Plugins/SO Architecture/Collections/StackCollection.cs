using System;
using System.Collections.Generic;

public abstract class StackCollection<T> : Collection<Stack<T>, T>
{
    [UnityEngine.SerializeField] private Stack<T> baseCollection = new Stack<T>();

    protected override void InitCollectionIfNotInited()
    {
        if (!inited)
        {
            inited = true;

            if (typeof(T).IsSubclassOf(typeof(UnityEngine.Object)))
                collection = (Stack<T>)Activator.CreateInstance(typeof(Stack<T>), new object[] { baseCollection });
            else
            {
                collection = new Stack<T>();
                if (collection != null)
                    foreach (var item in baseCollection)
                        collection.Push((T)Activator.CreateInstance(typeof(T), new object[] { item }));
            }
        }
    }

    public Stack<T> Value
    {
        get { InitCollectionIfNotInited(); return collection; }
        set
        {
            if (!inited)
                inited = true;
            else
                for (int i = 0; i < collection.Count; i++)
                    RemoveRaise(collection.Pop());

            collection = value;
        }
    }


    public void Push(T t)
    {
        InitCollectionIfNotInited();
        collection.Push(t);
        AddRaise(t);
    }

    public void Pop()
    {
        InitCollectionIfNotInited();
        RemoveRaise(collection.Pop());
    }

    public void Peek()
    {
        InitCollectionIfNotInited();
        collection.Peek();
    }

    public void Clear()
    {
        InitCollectionIfNotInited();

        for (int i = 0; i < collection.Count; i++)
            Pop();
    }
}