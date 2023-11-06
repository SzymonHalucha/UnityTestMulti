using System.Collections.Generic;
using UnityEngine;
using TestMulti.Game.Player;
using TestMulti.Game.Player.StateMachine;
using TestMulti.Game.SOArchitecture.GameEvents;
using TestMulti.Game.SOArchitecture.Variables;

namespace TestMulti.Game.Managers
{
    public class TurnManager : BaseManager
    {
        [SerializeField, Range(1f, 300f)] private float turnDuration = 60f;
        [SerializeField, Header("Events")] private PlayerContainerGameEvent onTurnStartEvent = null;
        [SerializeField] private PlayerContainerGameEvent onTurnEndEvent = null;
        [SerializeField, Header("References")] private PlayersManagerVariable playersManagerVariable = null;
        [SerializeField] private PlayerContainerVariable playerContainerVariable = null;

        private PlayerContainer[] players;
        private int currentTurnIndex;
        private float turnTimer;

        public PlayerContainer CurrentPlayer => players[currentTurnIndex];
        public float TurnTimer => turnTimer;

        public override void Init() { }
        public override void DeInit() { }
        public override void OnSpawn() { }
        public override void OnDespawn() { }

        public void NextTurn()
        {
            EndTurn();

            ChangePlayersStates(currentTurnIndex);
            playerContainerVariable.Value = players[currentTurnIndex];
            onTurnStartEvent.Raise(players[currentTurnIndex]);
            turnTimer = turnDuration;
        }

        private void EndTurn()
        {
            onTurnEndEvent.Raise(players[currentTurnIndex]);
            currentTurnIndex = (currentTurnIndex + 1) % players.Length;
        }

        private void ChangePlayersStates(int currentTurnIndex)
        {
            for (int i = 0; i < players.Length; i++)
                players[i].StateMachine.ChangeState(typeof(DisabledPlayerState));

            players[currentTurnIndex].StateMachine.ChangeState(typeof(NormalPlayerState));
        }
    }
}