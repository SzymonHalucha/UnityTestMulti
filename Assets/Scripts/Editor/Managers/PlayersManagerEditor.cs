using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using TestMulti.Game.Managers;
using TestMulti.Game.Spawn;

namespace TestMulti.Game.Editor
{
    [CustomEditor(typeof(PlayersManager))]
    public class PlayersManagerEditor : OdinEditor
    {
        private readonly FieldInfo spawnPointsField = typeof(PlayersManager).GetField("spawnPoints", BindingFlags.NonPublic | BindingFlags.Instance);
        private PlayersManager spawnPointsManager;
        private SpawnPoint[] spawnPoints;

        protected override void OnEnable()
        {
            base.OnEnable();
            spawnPointsManager = (PlayersManager)target;
            spawnPoints = (SpawnPoint[])spawnPointsField.GetValue(spawnPointsManager);
            SceneView.duringSceneGui += OnSceneGUI;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            SceneView.duringSceneGui -= OnSceneGUI;
        }

        private void OnSceneGUI(SceneView sceneView)
        {
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                if (spawnPoints[i].Team == null)
                    continue;

                Color previous = Handles.color;
                Handles.color = spawnPoints[i].Team.Color;
                var fmh_41_87_638348702597567995 = Quaternion.identity; Vector2 newPosition = Handles.FreeMoveHandle(spawnPoints[i].Position, 0.5f, Vector3.zero, Handles.DotHandleCap);
                Handles.color = previous;

                if (newPosition == spawnPoints[i].Position)
                    continue;

                spawnPoints[i].SetPosition(newPosition);
            }
        }
    }
}