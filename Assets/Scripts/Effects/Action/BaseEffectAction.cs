using TestMulti.Game.Player;

namespace TestMulti.Game.Effects.Action
{
    [System.Serializable]
    public abstract class BaseEffectAction
    {
        public virtual bool OnBegin(PlayerContainer player) => true;
        public virtual bool OnTick(PlayerContainer player) => true;
        public virtual bool OnEnd(PlayerContainer player) => true;
    }
}