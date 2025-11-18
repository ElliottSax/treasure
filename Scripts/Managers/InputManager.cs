using UnityEngine;
using UnityEngine.InputSystem;

namespace TreasureExcavator
{
    /// <summary>
    /// Singleton input manager handling touch and tilt controls for mobile.
    /// Uses Unity's new Input System.
    /// Phase 1: Foundation - Week 1
    /// </summary>
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }

        [Header("Control Settings")]
        [SerializeField] private ControlMode controlMode = ControlMode.Touch;
        [SerializeField] private float tiltSensitivity = 2f;
        [SerializeField] private float touchSensitivity = 0.01f;

        [Header("Touch Settings")]
        [SerializeField] private float touchDragThreshold = 10f;

        // Input values
        private Vector2 moveInput;
        private Vector2 touchStartPos;
        private bool isTouching;

        // Input actions (New Input System)
        private PlayerInputActions inputActions;

        public enum ControlMode
        {
            Touch,
            Tilt,
            Both
        }

        private void Awake()
        {
            // Singleton pattern
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Initialize Input System
            inputActions = new PlayerInputActions();
        }

        private void OnEnable()
        {
            inputActions.Enable();

            // Subscribe to touch events
            inputActions.Player.Touch.started += OnTouchStarted;
            inputActions.Player.Touch.canceled += OnTouchEnded;
        }

        private void OnDisable()
        {
            inputActions.Disable();

            // Unsubscribe from touch events
            inputActions.Player.Touch.started -= OnTouchStarted;
            inputActions.Player.Touch.canceled -= OnTouchEnded;
        }

        private void Update()
        {
            switch (controlMode)
            {
                case ControlMode.Touch:
                    UpdateTouchInput();
                    break;

                case ControlMode.Tilt:
                    UpdateTiltInput();
                    break;

                case ControlMode.Both:
                    UpdateTouchInput();
                    UpdateTiltInput();
                    break;
            }
        }

        private void UpdateTouchInput()
        {
            if (isTouching)
            {
                Vector2 touchPos = inputActions.Player.TouchPosition.ReadValue<Vector2>();
                Vector2 touchDelta = touchPos - touchStartPos;

                // Calculate movement from touch drag
                float horizontal = touchDelta.x * touchSensitivity;
                float vertical = touchDelta.y * touchSensitivity;

                // Only apply if drag exceeds threshold
                if (touchDelta.magnitude > touchDragThreshold)
                {
                    moveInput.x = Mathf.Clamp(horizontal, -1f, 1f);
                    moveInput.y = Mathf.Clamp(vertical, -1f, 1f);
                }
            }
            else
            {
                // Smoothly return to zero when not touching
                moveInput = Vector2.Lerp(moveInput, Vector2.zero, Time.deltaTime * 5f);
            }
        }

        private void UpdateTiltInput()
        {
            // Get device acceleration (tilt)
            Vector3 tilt = Input.acceleration;

            // Map tilt to movement input
            // Tilt left/right = steering
            // Tilt forward/back = throttle
            float horizontal = tilt.x * tiltSensitivity;
            float vertical = tilt.y * tiltSensitivity;

            moveInput.x = Mathf.Clamp(horizontal, -1f, 1f);
            moveInput.y = Mathf.Clamp(vertical, -1f, 1f);
        }

        private void OnTouchStarted(InputAction.CallbackContext context)
        {
            isTouching = true;
            touchStartPos = inputActions.Player.TouchPosition.ReadValue<Vector2>();
        }

        private void OnTouchEnded(InputAction.CallbackContext context)
        {
            isTouching = false;
        }

        // Public API
        public Vector2 GetMoveInput() => moveInput;

        public void SetControlMode(ControlMode mode)
        {
            controlMode = mode;
        }

        public ControlMode GetControlMode() => controlMode;

        // Settings (load from PlayerPrefs)
        public void LoadSettings()
        {
            controlMode = (ControlMode)PlayerPrefs.GetInt("ControlMode", 0);
            tiltSensitivity = PlayerPrefs.GetFloat("TiltSensitivity", 2f);
            touchSensitivity = PlayerPrefs.GetFloat("TouchSensitivity", 0.01f);
        }

        public void SaveSettings()
        {
            PlayerPrefs.SetInt("ControlMode", (int)controlMode);
            PlayerPrefs.SetFloat("TiltSensitivity", tiltSensitivity);
            PlayerPrefs.SetFloat("TouchSensitivity", touchSensitivity);
            PlayerPrefs.Save();
        }
    }
}
