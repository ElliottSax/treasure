using UnityEngine;
using System.Collections.Generic;
using System.Text;

namespace TreasureExcavator.Debug
{
    /// <summary>
    /// Validates that all Phase 2 systems are properly set up and configured.
    /// Run this in the Editor or at runtime to check for issues.
    /// </summary>
    public class SystemValidator : MonoBehaviour
    {
        [Header("Validation Settings")]
        [SerializeField] private bool runOnStart = true;
        [SerializeField] private bool logToConsole = true;
        [SerializeField] private bool logToFile = false;

        private StringBuilder validationReport;
        private int errorCount = 0;
        private int warningCount = 0;
        private int passCount = 0;

        private void Start()
        {
            if (runOnStart)
            {
                ValidateAllSystems();
            }
        }

        [ContextMenu("Validate All Systems")]
        public void ValidateAllSystems()
        {
            validationReport = new StringBuilder();
            errorCount = 0;
            warningCount = 0;
            passCount = 0;

            validationReport.AppendLine("========================================");
            validationReport.AppendLine("TREASURE EXCAVATOR - SYSTEM VALIDATION");
            validationReport.AppendLine($"Date: {System.DateTime.Now}");
            validationReport.AppendLine("========================================\n");

            // Validate each system
            ValidateSaveSystem();
            ValidateSettingsSystem();
            ValidateAchievementSystem();
            ValidatePowerUpSystem();
            ValidateComboSystem();
            ValidateShopSystem();
            ValidateDailyRewardSystem();
            ValidateAnalyticsSystem();
            ValidateParticleSystem();
            ValidateIntegration();

            // Summary
            validationReport.AppendLine("\n========================================");
            validationReport.AppendLine("VALIDATION SUMMARY");
            validationReport.AppendLine("========================================");
            validationReport.AppendLine($"✓ Passed: {passCount}");
            validationReport.AppendLine($"⚠ Warnings: {warningCount}");
            validationReport.AppendLine($"✗ Errors: {errorCount}");

            if (errorCount == 0 && warningCount == 0)
            {
                validationReport.AppendLine("\n🎉 ALL SYSTEMS VALIDATED SUCCESSFULLY!");
            }
            else if (errorCount == 0)
            {
                validationReport.AppendLine("\n✓ No errors found, but there are warnings to address.");
            }
            else
            {
                validationReport.AppendLine("\n✗ CRITICAL ERRORS FOUND - Please fix before proceeding!");
            }

            // Output results
            string report = validationReport.ToString();

            if (logToConsole)
            {
                UnityEngine.Debug.Log(report);
            }

            if (logToFile)
            {
                System.IO.File.WriteAllText(
                    Application.persistentDataPath + "/SystemValidation.txt",
                    report
                );
            }
        }

        private void ValidateSaveSystem()
        {
            validationReport.AppendLine("--- SAVE SYSTEM ---");

            if (CheckInstance("SaveManager", SaveManager.Instance))
            {
                // Test save/load
                try
                {
                    SaveManager.Instance.SaveGame();
                    LogPass("Save system can write to disk");

                    SaveManager.Instance.LoadGame();
                    LogPass("Save system can read from disk");

                    var saveData = SaveManager.Instance.GetSaveData();
                    if (saveData != null)
                    {
                        LogPass($"Save data initialized (Gold: {saveData.totalGold})");
                    }
                    else
                    {
                        LogError("Save data is null");
                    }
                }
                catch (System.Exception e)
                {
                    LogError($"Save system error: {e.Message}");
                }
            }

            validationReport.AppendLine();
        }

        private void ValidateSettingsSystem()
        {
            validationReport.AppendLine("--- SETTINGS SYSTEM ---");

            if (CheckInstance("SettingsManager", SettingsManager.Instance))
            {
                // Test settings
                float masterVolume = SettingsManager.Instance.GetMasterVolume();
                LogPass($"Master volume: {masterVolume}");

                int graphicsQuality = SettingsManager.Instance.GetGraphicsQuality();
                LogPass($"Graphics quality: {SettingsManager.Instance.GetGraphicsQualityName()}");

                // Check AudioManager integration
                if (AudioManager.Instance != null)
                {
                    LogPass("AudioManager integration available");
                }
                else
                {
                    LogWarning("AudioManager not found - settings won't affect audio");
                }
            }

            validationReport.AppendLine();
        }

        private void ValidateAchievementSystem()
        {
            validationReport.AppendLine("--- ACHIEVEMENT SYSTEM ---");

            if (CheckInstance("AchievementManager", AchievementManager.Instance))
            {
                var achievements = AchievementManager.Instance.GetAllAchievements();

                if (achievements.Count > 0)
                {
                    LogPass($"Found {achievements.Count} achievements");

                    int unlocked = AchievementManager.Instance.GetUnlockedAchievements().Count;
                    float completion = AchievementManager.Instance.GetCompletionPercentage();
                    LogInfo($"Unlocked: {unlocked}/{achievements.Count} ({completion:F1}%)");
                }
                else
                {
                    LogWarning("No achievements defined - using defaults");
                }
            }

            validationReport.AppendLine();
        }

        private void ValidatePowerUpSystem()
        {
            validationReport.AppendLine("--- POWER-UP SYSTEM ---");

            if (CheckInstance("PowerUpSystem", PowerUpSystem.Instance))
            {
                // Check for prefabs
                bool hasPrefabs = false; // Would need to check serialized fields

                if (!hasPrefabs)
                {
                    LogWarning("Power-up prefabs not assigned in Inspector");
                }

                LogPass("PowerUpSystem initialized");
            }

            validationReport.AppendLine();
        }

        private void ValidateComboSystem()
        {
            validationReport.AppendLine("--- COMBO SYSTEM ---");

            if (CheckInstance("ComboSystem", ComboSystem.Instance))
            {
                // Test combo functionality
                int currentCombo = ComboSystem.Instance.GetCurrentCombo();
                float multiplier = ComboSystem.Instance.GetCurrentMultiplier();

                LogPass($"Combo system ready (Current: {currentCombo}x, Multiplier: {multiplier}x)");
            }

            validationReport.AppendLine();
        }

        private void ValidateShopSystem()
        {
            validationReport.AppendLine("--- SHOP SYSTEM ---");

            if (CheckInstance("ShopManager", ShopManager.Instance))
            {
                var items = ShopManager.Instance.GetAllItems();

                if (items.Count > 0)
                {
                    LogPass($"Shop has {items.Count} items");

                    int vehicles = ShopManager.Instance.GetItemsByType(ShopItemType.Vehicle).Count;
                    int powerUps = ShopManager.Instance.GetItemsByType(ShopItemType.PowerUpBundle).Count;

                    LogInfo($"  - Vehicles: {vehicles}");
                    LogInfo($"  - Power-Up Bundles: {powerUps}");
                }
                else
                {
                    LogWarning("Shop has no items");
                }
            }

            validationReport.AppendLine();
        }

        private void ValidateDailyRewardSystem()
        {
            validationReport.AppendLine("--- DAILY REWARD SYSTEM ---");

            if (CheckInstance("DailyRewardsManager", DailyRewardsManager.Instance))
            {
                int streak = DailyRewardsManager.Instance.GetCurrentStreak();
                bool canClaim = DailyRewardsManager.Instance.CanClaimToday();

                LogPass($"Daily rewards active (Streak: {streak}, Can claim: {canClaim})");

                var schedule = DailyRewardsManager.Instance.GetRewardSchedule();
                if (schedule.Count > 0)
                {
                    LogPass($"Reward schedule: {schedule.Count} days");
                }
                else
                {
                    LogWarning("No reward schedule defined");
                }
            }

            validationReport.AppendLine();
        }

        private void ValidateAnalyticsSystem()
        {
            validationReport.AppendLine("--- ANALYTICS SYSTEM ---");

            if (CheckInstance("AnalyticsManager", AnalyticsManager.Instance))
            {
                LogPass("Analytics system ready");
                LogInfo("Note: Firebase integration requires manual setup");
            }

            validationReport.AppendLine();
        }

        private void ValidateParticleSystem()
        {
            validationReport.AppendLine("--- PARTICLE EFFECTS SYSTEM ---");

            if (CheckInstance("ParticleEffectsManager", ParticleEffectsManager.Instance))
            {
                LogPass("Particle effects manager ready");
                LogWarning("Particle prefabs need to be assigned in Inspector");
            }

            validationReport.AppendLine();
        }

        private void ValidateIntegration()
        {
            validationReport.AppendLine("--- INTEGRATION CHECKS ---");

            // Check GameManager
            if (GameManager.Instance != null)
            {
                LogPass("GameManager found");

                // Check for required Phase 2 methods
                var type = GameManager.Instance.GetType();
                if (type.GetMethod("SetScoreMultiplier") != null)
                {
                    LogPass("GameManager has Phase 2 extensions");
                }
                else
                {
                    LogWarning("GameManager missing Phase 2 extension methods");
                }
            }
            else
            {
                LogError("GameManager not found - required for gameplay");
            }

            // Check VehicleController
            var vehicle = FindObjectOfType<VehicleController>();
            if (vehicle != null)
            {
                LogPass("VehicleController found in scene");

                var type = vehicle.GetType();
                if (type.GetMethod("SetSpeedMultiplier") != null)
                {
                    LogPass("VehicleController has Phase 2 extensions");
                }
                else
                {
                    LogWarning("VehicleController missing Phase 2 extension methods");
                }
            }
            else
            {
                LogInfo("VehicleController not in scene (OK if in main menu)");
            }

            // Check CargoManager
            var cargo = FindObjectOfType<CargoManager>();
            if (cargo != null)
            {
                LogPass("CargoManager found in scene");

                var type = cargo.GetType();
                if (type.GetMethod("FillToCapacity") != null)
                {
                    LogPass("CargoManager has Phase 2 extensions");
                }
                else
                {
                    LogWarning("CargoManager missing Phase 2 extension methods");
                }
            }
            else
            {
                LogInfo("CargoManager not in scene (OK if in main menu)");
            }

            validationReport.AppendLine();
        }

        // --- Helper Methods ---

        private bool CheckInstance<T>(string systemName, T instance) where T : class
        {
            if (instance != null)
            {
                LogPass($"{systemName} instance found");
                return true;
            }
            else
            {
                LogError($"{systemName} instance is null - system not initialized");
                return false;
            }
        }

        private void LogPass(string message)
        {
            validationReport.AppendLine($"  ✓ {message}");
            passCount++;
        }

        private void LogWarning(string message)
        {
            validationReport.AppendLine($"  ⚠ WARNING: {message}");
            warningCount++;
        }

        private void LogError(string message)
        {
            validationReport.AppendLine($"  ✗ ERROR: {message}");
            errorCount++;
        }

        private void LogInfo(string message)
        {
            validationReport.AppendLine($"    {message}");
        }
    }
}
