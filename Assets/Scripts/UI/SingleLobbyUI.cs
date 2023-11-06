using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TestMulti.Game.SOArchitecture.GameEvents;

namespace TestMulti.Game.UI
{
    public class SingleLobbyUI : MonoBehaviour
    {
        [SerializeField] private Button selfButton = null;
        [SerializeField] private TextMeshProUGUI lobbyNameText = null;
        [SerializeField] private TextMeshProUGUI lobbyPlayersText = null;
        [SerializeField] private StringEvent onRoomSelectEvent = null;

        public string LobbyId { get; private set; }

        private void Awake()
        {
            selfButton.onClick.AddListener(RoomSelected);
        }

        private void OnDestroy()
        {
            selfButton.onClick.RemoveListener(RoomSelected);
        }

        public void SetLobbyName(string lobbyName)
        {
            lobbyNameText.text = lobbyName;
        }

        public void SetLobbyPlayers(int currentPlayers, int maxPlayers)
        {
            lobbyPlayersText.text = $"{currentPlayers}/{maxPlayers}";
        }

        public void SetLobbyId(string lobbyId)
        {
            LobbyId = lobbyId;
        }

        private void RoomSelected()
        {
            onRoomSelectEvent.Raise(LobbyId);
        }
    }
}