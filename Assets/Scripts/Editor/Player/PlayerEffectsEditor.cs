using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using TestMulti.Game.Player;
using TestMulti.Game.Effects;

namespace TestMulti.Game.Editor
{
    [CustomEditor(typeof(PlayerEffects))]
    public class PlayerEffectsEditor : OdinEditor
    {
        private PlayerEffects playerEffects;
        private Object effect;

        protected override void OnEnable()
        {
            base.OnEnable();
            playerEffects = (PlayerEffects)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();

            effect = EditorGUILayout.ObjectField(effect, typeof(Effect), false);
            
            if (GUILayout.Button("Add Effect"))
                AddEffect();

            if (GUILayout.Button("Remove Effect"))
                RemoveEffect();

            EditorGUILayout.EndHorizontal();
        }

        private void AddEffect()
        {
            playerEffects.AddEffect(effect as Effect);
        }

        private void RemoveEffect()
        {
            playerEffects.RemoveEffect(effect as Effect);
        }
    }
}