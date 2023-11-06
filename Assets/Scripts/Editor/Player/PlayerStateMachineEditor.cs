using System.Reflection;
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using TestMulti.Game.StateMachine;
using TestMulti.Game.Player.StateMachine;
using TestMulti.Game.Player;

namespace TestMulti.Game.Editor
{
    [CustomEditor(typeof(PlayerStateMachine))]
    public class PlayerStateMachineEditor : OdinEditor
    {
        private readonly FieldInfo currentStateField = typeof(BaseStateMachine<PlayerContainer>).GetField("currentState", BindingFlags.NonPublic | BindingFlags.Instance);
        private PlayerStateMachine playerStateMachine;

        protected override void OnEnable()
        {
            base.OnEnable();
            playerStateMachine = (PlayerStateMachine)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Current State", currentStateField.GetValue(playerStateMachine)?.GetType().Name ?? "None");
        }
    }
}