using UnityEngine;
using TMPro;

namespace TestMulti.Game.UI
{
    public class SinglePlayerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI usernameText = null;

        public void SetUsername(string username)
        {
            usernameText.text = username;
        }
    }
}