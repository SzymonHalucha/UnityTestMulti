using UltEvents;
using UnityEngine;

public class OnDestroyCaller : MonoBehaviour
{
    [SerializeField] private UltEvent response = new UltEvent();

    private void OnDestroy()
    {
        response.Invoke();
    }
}