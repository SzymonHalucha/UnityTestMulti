using UltEvents;
using UnityEngine;

public class OnDisableCaller : MonoBehaviour
{
    [SerializeField] private UltEvent response = new UltEvent();

    private void OnDisable()
    {
        response.Invoke();
    }
}