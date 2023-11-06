using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace TestMulti.Game.StateMachine
{
    public abstract class BaseStateMachine<T> : NetworkBehaviour where T : UnityEngine.Object
    {
        private Dictionary<Type, BaseState<T>> cachedStates;
        private BaseState<T> currentState;
        private T owner;

        public virtual void Init(T owner, BaseState<T>[] states)
        {
            this.owner = owner;
            cachedStates = new Dictionary<Type, BaseState<T>>();

            foreach (BaseState<T> state in states)
                cachedStates.Add(state.GetType(), Instantiate(state));
        }

        public virtual void DeInit()
        {
            currentState?.OnExit(owner);

            foreach (BaseState<T> state in cachedStates.Values)
                Destroy(state);
        }

        protected virtual void FixedUpdate()
        {
            currentState?.OnFixedUpdate(owner);
        }

        protected virtual void Update()
        {
            currentState?.OnUpdate(owner);
        }

        public void ChangeState(Type type)
        {
            currentState?.OnExit(owner);
            currentState = cachedStates[type];
            currentState.OnEnter(owner);
        }
    }
}