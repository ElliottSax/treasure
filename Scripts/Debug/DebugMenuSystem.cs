using UnityEngine;
using System.Collections.Generic;

namespace TreasureExcavator.Debug
{
    /// <summary>
    /// In-game debug menu for testing all Phase 2 systems.
    /// Accessible by pressing F1 (desktop) or three-finger tap (mobile).
    /// </summary>
    public class DebugMenuSystem : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private bool enableDebugMenu = true;
        [SerializeField] private KeyCode toggleKey = KeyCode.F1;

        private bool menuVisible = false;
        private Vector2 scrollPosition = Vector2.zero;
        private GUIStyle buttonStyle;
        private GUIStyle labelStyle;
        private GUIStyle boxStyle;

        private void Start()
        {
#if !UNITY_EDITOR && !DEVELOPMENT_BUILD
            // Disable in production builds
            enableDebugMenu = false;
#endif
        }

        private void Update()
        {
            if (!enableDebugMenu) return;

            // Toggle with F1 key
            if (Input.GetKeyDown(toggleKey))
            {
                menuVisible = !menuVisible;
            }

            // Three-finger tap for mobile
            if (Input.touchCount == 3)
            {
                menuVisible = !menuVisible;
            }
        }

        private void OnGUI()
        {
            if (!enableDebugMenu || !menuVisible) return;

            InitializeStyles();

            // Main debug window
            GUILayout.BeginArea(new Rect(10, 10, 400, Screen.height - 20));
            GUILayout.BeginVertical(boxStyle);

            GUILayout.Label("=== DEBUG MENU ===", labelStyle);
            GUILayout.Space(10);

            scrollPosition = GUILayout.BeginScrollView(scrollPosition);

            DrawSaveSystemTests();
            DrawAchievementTests();
            DrawPowerUpTests();
            DrawComboTests();
            DrawShopTests();
            DrawDailyRewardTests();
            DrawCurrencyTests();
            DrawLevelTests();
            DrawAnalyticsTests();

            GUILayout.EndScrollView();

            if (GUILayout.Button("Close Debug Menu", buttonStyle))
            {
                menuVisible = false;
            }

            GUILayout.EndVertical();
            GUILayout.EndArea();
        }

        private void InitializeStyles()
        {
            if (buttonStyle == null)
            {
                buttonStyle = new GUIStyle(GUI.skin.button);
                buttonStyle.fontSize = 12;
                buttonStyle.padding = new RectOffset(10, 10, 5, 5);
            }

            if (labelStyle == null)
            {
                labelStyle = new GUIStyle(GUI.skin.label);
                labelStyle.fontSize = 14;
                labelStyle.fontStyle = FontStyle.Bold;
                labelStyle.alignment = TextAnchor.MiddleCenter;
            }

            if (boxStyle == null)
            {
                boxStyle = new GUIStyle(GUI.skin.box);
                boxStyle.padding = new RectOffset(10, 10, 10, 10);
            }
        }

        // --- Save System Tests ---

        private void DrawSaveSystemTests()
        {
            GUILayout.Label("SAVE SYSTEM", labelStyle);

            if (GUILayout.Button("Save Game Now", buttonStyle))
            {
                SaveManager.Instance?.SaveGame();
                UnityEngine.Debug.Log("[Debug] Game saved manually");
            }

            if (GUILayout.Button("Load Game", buttonStyle))
            {
                SaveManager.Instance?.LoadGame();
                UnityEngine.Debug.Log("[Debug] Game loaded");
            }

            if (GUILayout.Button("Delete Save Data", buttonStyle))
            {
                if (SaveManager.Instance != null)
                {
                    SaveManager.Instance.DeleteSaveData();
                    UnityEngine.Debug.Log("[Debug] Save data deleted");
                }
            }

            GUILayout.Space(10);
        }

        // --- Achievement Tests ---

        private void DrawAchievementTests()
        {
            GUILayout.Label("ACHIEVEMENTS", labelStyle);

            if (GUILayout.Button("Unlock Random Achievement", buttonStyle))
            {
                if (AchievementManager.Instance != null)
                {
                    var locked = AchievementManager.Instance.GetLockedAchievements();
                    if (locked.Count > 0)
                    {
                        var achievement = locked[Random.Range(0, locked.Count)];
                        AchievementManager.Instance.UpdateAchievementProgress(achievement.id, achievement.targetValue);
                        UnityEngine.Debug.Log($"[Debug] Unlocked: {achievement.title}");
                    }
                }
            }

            if (GUILayout.Button("Simulate 100 Treasures", buttonStyle))
            {
                AchievementManager.Instance?.OnTreasureCollected(100);
                UnityEngine.Debug.Log("[Debug] Simulated collecting 100 treasures");
            }

            if (GUILayout.Button("Show Achievement Progress", buttonStyle))
            {
                if (AchievementManager.Instance != null)
                {
                    float percent = AchievementManager.Instance.GetCompletionPercentage();
                    int total = AchievementManager.Instance.GetTotalAchievementGold();
                    UnityEngine.Debug.Log($"[Debug] Achievement completion: {percent:F1}%, Total gold earned: {total}");
                }
            }

            GUILayout.Space(10);
        }

        // --- Power-Up Tests ---

        private void DrawPowerUpTests()
        {
            GUILayout.Label("POWER-UPS", labelStyle);

            if (GUILayout.Button("Activate Speed Boost", buttonStyle))
            {
                PowerUpSystem.Instance?.ActivatePowerUp(PowerUpType.SpeedBoost, 15f);
                UnityEngine.Debug.Log("[Debug] Speed Boost activated");
            }

            if (GUILayout.Button("Activate Magnet", buttonStyle))
            {
                PowerUpSystem.Instance?.ActivatePowerUp(PowerUpType.Magnet, 15f);
                UnityEngine.Debug.Log("[Debug] Magnet activated");
            }

            if (GUILayout.Button("Activate Double Points", buttonStyle))
            {
                PowerUpSystem.Instance?.ActivatePowerUp(PowerUpType.DoublePoints, 15f);
                UnityEngine.Debug.Log("[Debug] Double Points activated");
            }

            if (GUILayout.Button("Clear All Power-Ups", buttonStyle))
            {
                PowerUpSystem.Instance?.ClearAllPowerUps();
                UnityEngine.Debug.Log("[Debug] All power-ups cleared");
            }

            GUILayout.Space(10);
        }

        // --- Combo Tests ---

        private void DrawComboTests()
        {
            GUILayout.Label("COMBO SYSTEM", labelStyle);

            if (GUILayout.Button("Add 10x Combo", buttonStyle))
            {
                if (ComboSystem.Instance != null)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        ComboSystem.Instance.OnTreasureCollected();
                    }
                    UnityEngine.Debug.Log($"[Debug] Combo: {ComboSystem.Instance.GetCurrentCombo()}x");
                }
            }

            if (GUILayout.Button("Reset Combo", buttonStyle))
            {
                ComboSystem.Instance?.ResetCombo();
                UnityEngine.Debug.Log("[Debug] Combo reset");
            }

            if (GUILayout.Button("Show Combo Stats", buttonStyle))
            {
                if (ComboSystem.Instance != null)
                {
                    int current = ComboSystem.Instance.GetCurrentCombo();
                    float multiplier = ComboSystem.Instance.GetCurrentMultiplier();
                    UnityEngine.Debug.Log($"[Debug] Combo: {current}x, Multiplier: {multiplier:F2}x");
                }
            }

            GUILayout.Space(10);
        }

        // --- Shop Tests ---

        private void DrawShopTests()
        {
            GUILayout.Label("SHOP SYSTEM", labelStyle);

            if (GUILayout.Button("Buy Nitro Hauler (2,500g)", buttonStyle))
            {
                ShopManager.Instance?.PurchaseItem("vehicle_nitro_hauler");
            }

            if (GUILayout.Button("Buy Power-Up Bundle", buttonStyle))
            {
                ShopManager.Instance?.PurchaseItem("powerup_mega_bundle");
            }

            if (GUILayout.Button("List All Shop Items", buttonStyle))
            {
                if (ShopManager.Instance != null)
                {
                    var items = ShopManager.Instance.GetAllItems();
                    UnityEngine.Debug.Log($"[Debug] Shop has {items.Count} items");
                    foreach (var item in items)
                    {
                        UnityEngine.Debug.Log($"  - {item.itemName}: {item.price} {item.currencyType}");
                    }
                }
            }

            GUILayout.Space(10);
        }

        // --- Daily Reward Tests ---

        private void DrawDailyRewardTests()
        {
            GUILayout.Label("DAILY REWARDS", labelStyle);

            if (GUILayout.Button("Claim Daily Reward", buttonStyle))
            {
                bool claimed = DailyRewardsManager.Instance?.ClaimDailyReward() ?? false;
                UnityEngine.Debug.Log($"[Debug] Daily reward claimed: {claimed}");
            }

            if (GUILayout.Button("Show Streak Info", buttonStyle))
            {
                if (DailyRewardsManager.Instance != null)
                {
                    int streak = DailyRewardsManager.Instance.GetCurrentStreak();
                    bool canClaim = DailyRewardsManager.Instance.CanClaimToday();
                    UnityEngine.Debug.Log($"[Debug] Streak: {streak} days, Can claim: {canClaim}");
                }
            }

            if (GUILayout.Button("Reset Daily Rewards", buttonStyle))
            {
                DailyRewardsManager.Instance?.ResetDailyRewards();
                UnityEngine.Debug.Log("[Debug] Daily rewards reset");
            }

            GUILayout.Space(10);
        }

        // --- Currency Tests ---

        private void DrawCurrencyTests()
        {
            GUILayout.Label("CURRENCY", labelStyle);

            if (GUILayout.Button("Add 1,000 Gold", buttonStyle))
            {
                SaveManager.Instance?.AddGold(1000);
                UnityEngine.Debug.Log("[Debug] Added 1,000 gold");
            }

            if (GUILayout.Button("Add 100 Gems", buttonStyle))
            {
                if (SaveManager.Instance != null)
                {
                    SaveManager.Instance.GetSaveData().totalGems += 100;
                    SaveManager.Instance.SaveGame();
                    UnityEngine.Debug.Log("[Debug] Added 100 gems");
                }
            }

            if (GUILayout.Button("Show Currency", buttonStyle))
            {
                if (SaveManager.Instance != null)
                {
                    int gold = SaveManager.Instance.GetGold();
                    int gems = SaveManager.Instance.GetSaveData().totalGems;
                    UnityEngine.Debug.Log($"[Debug] Gold: {gold}, Gems: {gems}");
                }
            }

            GUILayout.Space(10);
        }

        // --- Level Tests ---

        private void DrawLevelTests()
        {
            GUILayout.Label("LEVELS", labelStyle);

            if (GUILayout.Button("Unlock All Levels", buttonStyle))
            {
                if (SaveManager.Instance != null)
                {
                    for (int i = 1; i <= 20; i++)
                    {
                        SaveManager.Instance.UnlockLevel(i);
                    }
                    UnityEngine.Debug.Log("[Debug] Unlocked all 20 levels");
                }
            }

            if (GUILayout.Button("Set All Levels 3-Star", buttonStyle))
            {
                if (SaveManager.Instance != null)
                {
                    for (int i = 1; i <= 20; i++)
                    {
                        SaveManager.Instance.SetLevelStars(i, 3);
                    }
                    UnityEngine.Debug.Log("[Debug] Set all levels to 3 stars");
                }
            }

            GUILayout.Space(10);
        }

        // --- Analytics Tests ---

        private void DrawAnalyticsTests()
        {
            GUILayout.Label("ANALYTICS", labelStyle);

            if (GUILayout.Button("Test Event Logging", buttonStyle))
            {
                AnalyticsManager.Instance?.TestAnalyticsEvent();
                UnityEngine.Debug.Log("[Debug] Test event sent");
            }

            if (GUILayout.Button("Enable/Disable Analytics", buttonStyle))
            {
                if (AnalyticsManager.Instance != null)
                {
                    // Toggle analytics
                    AnalyticsManager.Instance.SetAnalyticsEnabled(!AnalyticsManager.Instance);
                }
            }

            GUILayout.Space(10);
        }
    }
}
