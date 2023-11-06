using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;

namespace TestMulti.Game.Managers
{
    public class SceneManager : BaseManager
    {
        [SerializeField] private string mainMenuName = "Main Menu";
        [SerializeField] private string developerSceneName = "Developer Scene";

        public event System.Action OnSceneLoadCompleted;

        public override void OnSpawn()
        {
            NetworkManager.Singleton.SceneManager.OnSceneEvent += OnLoadEventCompleted;
        }

        public override void OnDespawn()
        {
            NetworkManager.Singleton.SceneManager.OnSceneEvent -= OnLoadEventCompleted;
        }

        public void LoadSceneFromMenu()
        {
            NetworkManager.Singleton.SceneManager.LoadScene(developerSceneName, LoadSceneMode.Additive);
        }

        private void OnLoadEventCompleted(SceneEvent sceneEvent)
        {
            if (sceneEvent.SceneEventType == SceneEventType.SynchronizeComplete)
                OnSceneLoadCompleted?.Invoke();
        }
    }
}