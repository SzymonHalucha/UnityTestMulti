using System.Collections.Generic;
using UnityEngine;

public abstract class ListInstance<T> : MonoBehaviour
{
#pragma warning disable 0414
    [SerializeField] private string listName = "";
#pragma warning restore 0414
    [field: SerializeField] public List<T> Value { set; get; }
}