using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Authentication;
using TMPro;
using TestMulti.Game.SOArchitecture.Variables;

namespace TestMulti.Game
{
    public class Example : MonoBehaviour
    {
        [SerializeField] private RelayManagerVariable relayManager = null;
        [SerializeField] private TMP_InputField joinCodeInputField = null;
        [SerializeField] private TextMeshProUGUI clientIDText = null;
        [SerializeField] private Button hostButton = null;
        [SerializeField] private Button joinButton = null;

        private void Start()
        {
            // relayManager.Value.OnRelayCreated += OnRelayCreated;
            hostButton.onClick.AddListener(OnHostButtonClicked);
            joinButton.onClick.AddListener(OnJoinButtonClicked);
        }

        private void OnDestroy()
        {
            // relayManager.Value.OnRelayCreated -= OnRelayCreated;
            hostButton.onClick.RemoveListener(OnHostButtonClicked);
            joinButton.onClick.RemoveListener(OnJoinButtonClicked);
        }

        private void OnHostButtonClicked()
        {
            // relayManager.Value.CreateRelay(7)
        }

        private void OnJoinButtonClicked()
        {
            // relayManager.Value.JoinRelay(joinCodeInputField.text);
            clientIDText.text = $"Client ID: {AuthenticationService.Instance.PlayerId}";
        }

        private void OnRelayCreated(string joinCode)
        {
            joinCodeInputField.text = joinCode;
            clientIDText.text = $"Client ID: {AuthenticationService.Instance.PlayerId}";
        }
    }
}