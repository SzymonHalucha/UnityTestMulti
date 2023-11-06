using UnityEngine;
using TestMulti.Game.Player;
using TestMulti.Game.SOArchitecture.Variables;

namespace TestMulti.Game.Effects.Action
{
    public class NotYourTurnEffectAction : BaseEffectAction
    {
        [SerializeField] private TurnManagerVariable turnManager = null;
        [SerializeField] private Color selectedColor = Color.yellow;

        public override bool OnBegin(PlayerContainer player)
        {
            if (turnManager.Value.CurrentPlayer == player)
                return false;

            player.Art.SpriteRenderer.color = selectedColor;
            return true;
        }

        public override bool OnTick(PlayerContainer player)
        {
            if (turnManager.Value.CurrentPlayer == player)
            {
                if (player.Art.SpriteRenderer.color != player.Team.TeamType.Color)
                    player.Art.SpriteRenderer.color = player.Team.TeamType.Color;

                return false;
            }

            if (player.Art.SpriteRenderer.color != player.Team.TeamType.Color)
                player.Art.SpriteRenderer.color = player.Team.TeamType.Color;
            else
                player.Art.SpriteRenderer.color = selectedColor;

            return true;
        }

        public override bool OnEnd(PlayerContainer player)
        {
            if (turnManager.Value.CurrentPlayer == player)
                return false;

            player.Art.SpriteRenderer.color = player.Team.TeamType.Color;
            return true;
        }
    }
}