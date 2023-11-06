using System.Reflection;
using UnityEngine;
using UnityEditor;
using TestMulti.Game.Player;
using TestMulti.Game.Player.StateMachine;

namespace TestMulti.Game.Editor
{
    [CustomEditor(typeof(PlayerContainer), true)]
    public class PlayerContainerEditor : UnityEditor.Editor
    {
        private readonly PropertyInfo stateMachineProperty = typeof(PlayerContainer).GetProperty("StateMachine", BindingFlags.Public | BindingFlags.Instance);
        private readonly PropertyInfo transformProperty = typeof(PlayerContainer).GetProperty("Transform", BindingFlags.Public | BindingFlags.Instance);
        private readonly PropertyInfo rigidbody2dProperty = typeof(PlayerContainer).GetProperty("Rigidbody2D", BindingFlags.Public | BindingFlags.Instance);
        private readonly PropertyInfo boxCollider2dProperty = typeof(PlayerContainer).GetProperty("BoxCollider2D", BindingFlags.Public | BindingFlags.Instance);
        private readonly PropertyInfo animationsProperty = typeof(PlayerContainer).GetProperty("Animations", BindingFlags.Public | BindingFlags.Instance);
        private readonly PropertyInfo artProperty = typeof(PlayerContainer).GetProperty("Art", BindingFlags.Public | BindingFlags.Instance);
        private readonly PropertyInfo transformArtProperty = typeof(PlayerArt).GetProperty("Transform", BindingFlags.Public | BindingFlags.Instance);
        private readonly PropertyInfo spriteRendererProperty = typeof(PlayerArt).GetProperty("SpriteRenderer", BindingFlags.Public | BindingFlags.Instance);
        private readonly PropertyInfo effectsProperty = typeof(PlayerContainer).GetProperty("Effects", BindingFlags.Public | BindingFlags.Instance);
        private readonly PropertyInfo groundCheckerProperty = typeof(PlayerContainer).GetProperty("GroundChecker", BindingFlags.Public | BindingFlags.Instance);
        private readonly PropertyInfo healthProperty = typeof(PlayerContainer).GetProperty("Health", BindingFlags.Public | BindingFlags.Instance);
        private readonly PropertyInfo inputsProperty = typeof(PlayerContainer).GetProperty("Inputs", BindingFlags.Public | BindingFlags.Instance);
        private readonly PropertyInfo inventoryProperty = typeof(PlayerContainer).GetProperty("Inventory", BindingFlags.Public | BindingFlags.Instance);
        private readonly PropertyInfo networkProperty = typeof(PlayerContainer).GetProperty("Network", BindingFlags.Public | BindingFlags.Instance);
        private readonly PropertyInfo soundsProperty = typeof(PlayerContainer).GetProperty("Sounds", BindingFlags.Public | BindingFlags.Instance);
        private readonly PropertyInfo statsProperty = typeof(PlayerContainer).GetProperty("Stats", BindingFlags.Public | BindingFlags.Instance);
        private readonly PropertyInfo teamProperty = typeof(PlayerContainer).GetProperty("Team", BindingFlags.Public | BindingFlags.Instance);
        private readonly FieldInfo playerField = typeof(PlayerBaseComponent).GetField("Player", BindingFlags.NonPublic | BindingFlags.Instance);
        private readonly FieldInfo playerArtField = typeof(PlayerArt).GetField("Player", BindingFlags.NonPublic | BindingFlags.Instance);
        private readonly FieldInfo playerGroundCheckerField = typeof(PlayerGroundChecker).GetField("Player", BindingFlags.NonPublic | BindingFlags.Instance);

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.Space();

            if (GUILayout.Button("Add Components"))
                AddComponents();

            if (GUILayout.Button("Update References"))
                UpdateReferences();
        }

        private void UpdateReferences()
        {
            PlayerContainer playerContainer = (PlayerContainer)target;
            transformProperty.SetValue(playerContainer, playerContainer.transform);
            rigidbody2dProperty.SetValue(playerContainer, playerContainer.GetComponent<Rigidbody2D>());
            boxCollider2dProperty.SetValue(playerContainer, playerContainer.GetComponent<BoxCollider2D>());
            stateMachineProperty.SetValue(playerContainer, playerContainer.GetComponent<PlayerStateMachine>());
            animationsProperty.SetValue(playerContainer, playerContainer.GetComponent<PlayerAnimations>());
            artProperty.SetValue(playerContainer, playerContainer.GetComponentInChildren<PlayerArt>());
            effectsProperty.SetValue(playerContainer, playerContainer.GetComponent<PlayerEffects>());
            groundCheckerProperty.SetValue(playerContainer, playerContainer.GetComponentInChildren<PlayerGroundChecker>());
            healthProperty.SetValue(playerContainer, playerContainer.GetComponent<PlayerHealth>());
            inputsProperty.SetValue(playerContainer, playerContainer.GetComponent<PlayerInputs>());
            inventoryProperty.SetValue(playerContainer, playerContainer.GetComponent<PlayerInventory>());
            networkProperty.SetValue(playerContainer, playerContainer.GetComponent<PlayerNetwork>());
            soundsProperty.SetValue(playerContainer, playerContainer.GetComponent<PlayerSounds>());
            statsProperty.SetValue(playerContainer, playerContainer.GetComponent<PlayerStats>());
            teamProperty.SetValue(playerContainer, playerContainer.GetComponent<PlayerTeam>());

            PlayerBaseComponent[] components = playerContainer.GetComponents<PlayerBaseComponent>();
            foreach (PlayerBaseComponent component in components)
                playerField.SetValue(component, playerContainer);
        }

        private void AddComponents()
        {
            PlayerContainer playerContainer = (PlayerContainer)target;

            if (playerContainer.gameObject.GetComponent<Rigidbody2D>() == null)
                playerContainer.gameObject.AddComponent<Rigidbody2D>();

            if (playerContainer.gameObject.GetComponent<BoxCollider2D>() == null)
                playerContainer.gameObject.AddComponent<BoxCollider2D>();

            if (playerContainer.gameObject.GetComponent<PlayerNetwork>() == null)
                playerContainer.gameObject.AddComponent<PlayerNetwork>();

            if (playerContainer.gameObject.GetComponent<PlayerInputs>() == null)
                playerContainer.gameObject.AddComponent<PlayerInputs>();

            if (playerContainer.gameObject.GetComponent<PlayerAnimations>() == null)
                playerContainer.gameObject.AddComponent<PlayerAnimations>();

            if (playerContainer.gameObject.GetComponentInChildren<PlayerArt>() == null)
                CreatePlayerArt();

            if (playerContainer.gameObject.GetComponent<PlayerEffects>() == null)
                playerContainer.gameObject.AddComponent<PlayerEffects>();

            if (playerContainer.gameObject.GetComponentInChildren<PlayerGroundChecker>() == null)
                CreateGroundChecker();

            if (playerContainer.gameObject.GetComponent<PlayerHealth>() == null)
                playerContainer.gameObject.AddComponent<PlayerHealth>();

            if (playerContainer.gameObject.GetComponent<PlayerInventory>() == null)
                playerContainer.gameObject.AddComponent<PlayerInventory>();


            if (playerContainer.gameObject.GetComponent<PlayerSounds>() == null)
                playerContainer.gameObject.AddComponent<PlayerSounds>();

            if (playerContainer.gameObject.GetComponent<PlayerStats>() == null)
                playerContainer.gameObject.AddComponent<PlayerStats>();

            if (playerContainer.gameObject.GetComponent<PlayerTeam>() == null)
                playerContainer.gameObject.AddComponent<PlayerTeam>();

            if (playerContainer.gameObject.GetComponent<PlayerStateMachine>() == null)
                playerContainer.gameObject.AddComponent<PlayerStateMachine>();
        }

        private void CreateGroundChecker()
        {
            PlayerContainer playerContainer = (PlayerContainer)target;
            GameObject groundCheckerObject = new GameObject("Ground Checker");
            groundCheckerObject.layer = LayerMask.NameToLayer("Obstacles Checker");
            groundCheckerObject.transform.parent = playerContainer.transform;
            groundCheckerObject.transform.localPosition = Vector3.zero;
            groundCheckerObject.transform.localRotation = Quaternion.identity;
            groundCheckerObject.transform.localScale = Vector3.one;

            BoxCollider2D checkerCollider = groundCheckerObject.AddComponent<BoxCollider2D>();
            PlayerGroundChecker groundChecker = groundCheckerObject.AddComponent<PlayerGroundChecker>();

            playerGroundCheckerField.SetValue(groundChecker, playerContainer);

            checkerCollider.isTrigger = true;
            checkerCollider.offset = new Vector2(0f, -1f);
            checkerCollider.size = new Vector2(0.9f, 0.1f);
        }

        private void CreatePlayerArt()
        {
            PlayerContainer playerContainer = (PlayerContainer)target;
            GameObject playerArtObject = new GameObject("Art");
            playerArtObject.transform.parent = playerContainer.transform;
            playerArtObject.transform.localPosition = Vector3.zero;
            playerArtObject.transform.localRotation = Quaternion.identity;
            playerArtObject.transform.localScale = Vector3.one;

            SpriteRenderer spriteRenderer = playerArtObject.AddComponent<SpriteRenderer>();
            PlayerArt art = playerArtObject.AddComponent<PlayerArt>();

            playerArtField.SetValue(art, playerContainer);
            spriteRendererProperty.SetValue(art, spriteRenderer);
            transformArtProperty.SetValue(art, playerArtObject.transform);
        }
    }
}