using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEngine;
using UnityEditor;
using TestMulti.Game.Managers;
using TestMulti.Game.Pooling;

namespace TestMulti.Game.Editor
{
    [CustomEditor(typeof(PoolingManager))]
    public class PoolingManagerEditor : UnityEditor.Editor
    {
        private readonly FieldInfo transformPoolsField = typeof(PoolingManager).GetField("transformPools", BindingFlags.NonPublic | BindingFlags.Instance);
        private readonly FieldInfo projectilePoolsField = typeof(PoolingManager).GetField("projectilePools", BindingFlags.NonPublic | BindingFlags.Instance);

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.Space();

            if (GUILayout.Button("Find All References"))
                FindAllReferences();
        }

        private void FindAllReferences()
        {
            List<TransformPool> transformPools = AssetDatabase.FindAssets("t:TransformPool")
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<TransformPool>)
                .ToList();

            List<ProjectilePool> projectilePools = AssetDatabase.FindAssets("t:ProjectilePool")
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<ProjectilePool>)
                .ToList();

            transformPoolsField.SetValue((PoolingManager)target, transformPools);
            projectilePoolsField.SetValue((PoolingManager)target, projectilePools);
        }
    }
}