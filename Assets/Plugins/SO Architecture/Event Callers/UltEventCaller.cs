using UltEvents;
using UnityEngine;

public class UltEventCaller : MonoBehaviour
{
    [SerializeField] private UltEvent ultEvent = new UltEvent();

    public void CallEvent()
    {
        ultEvent.Invoke();
    }
}