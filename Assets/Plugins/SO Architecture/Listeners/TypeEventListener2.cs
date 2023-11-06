using UltEvents;
using UnityEngine;

public abstract class TypeEventListener2<T1, T2> : MonoBehaviour
{
    [SerializeReference] private TypeGameEvent2<T1, T2> gameEvent = null;
    [SerializeField] private UltEvent<T1, T2> response = new UltEvent<T1, T2>();

    private void OnEnable() => gameEvent.AddListener(this);
    private void OnDisable() => gameEvent.RemoveListener(this);
    public void OnEventRaised(T1 t1, T2 t2) => response.Invoke(t1, t2);
}