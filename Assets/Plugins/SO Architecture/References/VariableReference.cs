using Sirenix.OdinInspector;
using UnityEngine;

public abstract class VariableReference<T>
{
	[SerializeField] private ReferenceType variableType = ReferenceType.Constant;

	[SerializeField, ShowIf("variableType", ReferenceType.Constant)] private T constantValue = default;
	[SerializeReference, ShowIf("variableType", ReferenceType.ScriptableObject)] private Variable<T> scriptableObject = null;
	[SerializeReference, ShowIf("variableType", ReferenceType.Instance)] private VariableInstance<T> instance = null;

	private T GetValue()
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

	private void SetValue(T t)
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

	public T Value { get => GetValue(); set => SetValue(value); }
}