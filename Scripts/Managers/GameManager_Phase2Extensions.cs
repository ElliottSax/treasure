using UnityEngine;

namespace TreasureExcavator
{
    /// <summary>
    /// Extension methods for GameManager to support Phase 2 systems.
    /// Add these methods to your existing GameManager.cs file.
    /// </summary>
    public partial class GameManager
    {
        [Header("Phase 2 - Multipliers & Timers")]
        private float scoreMultiplier = 1f;
        private float goldMultiplier = 1f;
        private bool timerFrozen = false;
        private float frozenTimeRemaining = 0f;

        // --- Score Multiplier Methods ---

        /// <summary>
        /// Sets score multiplier (for power-ups like Double Points)
        /// </summary>
        public void SetScoreMultiplier(float multiplier)
        {
            scoreMultiplier = Mathf.Max(0f, multiplier);
            Debug.Log($"[GameManager] Score multiplier set to {scoreMultiplier}x");
        }

        /// <summary>
        /// Gets current score multiplier
        /// </summary>
        public float GetScoreMultiplier()
        {
            return scoreMultiplier;
        }

        /// <summary>
        /// Adds score with multipliers applied
        /// Call this version instead of the basic AddScore when you want multipliers
        /// </summary>
        public void AddScoreWithMultipliers(int baseScore)
        {
            // Apply score multiplier from power-ups
            int multipliedScore = Mathf.RoundToInt(baseScore * scoreMultiplier);

            // Apply combo multiplier if available
            if (ComboSystem.Instance != null)
            {
                multipliedScore = ComboSystem.Instance.ApplyComboToScore(multipliedScore);
            }

            // Add to total score
            AddScore(multipliedScore);

            Debug.Log($"[GameManager] Added {baseScore} → {multipliedScore} (after multipliers)");
        }

        // --- Gold Multiplier Methods ---

        /// <summary>
        /// Sets gold earning multiplier (for power-ups like Gold Rush)
        /// </summary>
        public void SetGoldMultiplier(float multiplier)
        {
            goldMultiplier = Mathf.Max(0f, multiplier);
            Debug.Log($"[GameManager] Gold multiplier set to {goldMultiplier}x");
        }

        /// <summary>
        /// Gets current gold multiplier
        /// </summary>
        public float GetGoldMultiplier()
        {
            return goldMultiplier;
        }

        /// <summary>
        /// Adds gold with multiplier applied
        /// </summary>
        public void AddGoldWithMultiplier(int baseGold)
        {
            int multipliedGold = Mathf.RoundToInt(baseGold * goldMultiplier);

            if (SaveManager.Instance != null)
            {
                SaveManager.Instance.AddGold(multipliedGold);
            }

            Debug.Log($"[GameManager] Added {baseGold} → {multipliedGold} gold");
        }

        // --- Timer Methods ---

        /// <summary>
        /// Freezes or unfreezes the level timer (for Time Freeze power-up)
        /// </summary>
        public void FreezeTimer(bool freeze)
        {
            timerFrozen = freeze;

            if (freeze)
            {
                // Store current time remaining
                frozenTimeRemaining = GetRemainingTime();
                Debug.Log($"[GameManager] Timer frozen at {frozenTimeRemaining:F1}s");
            }
            else
            {
                Debug.Log("[GameManager] Timer unfrozen");
            }
        }

        /// <summary>
        /// Checks if timer is currently frozen
        /// </summary>
        public bool IsTimerFrozen()
        {
            return timerFrozen;
        }

        /// <summary>
        /// Gets remaining time (respects frozen state)
        /// </summary>
        public float GetRemainingTime()
        {
            if (timerFrozen)
            {
                return frozenTimeRemaining;
            }

            // Return your actual timer value here
            // Example: return levelTimeLimit - elapsedTime;
            return 0f; // Replace with your actual timer logic
        }

        // --- Progress Methods ---

        /// <summary>
        /// Gets score progress as a percentage (0-1) for UI progress bars
        /// </summary>
        public float GetScoreProgress()
        {
            int currentScore = GetCurrentScore();
            int threeStarTarget = GetThreeStarTarget();

            if (threeStarTarget <= 0)
                return 0f;

            return Mathf.Clamp01((float)currentScore / threeStarTarget);
        }

        /// <summary>
        /// Gets score progress towards next star
        /// </summary>
        public float GetScoreProgressToNextStar()
        {
            int currentScore = GetCurrentScore();
            int currentStars = GetCurrentStars();

            int nextStarTarget = 0;
            int previousStarTarget = 0;

            switch (currentStars)
            {
                case 0:
                    previousStarTarget = 0;
                    nextStarTarget = GetOneStarTarget();
                    break;
                case 1:
                    previousStarTarget = GetOneStarTarget();
                    nextStarTarget = GetTwoStarTarget();
                    break;
                case 2:
                    previousStarTarget = GetTwoStarTarget();
                    nextStarTarget = GetThreeStarTarget();
                    break;
                case 3:
                    return 1f; // Already at max stars
            }

            if (nextStarTarget <= previousStarTarget)
                return 0f;

            float progress = (float)(currentScore - previousStarTarget) / (nextStarTarget - previousStarTarget);
            return Mathf.Clamp01(progress);
        }

        // --- Helper Methods for Score Targets ---

        /// <summary>
        /// Gets 1-star target score
        /// </summary>
        public int GetOneStarTarget()
        {
            // Replace with your actual level data
            // Example: return currentLevelData.OneStarScore;
            return 100; // Placeholder
        }

        /// <summary>
        /// Gets 2-star target score
        /// </summary>
        public int GetTwoStarTarget()
        {
            // Replace with your actual level data
            // Example: return currentLevelData.TwoStarScore;
            return 200; // Placeholder
        }

        /// <summary>
        /// Gets 3-star target score
        /// </summary>
        public int GetThreeStarTarget()
        {
            // Replace with your actual level data
            // Example: return currentLevelData.ThreeStarScore;
            return 300; // Placeholder
        }

        /// <summary>
        /// Gets current score
        /// </summary>
        public int GetCurrentScore()
        {
            // Replace with your actual score variable
            // Example: return currentScore;
            return 0; // Placeholder
        }

        /// <summary>
        /// Gets current star rating based on score
        /// </summary>
        public int GetCurrentStars()
        {
            int score = GetCurrentScore();

            if (score >= GetThreeStarTarget())
                return 3;
            if (score >= GetTwoStarTarget())
                return 2;
            if (score >= GetOneStarTarget())
                return 1;

            return 0;
        }

        // --- Analytics Integration ---

        /// <summary>
        /// Override your existing AddScore to track analytics
        /// </summary>
        public void AddScore(int amount)
        {
            // Your existing score adding logic here
            // currentScore += amount;

            // Track with analytics
            if (AnalyticsManager.Instance != null)
            {
                // Track milestone scores
                int newScore = GetCurrentScore();
                if (newScore >= 1000 && newScore - amount < 1000)
                {
                    AnalyticsManager.Instance.TrackEvent("score_milestone_1000");
                }
            }
        }

        // --- Phase 2 Integration Update Method ---

        /// <summary>
        /// Call this in your existing Update() to handle frozen timer
        /// </summary>
        private void UpdateTimerWithFreeze()
        {
            if (!timerFrozen)
            {
                // Your normal timer update code here
                // Example: elapsedTime += Time.deltaTime;
            }
            else
            {
                // Timer is frozen, don't update
                // Keep displaying frozenTimeRemaining
            }
        }
    }
}
