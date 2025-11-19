using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TreasureExcavator
{
    /// <summary>
    /// Manages power-up spawning, collection, and effects.
    /// Power-ups provide temporary gameplay bonuses and add variety to levels.
    /// </summary>
    public class PowerUpSystem : Singleton<PowerUpSystem>
    {
        [Header("Power-Up Prefabs")]
        [SerializeField] private GameObject speedBoostPrefab;
        [SerializeField] private GameObject magnetPrefab;
        [SerializeField] private GameObject doublePointsPrefab;
        [SerializeField] private GameObject invincibilityPrefab;
        [SerializeField] private GameObject instantCapacityPrefab;

        [Header("Events")]
        public UnityEvent<PowerUpType> onPowerUpCollected = new UnityEvent<PowerUpType>();
        public UnityEvent<PowerUpType> onPowerUpExpired = new UnityEvent<PowerUpType>();
        public UnityEvent<PowerUpType, float> onPowerUpTimeRemaining = new UnityEvent<PowerUpType, float>();

        private Dictionary<PowerUpType, PowerUpEffect> activePowerUps = new Dictionary<PowerUpType, PowerUpEffect>();
        private List<GameObject> spawnedPowerUps = new List<GameObject>();

        /// <summary>
        /// Spawns a power-up at a specific location
        /// </summary>
        public GameObject SpawnPowerUp(PowerUpType type, Vector3 position)
        {
            GameObject prefab = GetPowerUpPrefab(type);
            if (prefab == null)
            {
                Debug.LogWarning($"[PowerUpSystem] No prefab assigned for {type}");
                return null;
            }

            GameObject powerUpObject = Instantiate(prefab, position, Quaternion.identity);
            PowerUp powerUpComponent = powerUpObject.GetComponent<PowerUp>();

            if (powerUpComponent == null)
            {
                powerUpComponent = powerUpObject.AddComponent<PowerUp>();
            }

            powerUpComponent.Initialize(type);
            spawnedPowerUps.Add(powerUpObject);

            return powerUpObject;
        }

        /// <summary>
        /// Spawns a random power-up at a location
        /// </summary>
        public GameObject SpawnRandomPowerUp(Vector3 position)
        {
            PowerUpType randomType = (PowerUpType)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(PowerUpType)).Length);
            return SpawnPowerUp(randomType, position);
        }

        /// <summary>
        /// Activates a power-up effect
        /// </summary>
        public void ActivatePowerUp(PowerUpType type, float duration = 10f)
        {
            // If power-up is already active, refresh duration
            if (activePowerUps.ContainsKey(type))
            {
                activePowerUps[type].remainingTime = duration;
                Debug.Log($"[PowerUpSystem] Refreshed {type} duration to {duration}s");
                return;
            }

            // Create new power-up effect
            PowerUpEffect effect = new PowerUpEffect(type, duration);
            activePowerUps[type] = effect;

            // Apply power-up effect
            ApplyPowerUpEffect(type);

            // Start countdown coroutine
            StartCoroutine(PowerUpCountdown(type, duration));

            // Fire event
            onPowerUpCollected?.Invoke(type);

            Debug.Log($"[PowerUpSystem] Activated {type} for {duration}s");
        }

        /// <summary>
        /// Applies the actual power-up effect to game systems
        /// </summary>
        private void ApplyPowerUpEffect(PowerUpType type)
        {
            VehicleController vehicle = FindObjectOfType<VehicleController>();

            switch (type)
            {
                case PowerUpType.SpeedBoost:
                    if (vehicle != null)
                    {
                        vehicle.SetSpeedMultiplier(1.5f);
                    }
                    break;

                case PowerUpType.Magnet:
                    if (vehicle != null)
                    {
                        vehicle.SetCollectionRadius(5f); // Increased from default 1.5
                    }
                    break;

                case PowerUpType.DoublePoints:
                    if (GameManager.Instance != null)
                    {
                        GameManager.Instance.SetScoreMultiplier(2f);
                    }
                    break;

                case PowerUpType.Invincibility:
                    if (vehicle != null)
                    {
                        vehicle.SetInvincible(true);
                    }
                    break;

                case PowerUpType.InstantCapacity:
                    // Instantly fill cargo to max capacity
                    CargoManager cargo = FindObjectOfType<CargoManager>();
                    if (cargo != null)
                    {
                        cargo.FillToCapacity();
                    }
                    break;

                case PowerUpType.TimeFreeze:
                    // Freeze level timer (if applicable)
                    if (GameManager.Instance != null)
                    {
                        GameManager.Instance.FreezeTimer(true);
                    }
                    break;

                case PowerUpType.GoldRush:
                    // Increase gold rewards temporarily
                    if (GameManager.Instance != null)
                    {
                        GameManager.Instance.SetGoldMultiplier(3f);
                    }
                    break;

                case PowerUpType.Shield:
                    if (vehicle != null)
                    {
                        vehicle.EnableShield(true);
                    }
                    break;
            }
        }

        /// <summary>
        /// Removes power-up effect
        /// </summary>
        private void RemovePowerUpEffect(PowerUpType type)
        {
            VehicleController vehicle = FindObjectOfType<VehicleController>();

            switch (type)
            {
                case PowerUpType.SpeedBoost:
                    if (vehicle != null)
                    {
                        vehicle.SetSpeedMultiplier(1f);
                    }
                    break;

                case PowerUpType.Magnet:
                    if (vehicle != null)
                    {
                        vehicle.ResetCollectionRadius();
                    }
                    break;

                case PowerUpType.DoublePoints:
                    if (GameManager.Instance != null)
                    {
                        GameManager.Instance.SetScoreMultiplier(1f);
                    }
                    break;

                case PowerUpType.Invincibility:
                    if (vehicle != null)
                    {
                        vehicle.SetInvincible(false);
                    }
                    break;

                case PowerUpType.TimeFreeze:
                    if (GameManager.Instance != null)
                    {
                        GameManager.Instance.FreezeTimer(false);
                    }
                    break;

                case PowerUpType.GoldRush:
                    if (GameManager.Instance != null)
                    {
                        GameManager.Instance.SetGoldMultiplier(1f);
                    }
                    break;

                case PowerUpType.Shield:
                    if (vehicle != null)
                    {
                        vehicle.EnableShield(false);
                    }
                    break;
            }
        }

        /// <summary>
        /// Countdown timer for power-up duration
        /// </summary>
        private IEnumerator PowerUpCountdown(PowerUpType type, float duration)
        {
            float elapsed = 0f;

            while (elapsed < duration)
            {
                yield return null;
                elapsed += Time.deltaTime;

                if (activePowerUps.ContainsKey(type))
                {
                    activePowerUps[type].remainingTime = duration - elapsed;

                    // Fire time remaining event every second
                    if (Mathf.RoundToInt(elapsed) != Mathf.RoundToInt(elapsed - Time.deltaTime))
                    {
                        onPowerUpTimeRemaining?.Invoke(type, duration - elapsed);
                    }
                }
            }

            // Power-up expired
            DeactivatePowerUp(type);
        }

        /// <summary>
        /// Deactivates a power-up
        /// </summary>
        public void DeactivatePowerUp(PowerUpType type)
        {
            if (!activePowerUps.ContainsKey(type)) return;

            // Remove effect
            RemovePowerUpEffect(type);

            // Remove from active list
            activePowerUps.Remove(type);

            // Fire event
            onPowerUpExpired?.Invoke(type);

            Debug.Log($"[PowerUpSystem] Deactivated {type}");
        }

        /// <summary>
        /// Checks if a power-up is currently active
        /// </summary>
        public bool IsPowerUpActive(PowerUpType type)
        {
            return activePowerUps.ContainsKey(type);
        }

        /// <summary>
        /// Gets remaining time for an active power-up
        /// </summary>
        public float GetPowerUpTimeRemaining(PowerUpType type)
        {
            return activePowerUps.ContainsKey(type) ? activePowerUps[type].remainingTime : 0f;
        }

        /// <summary>
        /// Clears all active power-ups (for level end/restart)
        /// </summary>
        public void ClearAllPowerUps()
        {
            List<PowerUpType> activePowerUpTypes = new List<PowerUpType>(activePowerUps.Keys);

            foreach (PowerUpType type in activePowerUpTypes)
            {
                DeactivatePowerUp(type);
            }

            // Destroy spawned power-up objects
            foreach (GameObject powerUp in spawnedPowerUps)
            {
                if (powerUp != null)
                {
                    Destroy(powerUp);
                }
            }

            spawnedPowerUps.Clear();
        }

        /// <summary>
        /// Gets the prefab for a specific power-up type
        /// </summary>
        private GameObject GetPowerUpPrefab(PowerUpType type)
        {
            switch (type)
            {
                case PowerUpType.SpeedBoost: return speedBoostPrefab;
                case PowerUpType.Magnet: return magnetPrefab;
                case PowerUpType.DoublePoints: return doublePointsPrefab;
                case PowerUpType.Invincibility: return invincibilityPrefab;
                case PowerUpType.InstantCapacity: return instantCapacityPrefab;
                default: return null;
            }
        }

        /// <summary>
        /// Returns info about a power-up type
        /// </summary>
        public PowerUpInfo GetPowerUpInfo(PowerUpType type)
        {
            return new PowerUpInfo(type);
        }
    }

    /// <summary>
    /// Power-up collectible component
    /// </summary>
    public class PowerUp : MonoBehaviour
    {
        private PowerUpType powerUpType;
        private float rotationSpeed = 90f;
        private float floatSpeed = 2f;
        private float floatHeight = 0.3f;
        private Vector3 startPosition;

        public void Initialize(PowerUpType type)
        {
            powerUpType = type;
            startPosition = transform.position;
        }

        private void Update()
        {
            // Rotate power-up
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

            // Float animation
            float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                // Activate power-up
                PowerUpSystem.Instance?.ActivatePowerUp(powerUpType);

                // Play collection effects
                if (AudioManager.Instance != null)
                {
                    AudioManager.Instance.PlaySFX("PowerUpCollect");
                }

                if (SettingsManager.Instance != null)
                {
                    SettingsManager.Instance.TriggerHaptic(SettingsManager.HapticType.Success);
                }

                // Destroy power-up object
                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// Active power-up effect data
    /// </summary>
    public class PowerUpEffect
    {
        public PowerUpType type;
        public float remainingTime;

        public PowerUpEffect(PowerUpType type, float duration)
        {
            this.type = type;
            this.remainingTime = duration;
        }
    }

    /// <summary>
    /// Power-up information for UI display
    /// </summary>
    public class PowerUpInfo
    {
        public PowerUpType type;
        public string name;
        public string description;
        public Color color;

        public PowerUpInfo(PowerUpType type)
        {
            this.type = type;

            switch (type)
            {
                case PowerUpType.SpeedBoost:
                    name = "Speed Boost";
                    description = "Increases vehicle speed by 50%";
                    color = new Color(0.2f, 0.8f, 1f); // Light blue
                    break;

                case PowerUpType.Magnet:
                    name = "Treasure Magnet";
                    description = "Attracts treasures from further away";
                    color = new Color(1f, 0.3f, 0.3f); // Red
                    break;

                case PowerUpType.DoublePoints:
                    name = "Double Points";
                    description = "Doubles all score earned";
                    color = new Color(1f, 0.84f, 0f); // Gold
                    break;

                case PowerUpType.Invincibility:
                    name = "Invincibility";
                    description = "Become immune to obstacles";
                    color = new Color(1f, 1f, 0f); // Yellow
                    break;

                case PowerUpType.InstantCapacity:
                    name = "Instant Cargo";
                    description = "Instantly fills cargo to max capacity";
                    color = new Color(0.5f, 1f, 0.5f); // Light green
                    break;

                case PowerUpType.TimeFreeze:
                    name = "Time Freeze";
                    description = "Stops the level timer";
                    color = new Color(0.6f, 0.8f, 1f); // Ice blue
                    break;

                case PowerUpType.GoldRush:
                    name = "Gold Rush";
                    description = "Triples gold earned";
                    color = new Color(1f, 0.65f, 0f); // Orange
                    break;

                case PowerUpType.Shield:
                    name = "Energy Shield";
                    description = "Absorbs one hit from obstacles";
                    color = new Color(0.3f, 0.7f, 1f); // Blue
                    break;

                default:
                    name = "Unknown";
                    description = "";
                    color = Color.white;
                    break;
            }
        }
    }

    public enum PowerUpType
    {
        SpeedBoost,
        Magnet,
        DoublePoints,
        Invincibility,
        InstantCapacity,
        TimeFreeze,
        GoldRush,
        Shield
    }
}
