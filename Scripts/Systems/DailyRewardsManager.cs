using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TreasureExcavator
{
    /// <summary>
    /// Manages daily login rewards to encourage player retention.
    /// Tracks consecutive login days and provides escalating rewards.
    /// </summary>
    public class DailyRewardsManager : Singleton<DailyRewardsManager>
    {
        [Header("Reward Schedule")]
        [SerializeField] private List<DailyReward> rewardSchedule = new List<DailyReward>();

        [Header("Settings")]
        [SerializeField] private int maxConsecutiveDays = 7; // Reset after 7 days
        [SerializeField] private bool loopRewards = true; // Loop back to day 1 after max days

        [Header("Events")]
        public UnityEvent<DailyReward> onRewardAvailable = new UnityEvent<DailyReward>();
        public UnityEvent<DailyReward> onRewardClaimed = new UnityEvent<DailyReward>();
        public UnityEvent<int> onStreakBroken = new UnityEvent<int>();
        public UnityEvent<int> onStreakMilestone = new UnityEvent<int>();

        private bool hasCheckedTodayReward = false;

        protected override void Awake()
        {
            base.Awake();
            InitializeRewards();
        }

        private void Start()
        {
            CheckDailyReward();
        }

        /// <summary>
        /// Initializes daily reward schedule
        /// </summary>
        private void InitializeRewards()
        {
            // Create default reward schedule if none assigned
            if (rewardSchedule.Count == 0)
            {
                CreateDefaultRewardSchedule();
            }
        }

        /// <summary>
        /// Creates default 7-day reward schedule
        /// </summary>
        private void CreateDefaultRewardSchedule()
        {
            rewardSchedule = new List<DailyReward>
            {
                // Day 1: Small reward
                new DailyReward
                {
                    day = 1,
                    rewardType = RewardType.Gold,
                    goldAmount = 100,
                    description = "Welcome back! Here's some gold to get started."
                },

                // Day 2: Power-up
                new DailyReward
                {
                    day = 2,
                    rewardType = RewardType.PowerUp,
                    powerUpType = "SpeedBoost",
                    powerUpQuantity = 1,
                    description = "Speed Boost power-up to help you race!"
                },

                // Day 3: More gold
                new DailyReward
                {
                    day = 3,
                    rewardType = RewardType.Gold,
                    goldAmount = 200,
                    description = "You're on a roll! Have some more gold."
                },

                // Day 4: Multiple power-ups
                new DailyReward
                {
                    day = 4,
                    rewardType = RewardType.PowerUp,
                    powerUpType = "Magnet",
                    powerUpQuantity = 2,
                    description = "2x Magnet power-ups for treasure hunting!"
                },

                // Day 5: Big gold bonus
                new DailyReward
                {
                    day = 5,
                    rewardType = RewardType.Gold,
                    goldAmount = 500,
                    description = "5 days strong! Big gold bonus for you!"
                },

                // Day 6: Premium currency
                new DailyReward
                {
                    day = 6,
                    rewardType = RewardType.Gems,
                    gemsAmount = 50,
                    description = "Almost there! Have some premium gems."
                },

                // Day 7: MEGA reward
                new DailyReward
                {
                    day = 7,
                    rewardType = RewardType.Bundle,
                    goldAmount = 1000,
                    gemsAmount = 100,
                    powerUpType = "DoublePoints",
                    powerUpQuantity = 3,
                    description = "7 DAY STREAK! Mega bundle with gold, gems, and power-ups!"
                }
            };
        }

        /// <summary>
        /// Checks if daily reward is available
        /// </summary>
        public void CheckDailyReward()
        {
            if (hasCheckedTodayReward) return;

            hasCheckedTodayReward = true;

            if (SaveManager.Instance == null) return;

            GameSaveData saveData = SaveManager.Instance.GetSaveData();
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            string lastLogin = saveData.lastLoginDate;

            // First time playing
            if (string.IsNullOrEmpty(lastLogin))
            {
                StartNewStreak();
                ShowRewardAvailable();
                return;
            }

            // Already claimed today
            if (lastLogin == today)
            {
                Debug.Log("[DailyRewards] Reward already claimed today");
                return;
            }

            // Check if streak continues
            DateTime lastLoginDate = DateTime.Parse(lastLogin);
            DateTime currentDate = DateTime.Now.Date;
            TimeSpan timeSinceLastLogin = currentDate - lastLoginDate;

            if (timeSinceLastLogin.Days == 1)
            {
                // Consecutive day - increment streak
                ContinueStreak();
                ShowRewardAvailable();
            }
            else if (timeSinceLastLogin.Days > 1)
            {
                // Streak broken - reset
                int previousStreak = saveData.consecutiveLoginDays;
                onStreakBroken?.Invoke(previousStreak);
                StartNewStreak();
                ShowRewardAvailable();

                Debug.Log($"[DailyRewards] Streak broken after {previousStreak} days. Starting fresh!");
            }
        }

        /// <summary>
        /// Starts a new login streak
        /// </summary>
        private void StartNewStreak()
        {
            if (SaveManager.Instance == null) return;

            GameSaveData saveData = SaveManager.Instance.GetSaveData();
            saveData.consecutiveLoginDays = 1;
            SaveManager.Instance.SaveGame();

            Debug.Log("[DailyRewards] Started new streak");
        }

        /// <summary>
        /// Continues existing login streak
        /// </summary>
        private void ContinueStreak()
        {
            if (SaveManager.Instance == null) return;

            GameSaveData saveData = SaveManager.Instance.GetSaveData();
            saveData.consecutiveLoginDays++;

            // Check for looping
            if (loopRewards && saveData.consecutiveLoginDays > maxConsecutiveDays)
            {
                saveData.consecutiveLoginDays = 1;
                Debug.Log("[DailyRewards] Streak looped back to day 1");
            }

            SaveManager.Instance.SaveGame();

            // Check for milestone achievements
            CheckStreakMilestones(saveData.consecutiveLoginDays);

            Debug.Log($"[DailyRewards] Streak continued: Day {saveData.consecutiveLoginDays}");
        }

        /// <summary>
        /// Shows reward available notification
        /// </summary>
        private void ShowRewardAvailable()
        {
            DailyReward reward = GetTodayReward();
            if (reward != null)
            {
                onRewardAvailable?.Invoke(reward);
            }
        }

        /// <summary>
        /// Claims today's reward
        /// </summary>
        public bool ClaimDailyReward()
        {
            if (SaveManager.Instance == null) return false;

            GameSaveData saveData = SaveManager.Instance.GetSaveData();
            string today = DateTime.Now.ToString("yyyy-MM-dd");

            // Check if already claimed today
            if (saveData.lastLoginDate == today)
            {
                Debug.LogWarning("[DailyRewards] Reward already claimed today!");
                return false;
            }

            // Get today's reward
            DailyReward reward = GetTodayReward();
            if (reward == null)
            {
                Debug.LogWarning("[DailyRewards] No reward available for today!");
                return false;
            }

            // Grant rewards
            GrantReward(reward);

            // Update last login date
            saveData.lastLoginDate = today;
            SaveManager.Instance.SaveGame();

            // Fire event
            onRewardClaimed?.Invoke(reward);

            Debug.Log($"[DailyRewards] Claimed Day {reward.day} reward!");

            return true;
        }

        /// <summary>
        /// Gets today's reward based on consecutive login days
        /// </summary>
        public DailyReward GetTodayReward()
        {
            if (SaveManager.Instance == null) return null;

            int currentDay = SaveManager.Instance.GetSaveData().consecutiveLoginDays;

            // Clamp to valid range
            currentDay = Mathf.Clamp(currentDay, 1, rewardSchedule.Count);

            // Find reward for current day
            return rewardSchedule.Find(r => r.day == currentDay);
        }

        /// <summary>
        /// Grants reward to player
        /// </summary>
        private void GrantReward(DailyReward reward)
        {
            switch (reward.rewardType)
            {
                case RewardType.Gold:
                    SaveManager.Instance.AddGold(reward.goldAmount);
                    Debug.Log($"[DailyRewards] Granted {reward.goldAmount} gold");
                    break;

                case RewardType.Gems:
                    SaveManager.Instance.GetSaveData().totalGems += reward.gemsAmount;
                    SaveManager.Instance.SaveGame();
                    Debug.Log($"[DailyRewards] Granted {reward.gemsAmount} gems");
                    break;

                case RewardType.PowerUp:
                    GrantPowerUp(reward.powerUpType, reward.powerUpQuantity);
                    break;

                case RewardType.Bundle:
                    // Grant all rewards in bundle
                    if (reward.goldAmount > 0)
                        SaveManager.Instance.AddGold(reward.goldAmount);
                    if (reward.gemsAmount > 0)
                    {
                        SaveManager.Instance.GetSaveData().totalGems += reward.gemsAmount;
                        SaveManager.Instance.SaveGame();
                    }
                    if (!string.IsNullOrEmpty(reward.powerUpType))
                        GrantPowerUp(reward.powerUpType, reward.powerUpQuantity);
                    break;
            }
        }

        /// <summary>
        /// Grants power-up to player inventory
        /// </summary>
        private void GrantPowerUp(string powerUpType, int quantity)
        {
            // Store in PlayerPrefs for now (should be integrated with inventory system)
            int currentCount = PlayerPrefs.GetInt($"PowerUp_{powerUpType}", 0);
            PlayerPrefs.SetInt($"PowerUp_{powerUpType}", currentCount + quantity);
            PlayerPrefs.Save();

            Debug.Log($"[DailyRewards] Granted {quantity}x {powerUpType} power-up");
        }

        /// <summary>
        /// Checks for streak milestones and fires events
        /// </summary>
        private void CheckStreakMilestones(int currentStreak)
        {
            // Fire events for milestone days
            if (currentStreak == 3 || currentStreak == 7 || currentStreak == 14 ||
                currentStreak == 30 || currentStreak == 100)
            {
                onStreakMilestone?.Invoke(currentStreak);

                // Update achievements
                if (AchievementManager.Instance != null && currentStreak >= 7)
                {
                    AchievementManager.Instance.UpdateAchievementProgress("dedicated_player");
                }
            }
        }

        /// <summary>
        /// Gets current consecutive login days
        /// </summary>
        public int GetCurrentStreak()
        {
            if (SaveManager.Instance == null) return 0;
            return SaveManager.Instance.GetSaveData().consecutiveLoginDays;
        }

        /// <summary>
        /// Checks if reward can be claimed today
        /// </summary>
        public bool CanClaimToday()
        {
            if (SaveManager.Instance == null) return false;

            string today = DateTime.Now.ToString("yyyy-MM-dd");
            string lastLogin = SaveManager.Instance.GetSaveData().lastLoginDate;

            return lastLogin != today;
        }

        /// <summary>
        /// Gets all rewards in schedule
        /// </summary>
        public List<DailyReward> GetRewardSchedule()
        {
            return rewardSchedule;
        }

        /// <summary>
        /// Gets time until next reward is available
        /// </summary>
        public TimeSpan GetTimeUntilNextReward()
        {
            DateTime now = DateTime.Now;
            DateTime tomorrow = now.Date.AddDays(1);
            return tomorrow - now;
        }

        /// <summary>
        /// Resets daily rewards (for testing)
        /// </summary>
        [ContextMenu("Reset Daily Rewards (Testing)")]
        public void ResetDailyRewards()
        {
            if (SaveManager.Instance == null) return;

            GameSaveData saveData = SaveManager.Instance.GetSaveData();
            saveData.lastLoginDate = "";
            saveData.consecutiveLoginDays = 0;
            SaveManager.Instance.SaveGame();

            hasCheckedTodayReward = false;

            Debug.Log("[DailyRewards] Daily rewards reset for testing");
        }
    }

    /// <summary>
    /// Daily reward data structure
    /// </summary>
    [Serializable]
    public class DailyReward
    {
        public int day;
        public RewardType rewardType;
        public int goldAmount;
        public int gemsAmount;
        public string powerUpType;
        public int powerUpQuantity;
        public string description;
        public string iconName;

        public string GetRewardSummary()
        {
            switch (rewardType)
            {
                case RewardType.Gold:
                    return $"{goldAmount} Gold";
                case RewardType.Gems:
                    return $"{gemsAmount} Gems";
                case RewardType.PowerUp:
                    return $"{powerUpQuantity}x {powerUpType}";
                case RewardType.Bundle:
                    return $"{goldAmount} Gold + {gemsAmount} Gems + {powerUpQuantity}x {powerUpType}";
                default:
                    return "Mystery Reward";
            }
        }
    }

    public enum RewardType
    {
        Gold,
        Gems,
        PowerUp,
        Bundle
    }
}
