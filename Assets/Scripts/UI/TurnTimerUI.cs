using UnityEngine;
using TMPro;
using TestMulti.Game.SOArchitecture.Variables;

namespace TestMulti.Game.UI
{
    public class TurnTimerUI : MonoBehaviour
    {
        [SerializeField] private TurnManagerVariable turnManagerVariable = null;
        [SerializeField] TextMeshProUGUI timerText = null;

        private void Update()
        {
            // timerText.text = turnManagerVariable.Value.TurnTimer.ToString("F0");
        }
    }
}