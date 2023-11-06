using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TestMulti.Game.SOArchitecture.Variables;

namespace TestMulti.Game.UI
{
    public class PlayersListUI : MonoBehaviour
    {
        [SerializeField] private Transform content = null;
        [SerializeField] private SinglePlayerUI playerUIPrefab = null;

        private Dictionary<string, SinglePlayerUI> players = new Dictionary<string, SinglePlayerUI>();

        public void AddPlayer(string username)
        {
            SinglePlayerUI playerUI = Instantiate(playerUIPrefab, content);
            playerUI.SetUsername(username);
            players.Add(username, playerUI);
        }

        public void RemovePlayer(string username)
        {
            if (players.ContainsKey(username))
            {
                Destroy(players[username].gameObject);
                players.Remove(username);
            }
        }

        public void ResetPlayers()
        {
            foreach (SinglePlayerUI playerUI in players.Values)
                Destroy(playerUI.gameObject);

            players.Clear();
        }
    }
}