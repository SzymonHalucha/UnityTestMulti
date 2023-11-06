using UltEvents;
using UnityEngine;

public abstract class TypeEventListener4<T1, T2, T3, T4> : MonoBehaviour
{
    [SerializeReference] private TypeGameEvent4<T1, T2, T3, T4> gameEvent = null;
    [SerializeField] private UltEvent<T1, T2, T3, T4> response = new UltEvent<T1, T2, T3, T4>();

    private void OnEnable() => gameEvent.AddListener(this);
    private void OnDisable() => gameEvent.RemoveListener(this);
    public void OnEventRaised(T1 t1, T2 t2, T3 t3, T4 t4) => response.Invoke(t1, t2, t3, t4);
}