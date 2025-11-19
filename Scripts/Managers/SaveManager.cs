using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

namespace TreasureExcavator
{
    /// <summary>
    /// Manages game save data with JSON serialization and encryption support.
    /// Provides centralized save/load functionality with automatic backup and cloud sync preparation.
    /// </summary>
    public class SaveManager : Singleton<SaveManager>
    {
        [Header("Save Settings")]
        [SerializeField] private bool useEncryption = true;
        [SerializeField] private bool createBackups = true;
        [SerializeField] private int maxBackups = 3;

        private const string SAVE_FILE_NAME = "gamedata.sav";
        private const string BACKUP_EXTENSION = ".backup";
        private const string ENCRYPTION_KEY = "TreasureExcavator2024Key"; // In production, use more secure key management

        private GameSaveData currentSaveData;
        private string savePath;

        protected override void Awake()
        {
            base.Awake();
            savePath = Path.Combine(Application.persistentDataPath, SAVE_FILE_NAME);
            LoadGame();
        }

        /// <summary>
        /// Saves the current game state to disk
        /// </summary>
        public void SaveGame()
        {
            try
            {
                // Create backup before saving
                if (createBackups && File.Exists(savePath))
                {
                    CreateBackup();
                }

                // Serialize save data
                string json = JsonUtility.ToJson(currentSaveData, true);

                // Encrypt if enabled
                if (useEncryption)
                {
                    json = EncryptString(json);
                }

                // Write to file
                File.WriteAllText(savePath, json);

                Debug.Log($"[SaveManager] Game saved successfully to {savePath}");
            }
            catch (Exception e)
            {
                Debug.LogError($"[SaveManager] Failed to save game: {e.Message}");
            }
        }

        /// <summary>
        /// Loads game state from disk
        /// </summary>
        public void LoadGame()
        {
            try
            {
                if (File.Exists(savePath))
                {
                    string json = File.ReadAllText(savePath);

                    // Decrypt if enabled
                    if (useEncryption)
                    {
                        json = DecryptString(json);
                    }

                    currentSaveData = JsonUtility.FromJson<GameSaveData>(json);
                    Debug.Log($"[SaveManager] Game loaded successfully from {savePath}");
                }
                else
                {
                    // Create new save data
                    currentSaveData = new GameSaveData();
                    SaveGame();
                    Debug.Log("[SaveManager] No save file found, created new save data");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"[SaveManager] Failed to load game: {e.Message}");

                // Try to restore from backup
                if (RestoreFromBackup())
                {
                    Debug.Log("[SaveManager] Restored from backup successfully");
                }
                else
                {
                    // Create fresh save data
                    currentSaveData = new GameSaveData();
                    SaveGame();
                }
            }
        }

        /// <summary>
        /// Deletes all save data (use with caution!)
        /// </summary>
        public void DeleteSaveData()
        {
            if (File.Exists(savePath))
            {
                File.Delete(savePath);
            }

            currentSaveData = new GameSaveData();
            SaveGame();
            Debug.Log("[SaveManager] Save data deleted");
        }

        /// <summary>
        /// Creates a backup of the current save file
        /// </summary>
        private void CreateBackup()
        {
            string backupPath = savePath + BACKUP_EXTENSION + DateTime.Now.ToString("yyyyMMddHHmmss");
            File.Copy(savePath, backupPath, true);

            // Delete old backups
            CleanupOldBackups();
        }

        /// <summary>
        /// Removes old backup files exceeding maxBackups
        /// </summary>
        private void CleanupOldBackups()
        {
            string directory = Path.GetDirectoryName(savePath);
            string[] backups = Directory.GetFiles(directory, $"*{BACKUP_EXTENSION}*");

            if (backups.Length > maxBackups)
            {
                Array.Sort(backups);
                for (int i = 0; i < backups.Length - maxBackups; i++)
                {
                    File.Delete(backups[i]);
                }
            }
        }

        /// <summary>
        /// Attempts to restore save data from the most recent backup
        /// </summary>
        private bool RestoreFromBackup()
        {
            try
            {
                string directory = Path.GetDirectoryName(savePath);
                string[] backups = Directory.GetFiles(directory, $"*{BACKUP_EXTENSION}*");

                if (backups.Length > 0)
                {
                    Array.Sort(backups);
                    string mostRecentBackup = backups[backups.Length - 1];

                    File.Copy(mostRecentBackup, savePath, true);
                    LoadGame();
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"[SaveManager] Failed to restore from backup: {e.Message}");
            }

            return false;
        }

        /// <summary>
        /// Simple XOR encryption for save data (not cryptographically secure, but prevents casual cheating)
        /// </summary>
        private string EncryptString(string text)
        {
            char[] buffer = text.ToCharArray();
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = (char)(buffer[i] ^ ENCRYPTION_KEY[i % ENCRYPTION_KEY.Length]);
            }
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(buffer));
        }

        /// <summary>
        /// Decrypts XOR encrypted string
        /// </summary>
        private string DecryptString(string encryptedText)
        {
            byte[] buffer = Convert.FromBase64String(encryptedText);
            char[] chars = System.Text.Encoding.UTF8.GetString(buffer).ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                chars[i] = (char)(chars[i] ^ ENCRYPTION_KEY[i % ENCRYPTION_KEY.Length]);
            }

            return new string(chars);
        }

        // --- Public Accessors for Game Data ---

        public GameSaveData GetSaveData() => currentSaveData;

        public void UnlockLevel(int levelNumber)
        {
            if (!currentSaveData.unlockedLevels.Contains(levelNumber))
            {
                currentSaveData.unlockedLevels.Add(levelNumber);
                SaveGame();
            }
        }

        public bool IsLevelUnlocked(int levelNumber)
        {
            return currentSaveData.unlockedLevels.Contains(levelNumber);
        }

        public void SetLevelStars(int levelNumber, int stars)
        {
            if (stars > GetLevelStars(levelNumber))
            {
                currentSaveData.levelStars[levelNumber] = stars;
                SaveGame();
            }
        }

        public int GetLevelStars(int levelNumber)
        {
            return currentSaveData.levelStars.ContainsKey(levelNumber)
                ? currentSaveData.levelStars[levelNumber] : 0;
        }

        public void AddGold(int amount)
        {
            currentSaveData.totalGold += amount;
            SaveGame();
        }

        public bool SpendGold(int amount)
        {
            if (currentSaveData.totalGold >= amount)
            {
                currentSaveData.totalGold -= amount;
                SaveGame();
                return true;
            }
            return false;
        }

        public int GetGold() => currentSaveData.totalGold;

        public void UnlockVehicle(string vehicleName)
        {
            if (!currentSaveData.unlockedVehicles.Contains(vehicleName))
            {
                currentSaveData.unlockedVehicles.Add(vehicleName);
                SaveGame();
            }
        }

        public bool IsVehicleUnlocked(string vehicleName)
        {
            return currentSaveData.unlockedVehicles.Contains(vehicleName);
        }

        public void SetSelectedVehicle(string vehicleName)
        {
            currentSaveData.selectedVehicle = vehicleName;
            SaveGame();
        }

        public string GetSelectedVehicle() => currentSaveData.selectedVehicle;

        public void UpdateStatistics(int scoreEarned, int treasuresCollected, int gatesUsed)
        {
            currentSaveData.totalScore += scoreEarned;
            currentSaveData.totalTreasuresCollected += treasuresCollected;
            currentSaveData.totalGatesUsed += gatesUsed;
            currentSaveData.totalGamesPlayed++;
            SaveGame();
        }
    }

    /// <summary>
    /// Main save data structure containing all persistent game data
    /// </summary>
    [Serializable]
    public class GameSaveData
    {
        // Progression
        public List<int> unlockedLevels = new List<int> { 1 }; // Level 1 unlocked by default
        public SerializableDictionary<int, int> levelStars = new SerializableDictionary<int, int>();
        public int currentLevel = 1;

        // Currency
        public int totalGold = 0;
        public int totalGems = 0; // Premium currency for future use

        // Vehicles
        public List<string> unlockedVehicles = new List<string> { "Starter Bulldozer" };
        public string selectedVehicle = "Starter Bulldozer";

        // Statistics
        public int totalScore = 0;
        public int totalTreasuresCollected = 0;
        public int totalGatesUsed = 0;
        public int totalGamesPlayed = 0;
        public int highestCombo = 0;

        // Achievements
        public List<string> unlockedAchievements = new List<string>();

        // Settings (stored in save for cloud sync)
        public float masterVolume = 1f;
        public float musicVolume = 0.7f;
        public float sfxVolume = 1f;
        public int graphicsQuality = 2; // 0=Low, 1=Medium, 2=High
        public bool enableVibration = true;

        // Daily Rewards
        public string lastLoginDate = "";
        public int consecutiveLoginDays = 0;

        // Timestamps
        public string firstPlayDate = DateTime.Now.ToString("yyyy-MM-dd");
        public string lastPlayDate = DateTime.Now.ToString("yyyy-MM-dd");
        public float totalPlayTime = 0f; // In seconds
    }

    /// <summary>
    /// Serializable dictionary wrapper for Unity's JsonUtility
    /// </summary>
    [Serializable]
    public class SerializableDictionary<TKey, TValue>
    {
        [SerializeField] private List<TKey> keys = new List<TKey>();
        [SerializeField] private List<TValue> values = new List<TValue>();

        public TValue this[TKey key]
        {
            get
            {
                int index = keys.IndexOf(key);
                return index >= 0 ? values[index] : default(TValue);
            }
            set
            {
                int index = keys.IndexOf(key);
                if (index >= 0)
                {
                    values[index] = value;
                }
                else
                {
                    keys.Add(key);
                    values.Add(value);
                }
            }
        }

        public bool ContainsKey(TKey key) => keys.Contains(key);

        public void Add(TKey key, TValue value)
        {
            if (!keys.Contains(key))
            {
                keys.Add(key);
                values.Add(value);
            }
        }

        public void Remove(TKey key)
        {
            int index = keys.IndexOf(key);
            if (index >= 0)
            {
                keys.RemoveAt(index);
                values.RemoveAt(index);
            }
        }

        public void Clear()
        {
            keys.Clear();
            values.Clear();
        }
    }
}
