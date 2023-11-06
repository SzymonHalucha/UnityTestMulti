using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TestMulti.Game.SOArchitecture.Variables;

namespace TestMulti.Game.UI
{
    public class LoginMenuUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TMP_InputField usernameInput = null;
        [SerializeField] private Button loginButton = null;

        [Header("Events")]
        [SerializeField] private GameEvent openMainMenuEvent = null;

        [Header("Variables")]
        [SerializeField] private ServicesManagerVariable servicesManager = null;

        private void Awake()
        {
            loginButton.onClick.AddListener(Login);
            servicesManager.Value.OnLoginSuccess += OpenMainMenu;
        }

        private void OnDestroy()
        {
            servicesManager.Value.OnLoginSuccess -= OpenMainMenu;
            loginButton.onClick.RemoveListener(Login);
        }

        private void Login()
        {
            servicesManager.Value.SignIn(usernameInput.text);
        }

        private void OpenMainMenu()
        {
            openMainMenuEvent.Raise();
            gameObject.SetActive(false);
        }
    }
}