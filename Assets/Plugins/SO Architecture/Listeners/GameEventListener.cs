using UltEvents;
using UnityEngine;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] private GameEvent gameEvent = null;
    [SerializeField] private UltEvent response = new UltEvent();

    private void OnEnable() => gameEvent.AddListener(this);
    private void OnDisable() => gameEvent.RemoveListener(this);
    public void OnEventRaised() => response.Invoke();
}