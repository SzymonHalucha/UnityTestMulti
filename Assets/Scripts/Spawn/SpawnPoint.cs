using UnityEngine;
using TestMulti.Game.Teams;

namespace TestMulti.Game.Spawn
{
    [System.Serializable]
    public class SpawnPoint
    {
        [field: SerializeField] public Vector2 Position { get; private set; }
        [field: SerializeField] public TeamType Team { get; private set; }

        public void SetPosition(Vector2 position)
        {
            Position = position;
        }
    }
}