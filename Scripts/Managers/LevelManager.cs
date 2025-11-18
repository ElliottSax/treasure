using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

namespace TreasureExcavator
{
    /// <summary>
    /// Manages level data, spawning, and level-specific logic.
    /// Works with GameManager for overall game flow.
    /// Phase 1: Foundation - Level Management
    /// </summary>
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance { get; private set; }

        [Header("Level Configuration")]
        [SerializeField] private LevelData currentLevelData;
        [SerializeField] private LevelData[] allLevels;

        [Header("Spawn References")]
        [SerializeField] private Transform treasureSpawnParent;
        [SerializeField] private Transform gateSpawnParent;
        [SerializeField] private Transform vehicleSpawnPoint;

        [Header("Prefabs")]
        [SerializeField] private GameObject treasureSmallPrefab;
        [SerializeField] private GameObject treasureMediumPrefab;
        [SerializeField] private GameObject treasureLargePrefab;
        [SerializeField] private GameObject gateBronzePrefab;
        [SerializeField] private GameObject gateSilverPrefab;
        [SerializeField] private GameObject gateGoldPrefab;
        [SerializeField] private GameObject gatePlatinumPrefab;

        [Header("Events")]
        public UnityEvent onLevelLoaded;
        public UnityEvent onLevelReady;

        // Runtime data
        private List<GameObject> spawnedObjects = new List<GameObject>();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Start()
        {
            if (currentLevelData != null)
            {
                LoadLevel(currentLevelData);
            }
        }

        /// <summary>
        /// Load a specific level by its data.
        /// </summary>
        public void LoadLevel(LevelData levelData)
        {
            if (levelData == null)
            {
                Debug.LogError("Cannot load null level data!");
                return;
            }

            currentLevelData = levelData;

            // Validate level data
            if (!levelData.IsValid())
            {
                Debug.LogError($"Level {levelData.LevelNumber} has invalid configuration!");
                return;
            }

            // Clear existing objects
            ClearLevel();

            // Configure GameManager
            if (GameManager.Instance != null)
            {
                GameManager.Instance.StartLevel(levelData.LevelNumber);
            }

            // Spawn level elements
            SpawnTreasures();
            SpawnGates();

            // Notify listeners
            onLevelLoaded?.Invoke();

            Debug.Log($"Level loaded: {levelData.LevelName}");
        }

        /// <summary>
        /// Load level by number (1-8).
        /// </summary>
        public void LoadLevel(int levelNumber)
        {
            LevelData levelData = GetLevelData(levelNumber);
            if (levelData != null)
            {
                LoadLevel(levelData);
            }
            else
            {
                Debug.LogError($"Level {levelNumber} data not found!");
            }
        }

        /// <summary>
        /// Spawn treasures according to level data.
        /// </summary>
        private void SpawnTreasures()
        {
            if (currentLevelData == null) return;

            Vector3 spawnCenter = Vector3.zero;
            float spawnRadius = Mathf.Min(currentLevelData.LevelSize.x, currentLevelData.LevelSize.y) / 2f;

            // Spawn small treasures
            for (int i = 0; i < currentLevelData.SmallTreasureCount; i++)
            {
                SpawnTreasure(treasureSmallPrefab, spawnCenter, spawnRadius);
            }

            // Spawn medium treasures
            for (int i = 0; i < currentLevelData.MediumTreasureCount; i++)
            {
                SpawnTreasure(treasureMediumPrefab, spawnCenter, spawnRadius);
            }

            // Spawn large treasures
            for (int i = 0; i < currentLevelData.LargeTreasureCount; i++)
            {
                SpawnTreasure(treasureLargePrefab, spawnCenter, spawnRadius);
            }

            Debug.Log($"Spawned {currentLevelData.GetTotalTreasureCount()} treasures");
        }

        private void SpawnTreasure(GameObject prefab, Vector3 center, float radius)
        {
            if (prefab == null) return;

            Vector3 spawnPos = GetRandomSpawnPosition(center, radius);
            GameObject treasure = Instantiate(prefab, spawnPos, Quaternion.identity);

            if (treasureSpawnParent != null)
                treasure.transform.SetParent(treasureSpawnParent);

            spawnedObjects.Add(treasure);
        }

        /// <summary>
        /// Spawn gates according to level data.
        /// </summary>
        private void SpawnGates()
        {
            if (currentLevelData == null) return;

            Vector3 spawnCenter = Vector3.zero;
            float spawnRadius = Mathf.Min(currentLevelData.LevelSize.x, currentLevelData.LevelSize.y) / 2f;

            // Spawn bronze gates
            for (int i = 0; i < currentLevelData.BronzeGateCount; i++)
            {
                SpawnGate(gateBronzePrefab, spawnCenter, spawnRadius);
            }

            // Spawn silver gates
            for (int i = 0; i < currentLevelData.SilverGateCount; i++)
            {
                SpawnGate(gateSilverPrefab, spawnCenter, spawnRadius);
            }

            // Spawn gold gates
            for (int i = 0; i < currentLevelData.GoldGateCount; i++)
            {
                SpawnGate(gateGoldPrefab, spawnCenter, spawnRadius);
            }

            // Spawn platinum gates
            for (int i = 0; i < currentLevelData.PlatinumGateCount; i++)
            {
                SpawnGate(gatePlatinumPrefab, spawnCenter, spawnRadius);
            }

            Debug.Log($"Spawned {currentLevelData.GetTotalGateCount()} gates");
        }

        private void SpawnGate(GameObject prefab, Vector3 center, float radius)
        {
            if (prefab == null) return;

            Vector3 spawnPos = GetRandomSpawnPosition(center, radius);
            GameObject gate = Instantiate(prefab, spawnPos, Quaternion.identity);

            if (gateSpawnParent != null)
                gate.transform.SetParent(gateSpawnParent);

            spawnedObjects.Add(gate);
        }

        private Vector3 GetRandomSpawnPosition(Vector3 center, float radius)
        {
            Vector2 randomCircle = Random.insideUnitCircle * radius;
            return center + new Vector3(randomCircle.x, 0f, randomCircle.y);
        }

        /// <summary>
        /// Clear all spawned objects from the level.
        /// </summary>
        public void ClearLevel()
        {
            foreach (GameObject obj in spawnedObjects)
            {
                if (obj != null)
                    Destroy(obj);
            }

            spawnedObjects.Clear();
        }

        /// <summary>
        /// Get level data by number.
        /// </summary>
        public LevelData GetLevelData(int levelNumber)
        {
            if (allLevels == null || allLevels.Length == 0)
                return null;

            foreach (LevelData level in allLevels)
            {
                if (level != null && level.LevelNumber == levelNumber)
                    return level;
            }

            return null;
        }

        /// <summary>
        /// Get current level data.
        /// </summary>
        public LevelData GetCurrentLevelData() => currentLevelData;

        /// <summary>
        /// Get vehicle spawn point.
        /// </summary>
        public Vector3 GetVehicleSpawnPosition()
        {
            return vehicleSpawnPoint != null ? vehicleSpawnPoint.position : Vector3.zero;
        }

        /// <summary>
        /// Restart current level.
        /// </summary>
        public void RestartLevel()
        {
            if (currentLevelData != null)
            {
                LoadLevel(currentLevelData);
            }
        }
    }
}
