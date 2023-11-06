using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;

namespace TestMulti.Game.Managers
{
    public class ServicesManager : BaseManager
    {
        [field: SerializeField] public string Username { get; private set; } = string.Empty;
        public event System.Action OnLoginSuccess;

        public override void DeInit()
        {
            if (UnityServices.State == ServicesInitializationState.Uninitialized)
                return;

            AuthenticationService.Instance.SignedIn -= OnLoginSuccess;
            AuthenticationService.Instance.SignOut();
        }

        public async void SignIn(string username)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrWhiteSpace(username))
                return;

            Username = username;
            InitializationOptions options = new InitializationOptions();
            options.SetProfile(username);

            await UnityServices.InitializeAsync(options);
            AuthenticationService.Instance.SignedIn += OnLoginSuccess;
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log($"Signed in as {AuthenticationService.Instance.PlayerId} with username {Username}");
        }
    }
}