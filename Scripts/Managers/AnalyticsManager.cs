using System.Collections.Generic;
using UnityEngine;

namespace TreasureExcavator
{
    /// <summary>
    /// Analytics wrapper for tracking game events and player behavior.
    /// Provides abstraction layer for Firebase Analytics, Unity Analytics, or custom solutions.
    /// </summary>
    public class AnalyticsManager : Singleton<AnalyticsManager>
    {
        [Header("Settings")]
        [SerializeField] private bool enableAnalytics = true;
        [SerializeField] private bool enableDebugLogging = true;

        protected override void Awake()
        {
            base.Awake();

            if (enableAnalytics)
            {
                InitializeAnalytics();
            }
        }

        /// <summary>
        /// Initializes analytics services
        /// </summary>
        private void InitializeAnalytics()
        {
            // TODO: Initialize Firebase Analytics or Unity Analytics
            // FirebaseAnalytics.Initialize();

            Debug.Log("[AnalyticsManager] Analytics initialized");

            // Track app open
            TrackEvent("app_open");
        }

        /// <summary>
        /// Tracks a custom event
        /// </summary>
        public void TrackEvent(string eventName, Dictionary<string, object> parameters = null)
        {
            if (!enableAnalytics) return;

            if (enableDebugLogging)
            {
                string paramString = parameters != null ? $" with {parameters.Count} parameters" : "";
                Debug.Log($"[Analytics] Event: {eventName}{paramString}");
            }

            // TODO: Send to Firebase Analytics
            // FirebaseAnalytics.LogEvent(eventName, parameters);

            // TODO: Send to Unity Analytics
            // Analytics.CustomEvent(eventName, parameters);
        }

        // --- Game Flow Events ---

        public void TrackLevelStart(int levelNumber)
        {
            TrackEvent("level_start", new Dictionary<string, object>
            {
                { "level_number", levelNumber },
                { "vehicle_used", SaveManager.Instance?.GetSelectedVehicle() ?? "Unknown" }
            });
        }

        public void TrackLevelComplete(int levelNumber, int stars, int score, float timeSeconds)
        {
            TrackEvent("level_complete", new Dictionary<string, object>
            {
                { "level_number", levelNumber },
                { "stars_earned", stars },
                { "score", score },
                { "time_seconds", timeSeconds },
                { "vehicle_used", SaveManager.Instance?.GetSelectedVehicle() ?? "Unknown" }
            });
        }

        public void TrackLevelFail(int levelNumber, int score, float timeSeconds, string failReason)
        {
            TrackEvent("level_fail", new Dictionary<string, object>
            {
                { "level_number", levelNumber },
                { "score", score },
                { "time_seconds", timeSeconds },
                { "fail_reason", failReason }
            });
        }

        // --- Progression Events ---

        public void TrackTreasureCollected(string treasureType, int value)
        {
            TrackEvent("treasure_collected", new Dictionary<string, object>
            {
                { "treasure_type", treasureType },
                { "value", value }
            });
        }

        public void TrackGateUsed(string gateType, int multiplier, int cargoValue)
        {
            TrackEvent("gate_used", new Dictionary<string, object>
            {
                { "gate_type", gateType },
                { "multiplier", multiplier },
                { "cargo_value", cargoValue }
            });
        }

        public void TrackComboAchieved(int comboCount, float multiplier)
        {
            TrackEvent("combo_achieved", new Dictionary<string, object>
            {
                { "combo_count", comboCount },
                { "multiplier", multiplier }
            });
        }

        public void TrackAchievementUnlocked(string achievementId, int goldReward)
        {
            TrackEvent("achievement_unlocked", new Dictionary<string, object>
            {
                { "achievement_id", achievementId },
                { "gold_reward", goldReward }
            });
        }

        // --- Monetization Events ---

        public void TrackPurchase(string itemId, string itemType, string currency, float price)
        {
            TrackEvent("purchase", new Dictionary<string, object>
            {
                { "item_id", itemId },
                { "item_type", itemType },
                { "currency", currency },
                { "price", price }
            });
        }

        public void TrackIAPPurchase(string productId, float revenue, string currency)
        {
            TrackEvent("iap_purchase", new Dictionary<string, object>
            {
                { "product_id", productId },
                { "revenue", revenue },
                { "currency", currency }
            });
        }

        public void TrackAdShown(string adType, string placement)
        {
            TrackEvent("ad_shown", new Dictionary<string, object>
            {
                { "ad_type", adType },
                { "placement", placement }
            });
        }

        public void TrackAdClicked(string adType, string placement)
        {
            TrackEvent("ad_clicked", new Dictionary<string, object>
            {
                { "ad_type", adType },
                { "placement", placement }
            });
        }

        public void TrackRewardedAdComplete(string placement, string rewardType, int rewardAmount)
        {
            TrackEvent("rewarded_ad_complete", new Dictionary<string, object>
            {
                { "placement", placement },
                { "reward_type", rewardType },
                { "reward_amount", rewardAmount }
            });
        }

        // --- Power-Up Events ---

        public void TrackPowerUpCollected(string powerUpType)
        {
            TrackEvent("powerup_collected", new Dictionary<string, object>
            {
                { "powerup_type", powerUpType }
            });
        }

        public void TrackPowerUpUsed(string powerUpType, float duration)
        {
            TrackEvent("powerup_used", new Dictionary<string, object>
            {
                { "powerup_type", powerUpType },
                { "duration", duration }
            });
        }

        // --- Vehicle Events ---

        public void TrackVehicleUnlocked(string vehicleName, string unlockMethod)
        {
            TrackEvent("vehicle_unlocked", new Dictionary<string, object>
            {
                { "vehicle_name", vehicleName },
                { "unlock_method", unlockMethod }
            });
        }

        public void TrackVehicleSelected(string vehicleName)
        {
            TrackEvent("vehicle_selected", new Dictionary<string, object>
            {
                { "vehicle_name", vehicleName }
            });
        }

        // --- UI Events ---

        public void TrackScreenView(string screenName)
        {
            TrackEvent("screen_view", new Dictionary<string, object>
            {
                { "screen_name", screenName }
            });
        }

        public void TrackButtonClick(string buttonName, string screenName)
        {
            TrackEvent("button_click", new Dictionary<string, object>
            {
                { "button_name", buttonName },
                { "screen_name", screenName }
            });
        }

        public void TrackTutorialStep(int stepNumber, string stepName)
        {
            TrackEvent("tutorial_step", new Dictionary<string, object>
            {
                { "step_number", stepNumber },
                { "step_name", stepName }
            });
        }

        public void TrackTutorialComplete()
        {
            TrackEvent("tutorial_complete");
        }

        // --- Social Events ---

        public void TrackShareScore(int score, string platform)
        {
            TrackEvent("share_score", new Dictionary<string, object>
            {
                { "score", score },
                { "platform", platform }
            });
        }

        public void TrackRateApp(int rating)
        {
            TrackEvent("rate_app", new Dictionary<string, object>
            {
                { "rating", rating }
            });
        }

        // --- Error/Technical Events ---

        public void TrackError(string errorType, string errorMessage, string stackTrace = "")
        {
            TrackEvent("error", new Dictionary<string, object>
            {
                { "error_type", errorType },
                { "error_message", errorMessage },
                { "stack_trace", stackTrace }
            });
        }

        public void TrackPerformance(string metricName, float value)
        {
            TrackEvent("performance", new Dictionary<string, object>
            {
                { "metric_name", metricName },
                { "value", value }
            });
        }

        // --- Session Events ---

        public void TrackSessionStart()
        {
            TrackEvent("session_start", new Dictionary<string, object>
            {
                { "total_playtime", SaveManager.Instance?.GetSaveData().totalPlayTime ?? 0f }
            });
        }

        public void TrackSessionEnd(float sessionDuration)
        {
            TrackEvent("session_end", new Dictionary<string, object>
            {
                { "session_duration", sessionDuration }
            });
        }

        // --- Daily Rewards Events ---

        public void TrackDailyRewardClaimed(int day, string rewardType, int rewardAmount)
        {
            TrackEvent("daily_reward_claimed", new Dictionary<string, object>
            {
                { "day", day },
                { "reward_type", rewardType },
                { "reward_amount", rewardAmount }
            });
        }

        public void TrackStreakBroken(int previousStreak)
        {
            TrackEvent("streak_broken", new Dictionary<string, object>
            {
                { "previous_streak", previousStreak }
            });
        }

        // --- Retention Events ---

        public void TrackRetentionDay(int daysSinceInstall)
        {
            TrackEvent("retention", new Dictionary<string, object>
            {
                { "days_since_install", daysSinceInstall }
            });
        }

        // --- User Properties ---

        public void SetUserProperty(string propertyName, string value)
        {
            if (!enableAnalytics) return;

            if (enableDebugLogging)
            {
                Debug.Log($"[Analytics] User Property: {propertyName} = {value}");
            }

            // TODO: Set Firebase user property
            // FirebaseAnalytics.SetUserProperty(propertyName, value);
        }

        public void SetPlayerLevel(int level)
        {
            SetUserProperty("player_level", level.ToString());
        }

        public void SetTotalGold(int gold)
        {
            SetUserProperty("total_gold", gold.ToString());
        }

        public void SetVehicleOwned(int vehicleCount)
        {
            SetUserProperty("vehicles_owned", vehicleCount.ToString());
        }

        // --- Funnel Analysis ---

        public void TrackFunnelStep(string funnelName, int step, string stepName)
        {
            TrackEvent($"funnel_{funnelName}", new Dictionary<string, object>
            {
                { "step", step },
                { "step_name", stepName }
            });
        }

        // --- Debug Methods ---

        [ContextMenu("Test Analytics Event")]
        public void TestAnalyticsEvent()
        {
            TrackEvent("test_event", new Dictionary<string, object>
            {
                { "test_param_1", "value1" },
                { "test_param_2", 42 },
                { "test_param_3", true }
            });
        }

        /// <summary>
        /// Enables or disables analytics tracking
        /// </summary>
        public void SetAnalyticsEnabled(bool enabled)
        {
            enableAnalytics = enabled;
            Debug.Log($"[AnalyticsManager] Analytics {(enabled ? "enabled" : "disabled")}");
        }

        /// <summary>
        /// Enables or disables debug logging
        /// </summary>
        public void SetDebugLogging(bool enabled)
        {
            enableDebugLogging = enabled;
        }
    }
}
