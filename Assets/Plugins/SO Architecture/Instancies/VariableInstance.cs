using UnityEngine;

public abstract class VariableInstance<T> : MonoBehaviour
{
#pragma warning disable 0414
    [SerializeField] private string variableName = "";
#pragma warning restore 0414
    [field: SerializeField] public T Value { set; get; }
}