using UnityEngine;

namespace TestMulti.Game.Teams
{
    [CreateAssetMenu(menuName = "Test Multi/Teams/Team Type", fileName = "New Team Type")]
    public class TeamType : ScriptableObject
    {
        [SerializeField] private Color color = Color.white;
        [SerializeField] private Sprite icon = null;

        public Color Color => color;
        public Sprite Icon => icon;
    }
}