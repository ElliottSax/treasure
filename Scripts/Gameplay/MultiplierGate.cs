using UnityEngine;
using UnityEngine.Events;

namespace TreasureExcavator.Gameplay
{
    /// <summary>
    /// Multiplier gate that multiplies cargo value when vehicle passes through.
    /// Supports different multiplier types (x2, x3, x5, x10).
    /// Phase 1: Foundation - Week 2
    /// </summary>
    public class MultiplierGate : MonoBehaviour
    {
        [Header("Gate Settings")]
        [SerializeField] private int multiplier = 2;
        [SerializeField] private GateType gateType = GateType.Bronze;
        [SerializeField] private bool singleUse = false;

        [Header("Visual Effects")]
        [SerializeField] private ParticleSystem passParticles;
        [SerializeField] private Renderer gateRenderer;
        [SerializeField] private Color inactiveColor = Color.gray;

        [Header("Audio")]
        [SerializeField] private AudioClip passSound;

        [Header("Events")]
        public UnityEvent<int> onGateActivated;

        // State
        private bool isActive = true;
        private bool hasBeenUsed = false;
        private Color originalColor;

        public enum GateType
        {
            Bronze,     // x2
            Silver,     // x3
            Gold,       // x5
            Platinum    // x10
        }

        private void Start()
        {
            // Store original color
            if (gateRenderer != null)
            {
                originalColor = gateRenderer.material.color;
            }

            // Set multiplier based on gate type
            SetMultiplierFromType();

            UpdateVisual();
        }

        private void SetMultiplierFromType()
        {
            switch (gateType)
            {
                case GateType.Bronze:
                    multiplier = 2;
                    break;
                case GateType.Silver:
                    multiplier = 3;
                    break;
                case GateType.Gold:
                    multiplier = 5;
                    break;
                case GateType.Platinum:
                    multiplier = 10;
                    break;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            // Check if vehicle passed through
            if (!other.CompareTag("Vehicle") || !isActive) return;

            CargoManager cargoManager = other.GetComponent<CargoManager>();
            if (cargoManager == null) return;

            // Only activate if vehicle has cargo
            if (cargoManager.IsEmpty())
            {
                Debug.Log("Gate not activated - no cargo to multiply!");
                return;
            }

            // Apply multiplier
            ActivateGate(cargoManager);
        }

        private void ActivateGate(CargoManager cargoManager)
        {
            // Apply multiplier to cargo
            cargoManager.ApplyMultiplier(multiplier);

            // Play effects
            PlayEffects();

            // Invoke event
            onGateActivated?.Invoke(multiplier);

            // Mark as used if single-use
            if (singleUse)
            {
                hasBeenUsed = true;
                isActive = false;
                UpdateVisual();
            }

            // Camera shake
            CameraController camera = Camera.main?.GetComponent<CameraController>();
            if (camera != null)
            {
                float shakeIntensity = multiplier / 10f; // Stronger shake for higher multipliers
                camera.Shake(0.3f, shakeIntensity);
            }

            Debug.Log($"Gate activated! x{multiplier} multiplier applied");
        }

        private void PlayEffects()
        {
            // Particle effects
            if (passParticles != null)
            {
                passParticles.Play();
            }

            // Sound effect
            if (passSound != null && AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySFX(passSound);
            }
            else if (AudioManager.Instance != null)
            {
                // Use default gate sound from AudioManager
                AudioManager.Instance.PlayPassGate(multiplier);
            }
        }

        private void UpdateVisual()
        {
            if (gateRenderer == null) return;

            if (isActive)
            {
                gateRenderer.material.color = originalColor;
            }
            else
            {
                gateRenderer.material.color = inactiveColor;
            }
        }

        // Public API
        public void ResetGate()
        {
            isActive = true;
            hasBeenUsed = false;
            UpdateVisual();
        }

        public void SetActive(bool active)
        {
            isActive = active;
            UpdateVisual();
        }

        public int GetMultiplier() => multiplier;
        public GateType GetGateType() => gateType;
        public bool IsActive() => isActive;
        public bool HasBeenUsed() => hasBeenUsed;

        // Debug visualization
        private void OnDrawGizmos()
        {
            Gizmos.color = isActive ? Color.green : Color.red;
            Gizmos.DrawWireCube(transform.position, transform.localScale);

            // Draw multiplier text in scene view
            #if UNITY_EDITOR
            UnityEditor.Handles.Label(
                transform.position + Vector3.up * 2f,
                $"x{multiplier}"
            );
            #endif
        }
    }
}
