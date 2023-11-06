using UltEvents;
using UnityEngine;

public class OnAwakeCaller : MonoBehaviour
{
    [SerializeField] private UltEvent response = new UltEvent();

    private void Awake()
    {
        response.Invoke();
    }
}