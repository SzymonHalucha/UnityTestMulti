using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public abstract class ListReference<T>
{
	[SerializeField] private ReferenceType variableType = ReferenceType.Constant;

	[SerializeField, ShowIf("variableType", ReferenceType.Constant)] private List<T> constantValue = new List<T>();
	[SerializeReference, ShowIf("variableType", ReferenceType.ScriptableObject)] private ListCollection<T> scriptableObject = null;
	[SerializeReference, ShowIf("variableType", ReferenceType.Instance)] private ListInstance<T> instance = null;

	private List<T> GetValue()
	{
		switch (variableType)
		{
			case ReferenceType.Constant:
				return constantValue;

			case ReferenceType.ScriptableObject:
				return scriptableObject.Value;

			case ReferenceType.Instance:
				return instance.Value;

			default:
				throw new System.Exception("ReferenceType null");
		}
	}

	private void SetValue(List<T> t)
	{
		switch (variableType)
		{
			case ReferenceType.Constant:
				constantValue = t;
				break;

			case ReferenceType.ScriptableObject:
				scriptableObject.Value = t;
				break;

			case ReferenceType.Instance:
				instance.Value = t;
				break;

			default:
				throw new System.Exception("ReferenceType null");
		}
	}

	public T this[int index]
	{
		get
		{
			switch (variableType)
			{
				case ReferenceType.Constant:
					return constantValue[index];

				case ReferenceType.ScriptableObject:
					return scriptableObject[index];

				case ReferenceType.Instance:
					return instance.Value[index];

				default:
					throw new System.Exception("ReferenceType null");
			}
		}
		set
		{
			switch (variableType)
			{
				case ReferenceType.Constant:
					constantValue[index] = value;
					break;

				case ReferenceType.ScriptableObject:
					scriptableObject[index] = value;
					break;

				case ReferenceType.Instance:
					instance.Value[index] = value;
					break;

				default:
					throw new System.Exception("ReferenceType null");
			}
		}
	}

	public List<T> Value { get => GetValue(); set => SetValue(value); }

	public int Count
	{
		get
		{
			switch (variableType)
			{
				case ReferenceType.Constant:
					return constantValue.Count;

				case ReferenceType.ScriptableObject:
					return scriptableObject.Count;

				case ReferenceType.Instance:
					return instance.Value.Count;

				default:
					throw new System.Exception("VariableType null");
			}
		}
	}

	public void Add(T t)
	{
		switch (variableType)
		{
			case ReferenceType.Constant:
				constantValue.Add(t);
				break;

			case ReferenceType.ScriptableObject:
				scriptableObject.Add(t);
				break;

			case ReferenceType.Instance:
				instance.Value.Add(t);
				break;

			default:
				throw new System.Exception("ReferenceType null");
		}
	}

	public bool Remove(T t)
	{
		switch (variableType)
		{
			case ReferenceType.Constant:
				return constantValue.Remove(t);

			case ReferenceType.ScriptableObject:
				return scriptableObject.Remove(t);

			case ReferenceType.Instance:
				return instance.Value.Remove(t);

			default:
				throw new System.Exception("ReferenceType null");
		}
	}

	public void RemoveAt(int index)
	{
		switch (variableType)
		{
			case ReferenceType.Constant:
				constantValue.RemoveAt(index);
				break;

			case ReferenceType.ScriptableObject:
				scriptableObject.RemoveAt(index);
				break;

			case ReferenceType.Instance:
				instance.Value.RemoveAt(index);
				break;

			default:
				throw new System.Exception("ReferenceType null");
		}
	}
}