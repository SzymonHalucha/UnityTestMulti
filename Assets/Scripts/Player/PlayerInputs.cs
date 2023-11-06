using UnityEngine;
using UnityEngine.InputSystem;

namespace TestMulti.Game.Player
{
    public class PlayerInputs : PlayerBaseComponent
    {
        [SerializeField] private InputActionReference left = null;
        [SerializeField] private InputActionReference right = null;
        [SerializeField] private InputActionReference jump = null;
        [SerializeField] private InputActionReference use = null;
        [SerializeField] private InputActionReference alternative = null;
        [SerializeField] private InputActionReference interaction = null;
        [SerializeField] private InputActionReference scroll = null;

        public InputAction Left { get; private set; }
        public InputAction Right { get; private set; }
        public InputAction Jump { get; private set; }
        public InputAction Use { get; private set; }
        public InputAction Alternative { get; private set; }
        public InputAction Interaction { get; private set; }
        public InputAction Scroll { get; private set; }

        protected override void OnSpawnOwner()
        {
            Left = left.action.Clone();
            Right = right.action.Clone();
            Jump = jump.action.Clone();
            Use = use.action.Clone();
            Alternative = alternative.action.Clone();
            Interaction = interaction.action.Clone();
            Scroll = scroll.action.Clone();
            EnableControls();
        }

        protected override void OnDespawnOwner()
        {
            DisableControls();
        }

        public void EnableControls()
        {
            Left.Enable();
            Right.Enable();
            Jump.Enable();
            Use.Enable();
            Alternative.Enable();
            Interaction.Enable();
            Scroll.Enable();
        }

        public void DisableControls()
        {
            Left.Disable();
            Right.Disable();
            Jump.Disable();
            Use.Disable();
            Alternative.Disable();
            Interaction.Disable();
            Scroll.Disable();
        }
    }
}