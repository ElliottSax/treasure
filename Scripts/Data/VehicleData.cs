using UnityEngine;

namespace TreasureExcavator
{
    /// <summary>
    /// ScriptableObject defining a vehicle's stats and characteristics.
    /// Phase 1: Foundation - Data Structures
    /// </summary>
    [CreateAssetMenu(fileName = "VehicleData", menuName = "TreasureExcavator/Vehicle Data", order = 2)]
    public class VehicleData : ScriptableObject
    {
        [Header("Vehicle Info")]
        [SerializeField] private string vehicleName = "Starter Bulldozer";
        [SerializeField, TextArea] private string description = "A reliable starter vehicle for your treasure hunting adventures.";
        [SerializeField] private Sprite icon;

        [Header("Stats")]
        [SerializeField] private float speed = 8f;
        [SerializeField] private float acceleration = 5f;
        [SerializeField] private float turningSpeed = 180f;
        [SerializeField] private int cargoCapacity = 10;
        [SerializeField] private float collectionRadius = 1.5f;

        [Header("Special Ability")]
        [SerializeField] private VehicleAbilityType abilityType = VehicleAbilityType.None;
        [SerializeField, TextArea] private string abilityDescription = "No special ability.";

        [Header("Unlock Requirements")]
        [SerializeField] private UnlockType unlockType = UnlockType.Default;
        [SerializeField] private int unlockLevel = 0;
        [SerializeField] private int unlockGoldCost = 0;
        [SerializeField] private int requiredStars = 0;

        [Header("Visuals")]
        [SerializeField] private GameObject vehiclePrefab;
        [SerializeField] private Color primaryColor = Color.yellow;
        [SerializeField] private Color secondaryColor = Color.black;

        [Header("Audio")]
        [SerializeField] private AudioClip engineSound;
        [SerializeField] private float enginePitchMin = 0.8f;
        [SerializeField] private float enginePitchMax = 1.5f;

        public enum VehicleAbilityType
        {
            None,
            AutoCollect,    // Dual-Scoop Loader
            NitroBoost,     // Nitro Hauler
            GateMagnet      // Mega Vault Truck
        }

        public enum UnlockType
        {
            Default,        // Available from start
            LevelComplete,  // Unlock by completing a level
            Purchase,       // Unlock with gold
            Stars           // Unlock by getting stars
        }

        // Public getters
        public string VehicleName => vehicleName;
        public string Description => description;
        public Sprite Icon => icon;
        public float Speed => speed;
        public float Acceleration => acceleration;
        public float TurningSpeed => turningSpeed;
        public int CargoCapacity => cargoCapacity;
        public float CollectionRadius => collectionRadius;
        public VehicleAbilityType AbilityType => abilityType;
        public string AbilityDescription => abilityDescription;
        public UnlockType UnlockRequirement => unlockType;
        public int UnlockLevel => unlockLevel;
        public int UnlockGoldCost => unlockGoldCost;
        public int RequiredStars => requiredStars;
        public GameObject VehiclePrefab => vehiclePrefab;
        public Color PrimaryColor => primaryColor;
        public Color SecondaryColor => secondaryColor;
        public AudioClip EngineSound => engineSound;
        public float EnginePitchMin => enginePitchMin;
        public float EnginePitchMax => enginePitchMax;

        /// <summary>
        /// Check if this vehicle is unlocked.
        /// </summary>
        public bool IsUnlocked()
        {
            switch (unlockType)
            {
                case UnlockType.Default:
                    return true;

                case UnlockType.LevelComplete:
                    return GameManager.Instance != null &&
                           GameManager.Instance.IsLevelUnlocked(unlockLevel + 1);

                case UnlockType.Purchase:
                    return PlayerPrefs.GetInt($"Vehicle_{vehicleName}_Unlocked", 0) == 1;

                case UnlockType.Stars:
                    int totalStars = GetTotalStarsEarned();
                    return totalStars >= requiredStars;

                default:
                    return false;
            }
        }

        /// <summary>
        /// Unlock this vehicle (for purchase or achievement unlocks).
        /// </summary>
        public void Unlock()
        {
            PlayerPrefs.SetInt($"Vehicle_{vehicleName}_Unlocked", 1);
            PlayerPrefs.Save();
            Debug.Log($"Vehicle unlocked: {vehicleName}");
        }

        /// <summary>
        /// Check if player can afford to purchase this vehicle.
        /// </summary>
        public bool CanAfford()
        {
            if (unlockType != UnlockType.Purchase) return false;
            return GameManager.Instance != null &&
                   GameManager.Instance.GetGold() >= unlockGoldCost;
        }

        /// <summary>
        /// Purchase this vehicle with gold.
        /// </summary>
        public bool Purchase()
        {
            if (unlockType != UnlockType.Purchase) return false;
            if (!CanAfford()) return false;

            if (GameManager.Instance.SpendGold(unlockGoldCost))
            {
                Unlock();
                return true;
            }

            return false;
        }

        private int GetTotalStarsEarned()
        {
            int total = 0;
            for (int i = 1; i <= 8; i++) // 8 levels in MVP
            {
                total += GameManager.Instance?.GetLevelStars(i) ?? 0;
            }
            return total;
        }

        /// <summary>
        /// Get unlock status text for UI.
        /// </summary>
        public string GetUnlockStatusText()
        {
            if (IsUnlocked())
                return "UNLOCKED";

            switch (unlockType)
            {
                case UnlockType.LevelComplete:
                    return $"Complete Level {unlockLevel}";

                case UnlockType.Purchase:
                    return $"{unlockGoldCost} Gold";

                case UnlockType.Stars:
                    int current = GetTotalStarsEarned();
                    return $"{current}/{requiredStars} Stars";

                default:
                    return "LOCKED";
            }
        }

        #if UNITY_EDITOR
        private void OnValidate()
        {
            // Ensure values are reasonable
            speed = Mathf.Max(1f, speed);
            acceleration = Mathf.Max(0.1f, acceleration);
            turningSpeed = Mathf.Max(10f, turningSpeed);
            cargoCapacity = Mathf.Max(1, cargoCapacity);
            collectionRadius = Mathf.Max(0.5f, collectionRadius);
            unlockGoldCost = Mathf.Max(0, unlockGoldCost);
        }
        #endif
    }
}
