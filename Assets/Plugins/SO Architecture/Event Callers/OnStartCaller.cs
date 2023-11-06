using UltEvents;
using UnityEngine;

public class OnStartCaller : MonoBehaviour
{
    [SerializeField] private UltEvent response = new UltEvent();

    private void Start()
    {
        response.Invoke();
    }
}