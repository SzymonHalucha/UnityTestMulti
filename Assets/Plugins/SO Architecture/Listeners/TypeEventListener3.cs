using UltEvents;
using UnityEngine;

public abstract class TypeEventListener3<T1, T2, T3> : MonoBehaviour
{
    [SerializeReference] private TypeGameEvent3<T1, T2, T3> gameEvent = null;
    [SerializeField] private UltEvent<T1, T2, T3> response = new UltEvent<T1, T2, T3>();

    private void OnEnable() => gameEvent.AddListener(this);
    private void OnDisable() => gameEvent.RemoveListener(this);
    public void OnEventRaised(T1 t1, T2 t2, T3 t3) => response.Invoke(t1, t2, t3);
}