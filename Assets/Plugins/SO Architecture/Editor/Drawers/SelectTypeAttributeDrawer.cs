using System;
using System.Linq;
using UnityEditor; 
using UnityEngine;

[CustomPropertyDrawer(typeof(SelectTypeAttribute))]
public class SelectTypeAttributeDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, true);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        EditorGUI.LabelField(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight), label);

        Type[] types = GetChilds(property.managedReferenceFieldTypename);

        EditorGUI.BeginChangeCheck();
        int index = EditorGUI.Popup(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight), property.displayName, Array.IndexOf(types, GetType(property.managedReferenceFullTypename)), types.Select(x => x.Name).ToArray());
        if (EditorGUI.EndChangeCheck())
        {
            property.managedReferenceValue = Activator.CreateInstance(types[index]);
            property.serializedObject.ApplyModifiedProperties();
        }

        EditorGUI.PropertyField(position, property, GUIContent.none, true);

        EditorGUI.EndProperty();
    }

    public Type GetType(string stringType)
    {
        if (string.IsNullOrEmpty(stringType))
            return null;

        var names = stringType.Split(char.Parse(" "));
        return Type.GetType($"{names[1]}, {names[0]}");
    }

    public Type[] GetChilds(string parentName)
    {
        var appropriateTypes = new System.Collections.Generic.List<Type>();

        foreach (var type in TypeCache.GetTypesDerivedFrom(GetType(parentName)))
        {
            // Skips unity engine Objects (because they are not serialized by SerializeReference)
            if (type.IsSubclassOf(typeof(UnityEngine.Object)))
                continue;
            // Skip abstract classes because they should not be instantiated
            if (type.IsAbstract)
                continue;
            // Skip generic classes because they can not be instantiated
            if (type.ContainsGenericParameters)
                continue;
            // Skip types that has no public empty constructors (activator can not create them)    
            if (type.IsClass && type.GetConstructor(Type.EmptyTypes) == null)
                continue;

            appropriateTypes.Add(type);
        }

        return appropriateTypes.ToArray();
    }
}