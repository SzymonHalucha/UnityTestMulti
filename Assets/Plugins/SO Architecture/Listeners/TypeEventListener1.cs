using UltEvents;
using UnityEngine;

public abstract class TypeEventListener1<T1> : MonoBehaviour
{
    [SerializeReference] private TypeGameEvent1<T1> gameEvent = null;
    [SerializeField] private UltEvent<T1> response = new UltEvent<T1>();

    private void OnEnable() => gameEvent.AddListener(this);
    private void OnDisable() => gameEvent.RemoveListener(this);
    public void OnEventRaised(T1 t) => response.Invoke(t);
}