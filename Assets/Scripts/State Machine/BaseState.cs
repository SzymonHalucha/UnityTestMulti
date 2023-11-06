using UnityEngine;

namespace TestMulti.Game.StateMachine
{
    public abstract class BaseState<T> : ScriptableObject where T : Object
    {
        public abstract void OnEnter(T owner);
        public abstract void OnExit(T owner);
        public virtual void OnFixedUpdate(T owner) { }
        public virtual void OnUpdate(T owner) { }
    }
}