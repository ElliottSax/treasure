using UnityEngine;

namespace TreasureExcavator
{
    /// <summary>
    /// Extension methods for CargoManager to support Phase 2 systems.
    /// Add these methods to your existing CargoManager.cs file.
    /// </summary>
    public partial class CargoManager
    {
        // --- Power-Up Support Methods ---

        /// <summary>
        /// Instantly fills cargo to maximum capacity (for Instant Capacity power-up)
        /// </summary>
        public void FillToCapacity()
        {
            int spaceavailable = maxCapacity - currentCargo;

            if (spaceAvailable > 0)
            {
                currentCargo = maxCapacity;
                onCargoChanged?.Invoke(currentCargo);

                // Visual/audio feedback
                if (ParticleEffectsManager.Instance != null)
                {
                    ParticleEffectsManager.Instance.PlayEffect("InstantFill", transform.position);
                }

                if (AudioManager.Instance != null)
                {
                    AudioManager.Instance.PlaySFX("CargoFilled");
                }

                Debug.Log($"[CargoManager] Cargo instantly filled to capacity ({maxCapacity})");
            }
        }

        /// <summary>
        /// Loses cargo when hitting obstacles
        /// </summary>
        public void LoseCargo(int amount)
        {
            if (amount <= 0 || currentCargo <= 0)
                return;

            int actualLoss = Mathf.Min(amount, currentCargo);
            currentCargo -= actualLoss;

            // Fire event
            onCargoChanged?.Invoke(currentCargo);

            // Visual feedback - spawn treasure items falling out
            if (ParticleEffectsManager.Instance != null)
            {
                ParticleEffectsManager.Instance.PlayEffect("CargoLoss", transform.position);
            }

            // Audio feedback
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySFX("CargoLoss");
            }

            Debug.Log($"[CargoManager] Lost {actualLoss} cargo (remaining: {currentCargo})");

            // Track with analytics
            if (AnalyticsManager.Instance != null)
            {
                AnalyticsManager.Instance.TrackEvent("cargo_lost", new System.Collections.Generic.Dictionary<string, object>
                {
                    { "amount", actualLoss },
                    { "remaining", currentCargo }
                });
            }
        }

        /// <summary>
        /// Gets current cargo count
        /// </summary>
        public int GetCurrentCargo()
        {
            return currentCargo;
        }

        /// <summary>
        /// Gets maximum cargo capacity
        /// </summary>
        public int GetMaxCapacity()
        {
            return maxCapacity;
        }

        /// <summary>
        /// Gets cargo fill percentage (0-1)
        /// </summary>
        public float GetCargoFillPercentage()
        {
            if (maxCapacity <= 0)
                return 0f;

            return (float)currentCargo / maxCapacity;
        }

        /// <summary>
        /// Checks if cargo is full
        /// </summary>
        public bool IsCargoFull()
        {
            return currentCargo >= maxCapacity;
        }

        /// <summary>
        /// Checks if cargo is empty
        /// </summary>
        public bool IsCargoEmpty()
        {
            return currentCargo <= 0;
        }

        /// <summary>
        /// Gets total value of current cargo
        /// </summary>
        public int GetCurrentCargoValue()
        {
            return totalValueCollected;
        }

        // --- Integration with Achievements ---

        /// <summary>
        /// Override your existing AddTreasure to integrate with new systems
        /// </summary>
        public void AddTreasure(Treasure treasure)
        {
            // Your existing AddTreasure logic here

            // Update combo system
            if (ComboSystem.Instance != null)
            {
                ComboSystem.Instance.OnTreasureCollected();
            }

            // Update achievement system
            if (AchievementManager.Instance != null)
            {
                AchievementManager.Instance.OnTreasureCollected(1);
            }

            // Track analytics
            if (AnalyticsManager.Instance != null)
            {
                AnalyticsManager.Instance.TrackTreasureCollected(
                    treasure.GetTreasureType().ToString(),
                    treasure.GetValue()
                );
            }
        }

        // --- Integration with Multiplier Gates ---

        /// <summary>
        /// Override your existing ApplyMultiplier to track analytics
        /// </summary>
        public void ApplyMultiplier(int multiplier)
        {
            // Your existing multiplier logic here
            // totalValueCollected *= multiplier;

            // Update achievement system
            if (AchievementManager.Instance != null)
            {
                AchievementManager.Instance.OnGateUsed(1);

                // Special achievement for x10 gate with full cargo
                if (multiplier == 10 && IsCargoFull())
                {
                    AchievementManager.Instance.UpdateAchievementProgress("multiplier_king");
                }
            }

            // Track analytics
            if (AnalyticsManager.Instance != null)
            {
                AnalyticsManager.Instance.TrackGateUsed(
                    $"x{multiplier}",
                    multiplier,
                    GetCurrentCargoValue()
                );
            }
        }

        // --- Integration with Deposit ---

        /// <summary>
        /// Override your existing DepositCargo to integrate with new systems
        /// </summary>
        public int DepositCargo(int multiplier = 1)
        {
            if (currentCargo <= 0)
                return 0;

            int scoreEarned = totalValueCollected * multiplier;

            // Apply GameManager score multipliers
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScoreWithMultipliers(scoreEarned);
            }

            // Track analytics
            if (AnalyticsManager.Instance != null)
            {
                AnalyticsManager.Instance.TrackEvent("cargo_deposited", new System.Collections.Generic.Dictionary<string, object>
                {
                    { "cargo_count", currentCargo },
                    { "total_value", totalValueCollected },
                    { "score_earned", scoreEarned }
                });
            }

            // Reset cargo
            currentCargo = 0;
            totalValueCollected = 0;
            onCargoChanged?.Invoke(currentCargo);

            return scoreEarned;
        }

        // --- Visual Feedback Methods ---

        /// <summary>
        /// Shows visual indicator for cargo status
        /// </summary>
        private void UpdateCargoVisuals()
        {
            // Update cargo visual representation based on fill percentage
            float fillPercent = GetCargoFillPercentage();

            // Change color based on fullness
            if (fillPercent >= 0.9f)
            {
                // Nearly full - pulse effect
                if (ParticleEffectsManager.Instance != null)
                {
                    ParticleEffectsManager.Instance.PlayEffectAttached("Sparkle", transform);
                }
            }

            // Update any visual cargo indicators here
            // Example: cargoMeshRenderer.material.SetFloat("_FillAmount", fillPercent);
        }
    }
}
