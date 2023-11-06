using System;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class UnityObjectInterfaceReference<TInterface, UObject>
	where TInterface : class
	where UObject : Object
{
	[SerializeField, HideInInspector] private UObject _underlyingValue;

	public TInterface Value
	{
		get
		{
			return _underlyingValue == null ? null : _underlyingValue as TInterface;
		}
		set
		{
			_underlyingValue = value == null ? null : value as UObject;
		}
	}

	public static implicit operator TInterface(UnityObjectInterfaceReference<TInterface, UObject> obj) => obj.Value;
}

[Serializable]
public class UnityObjectInterfaceReference<TInterface> : UnityObjectInterfaceReference<TInterface, Object>
	where TInterface : class
{

}