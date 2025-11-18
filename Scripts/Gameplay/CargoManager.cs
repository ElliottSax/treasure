using UnityEngine;
using UnityEngine.Events;

namespace TreasureExcavator.Gameplay
{
    /// <summary>
    /// Manages treasure cargo for a vehicle.
    /// Handles collection, storage, and capacity limits.
    /// Phase 1: Foundation - Week 2
    /// </summary>
    public class CargoManager : MonoBehaviour
    {
        [Header("Cargo Settings")]
        [SerializeField] private int maxCapacity = 10;
        [SerializeField] private float collectionRadius = 1.5f;

        [Header("Visual Feedback")]
        [SerializeField] private GameObject cargoVisual;
        [SerializeField] private Transform cargoFillParent;

        [Header("Events")]
        public UnityEvent<int> onCargoChanged;
        public UnityEvent onCargoFull;
        public UnityEvent onCargoEmpty;

        // State
        private int currentCargo = 0;
        private int totalValueCollected = 0;

        // Sphere collider for collection radius
        private SphereCollider collectionTrigger;

        private void Awake()
        {
            // Create collection trigger
            collectionTrigger = gameObject.AddComponent<SphereCollider>();
            collectionTrigger.isTrigger = true;
            collectionTrigger.radius = collectionRadius;
        }

        private void Start()
        {
            UpdateCargoVisual();
        }

        public bool CanCollect()
        {
            return currentCargo < maxCapacity;
        }

        public void AddTreasure(Treasure treasure)
        {
            if (!CanCollect())
            {
                Debug.LogWarning("Cargo is full! Cannot collect more treasures.");
                return;
            }

            currentCargo++;
            totalValueCollected += treasure.GetValue();

            // Invoke events
            onCargoChanged?.Invoke(currentCargo);

            if (currentCargo >= maxCapacity)
            {
                onCargoFull?.Invoke();
            }

            // Update visual representation
            UpdateCargoVisual();

            Debug.Log($"Treasure collected! Cargo: {currentCargo}/{maxCapacity}, Value: {treasure.GetValue()}");
        }

        public int DepositCargo(int multiplier = 1)
        {
            if (currentCargo == 0)
            {
                Debug.LogWarning("No cargo to deposit!");
                return 0;
            }

            // Calculate score from cargo
            int score = totalValueCollected * multiplier;

            // Clear cargo
            int depositedAmount = currentCargo;
            currentCargo = 0;
            totalValueCollected = 0;

            // Invoke events
            onCargoChanged?.Invoke(currentCargo);
            onCargoEmpty?.Invoke();

            // Update visual
            UpdateCargoVisual();

            Debug.Log($"Deposited {depositedAmount} treasures for {score} points (x{multiplier} multiplier)");

            return score;
        }

        public void ApplyMultiplier(int multiplier)
        {
            if (currentCargo == 0)
            {
                Debug.LogWarning("No cargo to multiply!");
                return;
            }

            // Multiply total cargo value
            int oldValue = totalValueCollected;
            totalValueCollected *= multiplier;

            Debug.Log($"Cargo multiplied by x{multiplier}! Value: {oldValue} → {totalValueCollected}");

            // Visual/audio feedback
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayPassGate(multiplier);
            }
        }

        private void UpdateCargoVisual()
        {
            if (cargoVisual == null) return;

            // Scale cargo visual based on fill percentage
            float fillPercentage = (float)currentCargo / maxCapacity;
            cargoVisual.transform.localScale = Vector3.one * fillPercentage;

            // Show/hide based on cargo amount
            cargoVisual.SetActive(currentCargo > 0);
        }

        // Public getters
        public int GetCurrentCargo() => currentCargo;
        public int GetMaxCapacity() => maxCapacity;
        public int GetTotalValue() => totalValueCollected;
        public float GetFillPercentage() => (float)currentCargo / maxCapacity;
        public bool IsFull() => currentCargo >= maxCapacity;
        public bool IsEmpty() => currentCargo == 0;

        // For vehicle upgrades
        public void SetMaxCapacity(int newCapacity)
        {
            maxCapacity = newCapacity;
            onCargoChanged?.Invoke(currentCargo);
        }

        public void SetCollectionRadius(float newRadius)
        {
            collectionRadius = newRadius;
            if (collectionTrigger != null)
            {
                collectionTrigger.radius = collectionRadius;
            }
        }

        // Debug visualization
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = IsFull() ? Color.red : Color.green;
            Gizmos.DrawWireSphere(transform.position, collectionRadius);
        }
    }
}
