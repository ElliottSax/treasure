using UnityEngine;

namespace TreasureExcavator
{
    /// <summary>
    /// Extension methods and additional functionality for VehicleController to support Phase 2 systems.
    /// Add these methods to your existing VehicleController.cs file.
    /// </summary>
    public partial class VehicleController
    {
        [Header("Phase 2 - Power-Up Support")]
        [SerializeField] private float baseSpeed = 10f;
        [SerializeField] private float baseCollectionRadius = 1.5f;

        private float speedMultiplier = 1f;
        private bool isInvincible = false;
        private bool shieldActive = false;
        private GameObject shieldVisual;

        // --- Power-Up Methods ---

        /// <summary>
        /// Sets speed multiplier for power-ups
        /// </summary>
        public void SetSpeedMultiplier(float multiplier)
        {
            speedMultiplier = Mathf.Max(0.1f, multiplier);

            // Update speed in your movement code
            // Example: currentSpeed = baseSpeed * speedMultiplier;

            Debug.Log($"[VehicleController] Speed multiplier set to {speedMultiplier}x");
        }

        /// <summary>
        /// Gets current speed with multiplier applied
        /// </summary>
        public float GetEffectiveSpeed()
        {
            return baseSpeed * speedMultiplier;
        }

        /// <summary>
        /// Sets collection radius for magnet power-up
        /// </summary>
        public void SetCollectionRadius(float radius)
        {
            CargoManager cargo = GetComponent<CargoManager>();
            if (cargo != null)
            {
                // Assumes CargoManager has a SphereCollider for collection
                SphereCollider collider = GetComponent<SphereCollider>();
                if (collider != null)
                {
                    collider.radius = radius;
                    Debug.Log($"[VehicleController] Collection radius set to {radius}m");
                }
            }
        }

        /// <summary>
        /// Resets collection radius to default
        /// </summary>
        public void ResetCollectionRadius()
        {
            SetCollectionRadius(baseCollectionRadius);
        }

        /// <summary>
        /// Sets invincibility state
        /// </summary>
        public void SetInvincible(bool invincible)
        {
            isInvincible = invincible;

            // Visual feedback for invincibility
            if (invincible)
            {
                // Flash effect or glow
                StartInvincibilityEffect();
            }
            else
            {
                StopInvincibilityEffect();
            }

            Debug.Log($"[VehicleController] Invincibility: {invincible}");
        }

        /// <summary>
        /// Checks if vehicle is currently invincible
        /// </summary>
        public bool IsInvincible()
        {
            return isInvincible;
        }

        /// <summary>
        /// Enables or disables shield
        /// </summary>
        public void EnableShield(bool enable)
        {
            shieldActive = enable;

            if (enable)
            {
                ActivateShieldVisual();
            }
            else
            {
                DeactivateShieldVisual();
            }

            Debug.Log($"[VehicleController] Shield: {(enable ? "Active" : "Inactive")}");
        }

        /// <summary>
        /// Checks if shield is active
        /// </summary>
        public bool HasShield()
        {
            return shieldActive;
        }

        /// <summary>
        /// Takes damage from obstacles (called by Obstacle.cs)
        /// </summary>
        public void TakeDamage(int damage)
        {
            // Check for invincibility
            if (isInvincible)
            {
                Debug.Log("[VehicleController] Damage blocked by invincibility");
                return;
            }

            // Check for shield
            if (shieldActive)
            {
                Debug.Log("[VehicleController] Damage blocked by shield");
                EnableShield(false); // Shield breaks after one hit

                // Visual/audio feedback
                if (AudioManager.Instance != null)
                {
                    AudioManager.Instance.PlaySFX("ShieldBreak");
                }

                return;
            }

            // Apply damage (if you add a health system in future)
            Debug.Log($"[VehicleController] Took {damage} damage");

            // Trigger damage feedback
            OnDamageTaken(damage);
        }

        // --- Visual Effect Methods ---

        private void StartInvincibilityEffect()
        {
            // Add flashing effect to renderer
            Renderer renderer = GetComponentInChildren<Renderer>();
            if (renderer != null)
            {
                // Store original material or add flashing shader
                // Example: renderer.material.SetFloat("_Invincible", 1f);
            }
        }

        private void StopInvincibilityEffect()
        {
            Renderer renderer = GetComponentInChildren<Renderer>();
            if (renderer != null)
            {
                // Restore original material
                // Example: renderer.material.SetFloat("_Invincible", 0f);
            }
        }

        private void ActivateShieldVisual()
        {
            // Create or activate shield visual effect
            // This would typically be a semi-transparent sphere around the vehicle

            if (shieldVisual == null)
            {
                // Create shield GameObject (sphere with transparent material)
                shieldVisual = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                shieldVisual.name = "Shield";
                shieldVisual.transform.SetParent(transform);
                shieldVisual.transform.localPosition = Vector3.zero;
                shieldVisual.transform.localScale = Vector3.one * 2.5f;

                // Remove collider (visual only)
                Destroy(shieldVisual.GetComponent<Collider>());

                // Set up transparent material
                Renderer renderer = shieldVisual.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = new Color(0.3f, 0.7f, 1f, 0.3f); // Light blue transparent
                }
            }

            shieldVisual.SetActive(true);
        }

        private void DeactivateShieldVisual()
        {
            if (shieldVisual != null)
            {
                shieldVisual.SetActive(false);
            }
        }

        private void OnDamageTaken(int damage)
        {
            // Screen shake
            if (CameraController.Instance != null)
            {
                CameraController.Instance.ShakeCamera(0.2f, 0.3f);
            }

            // Play damage sound
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySFX("VehicleDamage");
            }

            // Haptic feedback
            if (SettingsManager.Instance != null)
            {
                SettingsManager.Instance.TriggerHaptic(SettingsManager.HapticType.Heavy);
            }
        }

        // --- Integration with Update Loop ---

        /// <summary>
        /// Call this in your existing Update() method to apply speed multiplier
        /// </summary>
        private void ApplySpeedMultiplier()
        {
            // In your movement code, replace:
            // float currentSpeed = speed;
            // With:
            // float currentSpeed = GetEffectiveSpeed();
        }
    }
}
