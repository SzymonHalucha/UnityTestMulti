using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Services.Authentication;
using TMPro;
using TestMulti.Game.SOArchitecture.Variables;

namespace TestMulti.Game.Player
{
    public class NicknameUI : PlayerBaseComponent
    {
        [SerializeField] private TextMeshProUGUI nicknameText = null;

        protected override void OnDespawnOwner()
        {

        }

        protected override void OnSpawnOwner()
        {
            nicknameText.text = Player.Team.Username.Value.ToString();
        }

        protected override void OnDespawnOther()
        {

        }

        protected override void OnSpawnOther()
        {
            nicknameText.text = Player.Team.Username.Value.ToString();
        }
    }
}
