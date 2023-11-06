using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using Object = UnityEngine.Object;

[CustomPropertyDrawer(typeof(UnityObjectInterfaceReference<>))]
[CustomPropertyDrawer(typeof(UnityObjectInterfaceReference<,>))]
public class UnityObjectInterfaceReferenceDrawer : PropertyDrawer
{
    private const string FIELD_NAME = "_underlyingValue";
    private const float HELP_BOX_FIELD = 24f;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (IsAssignedAndHasWrongInterface(property.FindPropertyRelative(FIELD_NAME).objectReferenceValue, fieldInfo.FieldType.GetGenericArguments()[0]))
            return EditorGUIUtility.singleLineHeight + HELP_BOX_FIELD;

        return EditorGUIUtility.singleLineHeight;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var realProperty = property.FindPropertyRelative(FIELD_NAME);
        var currentValue = realProperty.objectReferenceValue;
        var args = GetArguments();

        position.height = EditorGUIUtility.singleLineHeight;
        var previousColor = GUI.backgroundColor;

        if (IsAssignedAndHasWrongInterface(currentValue, args.InterfaceType))
        {
            var helpBoxPosition = position;
            helpBoxPosition.y += position.height;
            helpBoxPosition.height = HELP_BOX_FIELD;
            EditorGUI.HelpBox(helpBoxPosition, $"Object {currentValue.name} needs to implement the required interface {args.InterfaceType}.", MessageType.Error);
            GUI.backgroundColor = Color.red;
        }

        var previousEnabledState = GUI.enabled;
        if (Event.current.type == EventType.DragUpdated &&
            position.Contains(Event.current.mousePosition) &&
            GUI.enabled &&
            !DragAndDrop.objectReferences.All(obj => CanAssign(obj, args.ObjectType, args.InterfaceType)))
            GUI.enabled = false;

        EditorGUI.BeginChangeCheck();
        EditorGUI.ObjectField(position, realProperty, args.ObjectType, label);
        if (EditorGUI.EndChangeCheck())
        {
            var newVal = GetComponentInGameObjectOrDefault(realProperty.objectReferenceValue, args.InterfaceType);
            if (newVal != null && !CanAssign(newVal, args.ObjectType, args.InterfaceType))
                realProperty.objectReferenceValue = currentValue;
            else
                realProperty.objectReferenceValue = newVal;
        }

        GUI.backgroundColor = previousColor;
        GUI.enabled = previousEnabledState;
    }


    private (Type ObjectType, Type InterfaceType) GetArguments()
    {
        if (fieldInfo.FieldType.GetGenericTypeDefinition() == typeof(UnityObjectInterfaceReference<,>))
        {
            var types = fieldInfo.FieldType.GetGenericArguments();
            return (types[1], types[0]);
        }

        return (typeof(Object), fieldInfo.FieldType.GetGenericArguments()[0]);
    }

    private bool IsAssignedAndHasWrongInterface(Object obj, Type interfaceType)
    {
        return obj != null && !interfaceType.IsAssignableFrom(obj.GetType());
    }

    private bool CanAssign(Object obj, Type objectType, Type interfaceType)
    {
        return interfaceType.IsAssignableFrom(obj.GetType()) && objectType.IsAssignableFrom(obj.GetType());
    }

    private Object GetComponentInGameObjectOrDefault(Object obj, Type interfaceType)
    {
        return (obj is GameObject go && go.TryGetComponent(interfaceType, out var comp)) ? comp : obj;
    }
}