using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace TreasureExcavator
{
    /// <summary>
    /// Manages achievement tracking, unlocking, and rewards.
    /// Integrates with SaveManager and provides achievement progress UI data.
    /// </summary>
    public class AchievementManager : Singleton<AchievementManager>
    {
        [Header("Achievement Database")]
        [SerializeField] private List<AchievementData> allAchievements = new List<AchievementData>();

        [Header("Events")]
        public UnityEvent<AchievementData> onAchievementUnlocked = new UnityEvent<AchievementData>();
        public UnityEvent<AchievementData, int> onAchievementProgress = new UnityEvent<AchievementData, int>();

        private Dictionary<string, AchievementData> achievementDatabase;
        private Dictionary<string, int> achievementProgress; // Current progress for each achievement

        protected override void Awake()
        {
            base.Awake();
            InitializeAchievements();
            LoadAchievementProgress();
        }

        private void OnEnable()
        {
            // Subscribe to game events
            SubscribeToGameEvents();
        }

        private void OnDisable()
        {
            UnsubscribeFromGameEvents();
        }

        /// <summary>
        /// Initializes the achievement database
        /// </summary>
        private void InitializeAchievements()
        {
            achievementDatabase = new Dictionary<string, AchievementData>();
            achievementProgress = new Dictionary<string, int>();

            // If no achievements set in inspector, create default set
            if (allAchievements.Count == 0)
            {
                CreateDefaultAchievements();
            }

            // Populate dictionary
            foreach (var achievement in allAchievements)
            {
                if (!achievementDatabase.ContainsKey(achievement.id))
                {
                    achievementDatabase[achievement.id] = achievement;
                    achievementProgress[achievement.id] = 0;
                }
            }
        }

        /// <summary>
        /// Creates default achievement set
        /// </summary>
        private void CreateDefaultAchievements()
        {
            allAchievements = new List<AchievementData>
            {
                // Tutorial & First Steps
                new AchievementData("first_treasure", "First Haul", "Collect your first treasure",
                    AchievementType.CollectTreasures, 1, 10),

                new AchievementData("first_gate", "Gate Crasher", "Pass through your first multiplier gate",
                    AchievementType.UseGates, 1, 10),

                new AchievementData("first_level", "Getting Started", "Complete your first level",
                    AchievementType.CompleteLevels, 1, 25),

                // Collection Achievements
                new AchievementData("treasure_hunter", "Treasure Hunter", "Collect 100 treasures",
                    AchievementType.CollectTreasures, 100, 50),

                new AchievementData("treasure_hoarder", "Treasure Hoarder", "Collect 500 treasures",
                    AchievementType.CollectTreasures, 500, 100),

                new AchievementData("treasure_tycoon", "Treasure Tycoon", "Collect 1,000 treasures",
                    AchievementType.CollectTreasures, 1000, 200),

                // Gate Achievements
                new AchievementData("gate_novice", "Gate Novice", "Use 50 multiplier gates",
                    AchievementType.UseGates, 50, 50),

                new AchievementData("gate_master", "Gate Master", "Use 200 multiplier gates",
                    AchievementType.UseGates, 200, 100),

                new AchievementData("multiplier_king", "Multiplier King", "Use a x10 gate with full cargo",
                    AchievementType.Special, 1, 150),

                // Level Completion
                new AchievementData("level_clearer", "Level Clearer", "Complete 10 levels",
                    AchievementType.CompleteLevels, 10, 75),

                new AchievementData("star_collector", "Star Collector", "Earn 15 stars total",
                    AchievementType.EarnStars, 15, 100),

                new AchievementData("perfectionist", "Perfectionist", "Get 3 stars on 5 different levels",
                    AchievementType.ThreeStarLevels, 5, 200),

                // Score Achievements
                new AchievementData("high_scorer", "High Scorer", "Score 10,000 points in a single level",
                    AchievementType.ScoreInLevel, 10000, 100),

                new AchievementData("score_master", "Score Master", "Reach 100,000 total score",
                    AchievementType.TotalScore, 100000, 150),

                // Combo Achievements
                new AchievementData("combo_starter", "Combo Starter", "Achieve a 5x combo",
                    AchievementType.AchieveCombo, 5, 50),

                new AchievementData("combo_expert", "Combo Expert", "Achieve a 10x combo",
                    AchievementType.AchieveCombo, 10, 100),

                new AchievementData("combo_master", "Combo Master", "Achieve a 20x combo",
                    AchievementType.AchieveCombo, 20, 200),

                // Vehicle Achievements
                new AchievementData("vehicle_collector", "Vehicle Collector", "Unlock all 4 vehicles",
                    AchievementType.UnlockVehicles, 4, 150),

                new AchievementData("big_spender", "Big Spender", "Spend 5,000 gold in the shop",
                    AchievementType.SpendGold, 5000, 100),

                // Special/Hidden Achievements
                new AchievementData("speed_demon", "Speed Demon", "Complete a level in under 60 seconds",
                    AchievementType.Special, 1, 150),

                new AchievementData("no_gates", "Pure Collector", "Complete a level without using any gates",
                    AchievementType.Special, 1, 100),

                new AchievementData("dedicated_player", "Dedicated Player", "Play for 7 consecutive days",
                    AchievementType.ConsecutiveLogins, 7, 250),
            };
        }

        /// <summary>
        /// Loads achievement progress from SaveManager
        /// </summary>
        private void LoadAchievementProgress()
        {
            if (SaveManager.Instance == null) return;

            List<string> unlockedAchievements = SaveManager.Instance.GetSaveData().unlockedAchievements;

            foreach (var achievementId in unlockedAchievements)
            {
                if (achievementDatabase.ContainsKey(achievementId))
                {
                    achievementDatabase[achievementId].isUnlocked = true;
                }
            }
        }

        /// <summary>
        /// Subscribes to relevant game events for achievement tracking
        /// </summary>
        private void SubscribeToGameEvents()
        {
            // These would connect to actual game events
            // Example: GameManager.Instance.onLevelComplete += OnLevelComplete;
        }

        private void UnsubscribeFromGameEvents()
        {
            // Cleanup event subscriptions
        }

        /// <summary>
        /// Updates achievement progress and checks for unlock
        /// </summary>
        public void UpdateAchievementProgress(string achievementId, int incrementAmount = 1)
        {
            if (!achievementDatabase.ContainsKey(achievementId)) return;

            AchievementData achievement = achievementDatabase[achievementId];

            // Skip if already unlocked
            if (achievement.isUnlocked) return;

            // Update progress
            achievementProgress[achievementId] += incrementAmount;
            int currentProgress = achievementProgress[achievementId];

            // Fire progress event
            onAchievementProgress?.Invoke(achievement, currentProgress);

            // Check if achievement is now complete
            if (currentProgress >= achievement.targetValue)
            {
                UnlockAchievement(achievementId);
            }
        }

        /// <summary>
        /// Directly sets achievement progress to a specific value
        /// </summary>
        public void SetAchievementProgress(string achievementId, int value)
        {
            if (!achievementDatabase.ContainsKey(achievementId)) return;

            AchievementData achievement = achievementDatabase[achievementId];
            if (achievement.isUnlocked) return;

            achievementProgress[achievementId] = value;
            onAchievementProgress?.Invoke(achievement, value);

            if (value >= achievement.targetValue)
            {
                UnlockAchievement(achievementId);
            }
        }

        /// <summary>
        /// Unlocks an achievement and grants rewards
        /// </summary>
        private void UnlockAchievement(string achievementId)
        {
            if (!achievementDatabase.ContainsKey(achievementId)) return;

            AchievementData achievement = achievementDatabase[achievementId];
            if (achievement.isUnlocked) return;

            // Mark as unlocked
            achievement.isUnlocked = true;
            achievement.unlockDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // Save to persistent storage
            if (SaveManager.Instance != null)
            {
                SaveManager.Instance.GetSaveData().unlockedAchievements.Add(achievementId);
                SaveManager.Instance.AddGold(achievement.goldReward);
                SaveManager.Instance.SaveGame();
            }

            // Fire unlock event
            onAchievementUnlocked?.Invoke(achievement);

            Debug.Log($"[AchievementManager] Achievement Unlocked: {achievement.title} (+{achievement.goldReward} gold)");
        }

        /// <summary>
        /// Gets current progress for an achievement
        /// </summary>
        public int GetAchievementProgress(string achievementId)
        {
            return achievementProgress.ContainsKey(achievementId) ? achievementProgress[achievementId] : 0;
        }

        /// <summary>
        /// Gets achievement progress as a percentage (0-1)
        /// </summary>
        public float GetAchievementProgressPercent(string achievementId)
        {
            if (!achievementDatabase.ContainsKey(achievementId)) return 0f;

            AchievementData achievement = achievementDatabase[achievementId];
            int currentProgress = GetAchievementProgress(achievementId);

            return Mathf.Clamp01((float)currentProgress / achievement.targetValue);
        }

        /// <summary>
        /// Returns all achievements
        /// </summary>
        public List<AchievementData> GetAllAchievements()
        {
            return allAchievements;
        }

        /// <summary>
        /// Returns unlocked achievements only
        /// </summary>
        public List<AchievementData> GetUnlockedAchievements()
        {
            return allAchievements.Where(a => a.isUnlocked).ToList();
        }

        /// <summary>
        /// Returns locked achievements only
        /// </summary>
        public List<AchievementData> GetLockedAchievements()
        {
            return allAchievements.Where(a => !a.isUnlocked).ToList();
        }

        /// <summary>
        /// Returns total gold earned from achievements
        /// </summary>
        public int GetTotalAchievementGold()
        {
            return allAchievements.Where(a => a.isUnlocked).Sum(a => a.goldReward);
        }

        /// <summary>
        /// Returns achievement completion percentage
        /// </summary>
        public float GetCompletionPercentage()
        {
            if (allAchievements.Count == 0) return 0f;
            int unlockedCount = allAchievements.Count(a => a.isUnlocked);
            return (float)unlockedCount / allAchievements.Count * 100f;
        }

        // --- Convenience Methods for Common Achievement Updates ---

        public void OnTreasureCollected(int count = 1)
        {
            UpdateAchievementProgress("first_treasure", count);
            UpdateAchievementProgress("treasure_hunter", count);
            UpdateAchievementProgress("treasure_hoarder", count);
            UpdateAchievementProgress("treasure_tycoon", count);
        }

        public void OnGateUsed(int count = 1)
        {
            UpdateAchievementProgress("first_gate", count);
            UpdateAchievementProgress("gate_novice", count);
            UpdateAchievementProgress("gate_master", count);
        }

        public void OnLevelComplete(int stars)
        {
            UpdateAchievementProgress("first_level");
            UpdateAchievementProgress("level_clearer");
            UpdateAchievementProgress("star_collector", stars);

            if (stars == 3)
            {
                UpdateAchievementProgress("perfectionist");
            }
        }

        public void OnScoreEarned(int levelScore, int totalScore)
        {
            SetAchievementProgress("high_scorer", levelScore);
            SetAchievementProgress("score_master", totalScore);
        }

        public void OnComboAchieved(int comboCount)
        {
            SetAchievementProgress("combo_starter", comboCount);
            SetAchievementProgress("combo_expert", comboCount);
            SetAchievementProgress("combo_master", comboCount);
        }

        public void OnVehicleUnlocked(int totalVehicles)
        {
            SetAchievementProgress("vehicle_collector", totalVehicles);
        }

        public void OnGoldSpent(int amount)
        {
            UpdateAchievementProgress("big_spender", amount);
        }
    }

    /// <summary>
    /// Achievement data structure
    /// </summary>
    [Serializable]
    public class AchievementData
    {
        public string id;
        public string title;
        public string description;
        public AchievementType type;
        public int targetValue;
        public int goldReward;
        public bool isUnlocked;
        public bool isHidden; // Hidden until unlocked
        public string iconName; // Reference to achievement icon
        public string unlockDate;

        public AchievementData(string id, string title, string description,
            AchievementType type, int targetValue, int goldReward, bool isHidden = false)
        {
            this.id = id;
            this.title = title;
            this.description = description;
            this.type = type;
            this.targetValue = targetValue;
            this.goldReward = goldReward;
            this.isHidden = isHidden;
            this.isUnlocked = false;
            this.iconName = id; // Default icon name matches ID
            this.unlockDate = "";
        }
    }

    public enum AchievementType
    {
        CollectTreasures,
        UseGates,
        CompleteLevels,
        EarnStars,
        ThreeStarLevels,
        ScoreInLevel,
        TotalScore,
        AchieveCombo,
        UnlockVehicles,
        SpendGold,
        ConsecutiveLogins,
        Special
    }
}
