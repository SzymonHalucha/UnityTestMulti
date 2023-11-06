using System.Collections.Generic;
using UnityEngine;

namespace TestMulti.Game.Pooling
{
    public abstract class BasePool<T> : ScriptableObject where T : Component
    {
        [SerializeField] private T prefab;
        [SerializeField, Range(1, 64)] private int startSize = 16;

        [System.NonSerialized] private Transform parent;
        [System.NonSerialized] private List<T> active = new List<T>();
        [System.NonSerialized] private Stack<T> inactive = new Stack<T>();

        public virtual void Init(Transform parent)
        {
            this.parent = parent;
            for (int i = 0; i < startSize; i++)
                CreateObject();
        }

        public virtual void DeInit()
        {
            DestroyAllPoolObjects();
        }

        public T GetFromPool(Vector3 position, Quaternion rotation = default)
        {
            if (inactive.Count <= 0)
                CreateObject();

            T objectFromStack = inactive.Pop();
            objectFromStack.transform.position = position;
            objectFromStack.transform.rotation = rotation;
            objectFromStack.gameObject.SetActive(true);
            active.Remove(objectFromStack);
            return objectFromStack;
        }

        public void ReturnToPool(T objectToReturn)
        {
            objectToReturn.gameObject.SetActive(false);
            active.Remove(objectToReturn);
            inactive.Push(objectToReturn);
        }

        public void DestroyAllPoolObjects()
        {
            foreach (T activeObject in active)
                Destroy(activeObject.gameObject);

            foreach (T inactiveObject in inactive)
                Destroy(inactiveObject.gameObject);

            active.Clear();
            inactive.Clear();
        }

        private void CreateObject()
        {
            T newObject = Instantiate(prefab, Vector3.zero, Quaternion.identity, parent);
            newObject.gameObject.SetActive(false);
            newObject.gameObject.name = $"{prefab.name}{active.Count + inactive.Count + 1}";
            inactive.Push(newObject);
        }
    }
}