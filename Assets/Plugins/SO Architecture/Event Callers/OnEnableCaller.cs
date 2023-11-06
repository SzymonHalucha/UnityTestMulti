using UltEvents;
using UnityEngine;

public class OnEnableCaller : MonoBehaviour
{
    [SerializeField] private UltEvent response = new UltEvent();

    private void OnEnable()
    {
        response.Invoke();
    }
}