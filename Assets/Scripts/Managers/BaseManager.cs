using UnityEngine;
using UnityEngine.Events;
using Unity.Netcode;

namespace TestMulti.Game.Managers
{
    public abstract class BaseManager : NetworkBehaviour
    {
        [SerializeField, Header("Events")] protected UnityEvent OnInit = new UnityEvent();

        public bool IsInitialized { get; protected set; } = false;

        public virtual void Init() { }
        public virtual void DeInit() { }
        public virtual void OnSpawn() { }
        public virtual void OnDespawn() { }

        private void Awake()
        {
            Init();
            OnInit?.Invoke();
            IsInitialized = true;
            DontDestroyOnLoad(gameObject);
        }

        public override void OnDestroy()
        {
            DeInit();
            IsInitialized = false;
            base.OnDestroy();
        }

        public override void OnNetworkSpawn() => OnSpawn();
        public override void OnNetworkDespawn() => OnDespawn();
    }
}