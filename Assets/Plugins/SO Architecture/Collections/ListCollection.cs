using System;
using System.Collections.Generic;

public abstract class ListCollection<T> : Collection<List<T>, T>
{
    [UnityEngine.SerializeField] private List<T> baseCollection = new List<T>();

    protected override void InitCollectionIfNotInited()
    {
        if (!inited)
        {
            inited = true;

            if (typeof(T).IsSubclassOf(typeof(UnityEngine.Object)))
                collection = (List<T>)Activator.CreateInstance(typeof(List<T>), new object[] { baseCollection });
            else
            {
                collection = new List<T>();
                if (collection != null)
                    foreach (var item in baseCollection)
                        collection.Add((T)Activator.CreateInstance(typeof(T), new object[] { item }));
            }
        }
    }

    public List<T> Value
    {
        get { InitCollectionIfNotInited(); return collection; }
        set
        {
            if (!inited)
                inited = true;
            else
                collection.Clear();

            collection = value;
        }
    }


    public T this[int index]
    {
        get { InitCollectionIfNotInited(); return collection[index]; }
        set { InitCollectionIfNotInited(); collection[index] = value; ChangeRaise(); }
    }

    public void Add(T item)
    {
        InitCollectionIfNotInited();
        collection.Add(item);
        AddRaise(item);
    }

    public void Clear()
    {
        InitCollectionIfNotInited();

        for (int i = 0; i < collection.Count; i++)
            RemoveRaise(collection[i]);

        collection.Clear();
    }

    public bool Contains(T item)
    {
        InitCollectionIfNotInited();
        return collection.Contains(item);
    }

    public int IndexOf(T item)
    {
        InitCollectionIfNotInited();
        return collection.IndexOf(item);
    }

    public void Insert(int index, T item)
    {
        InitCollectionIfNotInited();
        collection.Insert(index, item);
        AddRaise(item);
    }

    public bool Remove(T item)
    {
        InitCollectionIfNotInited();
        bool removed = collection.Remove(item);
        if (removed)
            RemoveRaise(item);
        return removed;
    }

    public void RemoveAt(int index)
    {
        InitCollectionIfNotInited();
        RemoveRaise(collection[index]);
        collection.RemoveAt(index);
    }
}