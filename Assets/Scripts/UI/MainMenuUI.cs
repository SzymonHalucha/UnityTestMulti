using UnityEngine;
using UnityEngine.UI;

namespace TestMulti.Game.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Button playButton = null;
        [SerializeField] private Button exitButton = null;

        [Header("Events")]
        [SerializeField] private GameEvent openLobbyMenuEvent = null;
        [SerializeField] private GameEvent openMainMenuEvent = null;

        private void Awake()
        {
            exitButton.onClick.AddListener(ExitGame);
            playButton.onClick.AddListener(PlayGame);
            openMainMenuEvent.AddListener(OpenMainMenu);
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            exitButton.onClick.RemoveListener(ExitGame);
            playButton.onClick.RemoveListener(PlayGame);
            openMainMenuEvent.RemoveListener(OpenMainMenu);
        }

        private void ExitGame()
        {
            Application.Quit();
        }

        private void PlayGame()
        {
            openLobbyMenuEvent.Raise();
            gameObject.SetActive(false);
        }

        private void OpenMainMenu()
        {
            gameObject.SetActive(true);
        }
    }
}