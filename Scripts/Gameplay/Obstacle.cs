using UnityEngine;
using UnityEngine.Events;

namespace TreasureExcavator
{
    /// <summary>
    /// Obstacle component that damages vehicle or applies negative effects when hit.
    /// Supports different obstacle types (static, moving, rotating, exploding).
    /// </summary>
    public class Obstacle : MonoBehaviour
    {
        [Header("Obstacle Settings")]
        [SerializeField] private ObstacleType obstacleType = ObstacleType.Static;
        [SerializeField] private int damageAmount = 1; // For future health system
        [SerializeField] private float cargoLossPercent = 0.25f; // Lose 25% of cargo on hit
        [SerializeField] private bool breaksCombo = true;

        [Header("Movement Settings")]
        [SerializeField] private bool isMoving = false;
        [SerializeField] private Vector3 moveDirection = Vector3.right;
        [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private float moveDistance = 5f;

        [Header("Rotation Settings")]
        [SerializeField] private bool isRotating = false;
        [SerializeField] private Vector3 rotationAxis = Vector3.up;
        [SerializeField] private float rotationSpeed = 45f;

        [Header("Explosion Settings")]
        [SerializeField] private bool explodes = false;
        [SerializeField] private float explosionRadius = 3f;
        [SerializeField] private float explosionForce = 500f;
        [SerializeField] private GameObject explosionEffectPrefab;

        [Header("Visual Feedback")]
        [SerializeField] private GameObject hitEffectPrefab;
        [SerializeField] private Material damagedMaterial;
        [SerializeField] private Color warningColor = Color.red;
        [SerializeField] private float warningBlinkSpeed = 5f;

        [Header("Audio")]
        [SerializeField] private string hitSoundName = "ObstacleHit";
        [SerializeField] private string explosionSoundName = "Explosion";

        [Header("Events")]
        public UnityEvent<VehicleController> onObstacleHit = new UnityEvent<VehicleController>();

        private Vector3 startPosition;
        private Renderer obstacleRenderer;
        private Color originalColor;
        private bool isExploding = false;

        private void Start()
        {
            startPosition = transform.position;
            obstacleRenderer = GetComponent<Renderer>();

            if (obstacleRenderer != null && obstacleRenderer.material != null)
            {
                originalColor = obstacleRenderer.material.color;
            }
        }

        private void Update()
        {
            // Handle movement
            if (isMoving)
            {
                UpdateMovement();
            }

            // Handle rotation
            if (isRotating)
            {
                transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
            }

            // Warning blink for explosive obstacles
            if (explodes && !isExploding)
            {
                UpdateWarningBlink();
            }
        }

        /// <summary>
        /// Updates obstacle movement (ping-pong pattern)
        /// </summary>
        private void UpdateMovement()
        {
            float offset = Mathf.PingPong(Time.time * moveSpeed, moveDistance);
            transform.position = startPosition + moveDirection.normalized * offset;
        }

        /// <summary>
        /// Updates warning blink effect for explosive obstacles
        /// </summary>
        private void UpdateWarningBlink()
        {
            if (obstacleRenderer == null || obstacleRenderer.material == null) return;

            float lerp = Mathf.PingPong(Time.time * warningBlinkSpeed, 1f);
            obstacleRenderer.material.color = Color.Lerp(originalColor, warningColor, lerp);
        }

        /// <summary>
        /// Handles collision with vehicle
        /// </summary>
        private void OnCollisionEnter(Collision collision)
        {
            VehicleController vehicle = collision.gameObject.GetComponent<VehicleController>();

            if (vehicle != null)
            {
                HandleVehicleHit(vehicle);
            }
        }

        /// <summary>
        /// Alternative trigger-based collision
        /// </summary>
        private void OnTriggerEnter(Collider other)
        {
            VehicleController vehicle = other.GetComponent<VehicleController>();

            if (vehicle != null)
            {
                HandleVehicleHit(vehicle);
            }
        }

        /// <summary>
        /// Handles vehicle hitting the obstacle
        /// </summary>
        private void HandleVehicleHit(VehicleController vehicle)
        {
            // Check if vehicle is invincible
            if (vehicle.IsInvincible())
            {
                Debug.Log("[Obstacle] Vehicle is invincible - no damage");
                return;
            }

            // Apply obstacle effects
            ApplyObstacleEffects(vehicle);

            // Play hit effects
            PlayHitEffects(vehicle.transform.position);

            // Fire event
            onObstacleHit?.Invoke(vehicle);

            // Handle specific obstacle types
            switch (obstacleType)
            {
                case ObstacleType.Explosive:
                    TriggerExplosion();
                    break;

                case ObstacleType.Breakable:
                    BreakObstacle();
                    break;

                case ObstacleType.Bouncy:
                    ApplyBounceForce(vehicle);
                    break;
            }
        }

        /// <summary>
        /// Applies negative effects to vehicle
        /// </summary>
        private void ApplyObstacleEffects(VehicleController vehicle)
        {
            // Cargo loss
            CargoManager cargoManager = vehicle.GetComponent<CargoManager>();
            if (cargoManager != null && cargoLossPercent > 0)
            {
                int cargoLost = Mathf.CeilToInt(cargoManager.GetCurrentCargo() * cargoLossPercent);
                cargoManager.LoseCargo(cargoLost);

                Debug.Log($"[Obstacle] Vehicle lost {cargoLost} cargo ({cargoLossPercent * 100}%)");
            }

            // Break combo
            if (breaksCombo && ComboSystem.Instance != null)
            {
                ComboSystem.Instance.ResetCombo();
            }

            // Trigger haptic feedback
            if (SettingsManager.Instance != null)
            {
                SettingsManager.Instance.TriggerHaptic(SettingsManager.HapticType.Heavy);
            }

            // Camera shake
            if (CameraController.Instance != null)
            {
                CameraController.Instance.ShakeCamera(0.3f, 0.5f);
            }
        }

        /// <summary>
        /// Plays visual and audio hit effects
        /// </summary>
        private void PlayHitEffects(Vector3 hitPosition)
        {
            // Spawn hit particle effect
            if (hitEffectPrefab != null)
            {
                GameObject effect = Instantiate(hitEffectPrefab, hitPosition, Quaternion.identity);
                Destroy(effect, 2f);
            }

            // Play hit sound
            if (AudioManager.Instance != null && !string.IsNullOrEmpty(hitSoundName))
            {
                AudioManager.Instance.PlaySFX(hitSoundName);
            }
        }

        /// <summary>
        /// Triggers explosion effect
        /// </summary>
        private void TriggerExplosion()
        {
            if (isExploding) return;
            isExploding = true;

            // Spawn explosion visual effect
            if (explosionEffectPrefab != null)
            {
                GameObject explosion = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
                Destroy(explosion, 3f);
            }

            // Play explosion sound
            if (AudioManager.Instance != null && !string.IsNullOrEmpty(explosionSoundName))
            {
                AudioManager.Instance.PlaySFX(explosionSoundName);
            }

            // Apply explosion force to nearby objects
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach (Collider nearbyObject in colliders)
            {
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                }
            }

            // Destroy obstacle after explosion
            Destroy(gameObject, 0.1f);
        }

        /// <summary>
        /// Breaks the obstacle (for breakable obstacles)
        /// </summary>
        private void BreakObstacle()
        {
            Debug.Log("[Obstacle] Obstacle broken!");

            // Spawn break particles
            if (hitEffectPrefab != null)
            {
                GameObject breakEffect = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
                Destroy(breakEffect, 2f);
            }

            // Optionally award points/gold for breaking obstacle
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(10);
            }

            Destroy(gameObject);
        }

        /// <summary>
        /// Applies bounce force to vehicle (for bouncy obstacles)
        /// </summary>
        private void ApplyBounceForce(VehicleController vehicle)
        {
            Rigidbody vehicleRb = vehicle.GetComponent<Rigidbody>();
            if (vehicleRb != null)
            {
                Vector3 bounceDirection = (vehicle.transform.position - transform.position).normalized;
                bounceDirection.y = 0.5f; // Add upward component
                vehicleRb.AddForce(bounceDirection * 500f, ForceMode.Impulse);

                Debug.Log("[Obstacle] Applied bounce force to vehicle");
            }
        }

        /// <summary>
        /// Draws gizmos for obstacle visualization
        /// </summary>
        private void OnDrawGizmosSelected()
        {
            // Draw movement path
            if (isMoving)
            {
                Gizmos.color = Color.yellow;
                Vector3 start = Application.isPlaying ? startPosition : transform.position;
                Vector3 end = start + moveDirection.normalized * moveDistance;
                Gizmos.DrawLine(start, end);
                Gizmos.DrawWireSphere(start, 0.2f);
                Gizmos.DrawWireSphere(end, 0.2f);
            }

            // Draw explosion radius
            if (explodes)
            {
                Gizmos.color = new Color(1f, 0f, 0f, 0.3f);
                Gizmos.DrawWireSphere(transform.position, explosionRadius);
            }
        }
    }

    public enum ObstacleType
    {
        Static,      // Doesn't move
        Moving,      // Moves back and forth
        Rotating,    // Rotates in place
        Explosive,   // Explodes when hit
        Breakable,   // Can be destroyed
        Bouncy,      // Bounces vehicle away
        Hazard       // Environmental hazard (lava, spikes, etc.)
    }
}
