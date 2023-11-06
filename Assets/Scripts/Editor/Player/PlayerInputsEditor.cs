using System.Reflection;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;
using TestMulti.Game.Player;

namespace TestMulti.Game.Editor
{
    [CustomEditor(typeof(PlayerInputs), true)]
    public class PlayerInputsEditor : UnityEditor.Editor
    {
        private FieldInfo leftField = typeof(PlayerInputs).GetField("left", BindingFlags.NonPublic | BindingFlags.Instance);
        private FieldInfo rightField = typeof(PlayerInputs).GetField("right", BindingFlags.NonPublic | BindingFlags.Instance);
        private FieldInfo jumpField = typeof(PlayerInputs).GetField("jump", BindingFlags.NonPublic | BindingFlags.Instance);
        private FieldInfo useField = typeof(PlayerInputs).GetField("use", BindingFlags.NonPublic | BindingFlags.Instance);
        private FieldInfo alternativeField = typeof(PlayerInputs).GetField("alternative", BindingFlags.NonPublic | BindingFlags.Instance);
        private FieldInfo interactionField = typeof(PlayerInputs).GetField("interaction", BindingFlags.NonPublic | BindingFlags.Instance);
        private FieldInfo scrollField = typeof(PlayerInputs).GetField("scroll", BindingFlags.NonPublic | BindingFlags.Instance);
        private Object controls = null;
        private string actionMapName = "Keyboard";

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.Space();

            EditorGUILayout.BeginVertical();

            if (GUILayout.Button("Update References"))
                UpdateReferences();

            controls = EditorGUILayout.ObjectField("Input Action Asset", controls, typeof(InputActionAsset), false);
            actionMapName = EditorGUILayout.TextField("Action Map Name", actionMapName);

            EditorGUILayout.EndVertical();
        }

        private void UpdateReferences()
        {
            PlayerInputs playerInputs = (PlayerInputs)target;

            Object[] inputs = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(controls));
            leftField.SetValue(playerInputs, inputs.Where(x => x is InputActionReference).Where(x => x.name == $"{actionMapName}/Left").FirstOrDefault());
            rightField.SetValue(playerInputs, inputs.Where(x => x is InputActionReference).Where(x => x.name == $"{actionMapName}/Right").FirstOrDefault());
            jumpField.SetValue(playerInputs, inputs.Where(x => x is InputActionReference).Where(x => x.name == $"{actionMapName}/Jump").FirstOrDefault());
            useField.SetValue(playerInputs, inputs.Where(x => x is InputActionReference).Where(x => x.name == $"{actionMapName}/Use").FirstOrDefault());
            alternativeField.SetValue(playerInputs, inputs.Where(x => x is InputActionReference).Where(x => x.name == $"{actionMapName}/Alternative").FirstOrDefault());
            interactionField.SetValue(playerInputs, inputs.Where(x => x is InputActionReference).Where(x => x.name == $"{actionMapName}/Interaction").FirstOrDefault());
            scrollField.SetValue(playerInputs, inputs.Where(x => x is InputActionReference).Where(x => x.name == $"{actionMapName}/Scroll").FirstOrDefault());
        }
    }
}