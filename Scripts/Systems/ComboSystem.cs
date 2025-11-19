using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace TreasureExcavator
{
    /// <summary>
    /// Manages combo multiplier system for consecutive treasure collection.
    /// Rewards players for collecting treasures quickly without breaks.
    /// </summary>
    public class ComboSystem : Singleton<ComboSystem>
    {
        [Header("Combo Settings")]
        [SerializeField] private float comboTimeWindow = 3f; // Time between collections to maintain combo
        [SerializeField] private int minComboForBonus = 3; // Minimum combo to apply multiplier
        [SerializeField] private float comboScoreMultiplier = 0.1f; // +10% per combo level

        [Header("Combo Thresholds")]
        [SerializeField] private int[] comboMilestones = { 5, 10, 15, 20, 30, 50 }; // Achievement thresholds

        [Header("Events")]
        public UnityEvent<int> onComboChanged = new UnityEvent<int>();
        public UnityEvent<int, float> onComboMultiplierApplied = new UnityEvent<int, float>();
        public UnityEvent<int> onComboMilestoneReached = new UnityEvent<int>();
        public UnityEvent onComboBroken = new UnityEvent();

        private int currentCombo = 0;
        private int highestComboThisLevel = 0;
        private float comboTimer = 0f;
        private bool comboActive = false;

        private Coroutine comboTimeoutCoroutine;

        protected override void Awake()
        {
            base.Awake();
        }

        private void Update()
        {
            // Update combo timer display if active
            if (comboActive && comboTimer > 0)
            {
                comboTimer -= Time.deltaTime;
            }
        }

        /// <summary>
        /// Called when a treasure is collected
        /// </summary>
        public void OnTreasureCollected()
        {
            // Increment combo
            currentCombo++;

            // Update highest combo
            if (currentCombo > highestComboThisLevel)
            {
                highestComboThisLevel = currentCombo;

                // Update all-time highest combo
                if (SaveManager.Instance != null)
                {
                    int allTimeHighest = SaveManager.Instance.GetSaveData().highestCombo;
                    if (currentCombo > allTimeHighest)
                    {
                        SaveManager.Instance.GetSaveData().highestCombo = currentCombo;
                        SaveManager.Instance.SaveGame();
                    }
                }
            }

            // Check for milestone achievements
            CheckComboMilestones();

            // Reset combo timer
            comboTimer = comboTimeWindow;
            comboActive = true;

            // Restart timeout coroutine
            if (comboTimeoutCoroutine != null)
            {
                StopCoroutine(comboTimeoutCoroutine);
            }
            comboTimeoutCoroutine = StartCoroutine(ComboTimeoutCountdown());

            // Fire combo changed event
            onComboChanged?.Invoke(currentCombo);

            // Update achievements
            if (AchievementManager.Instance != null)
            {
                AchievementManager.Instance.OnComboAchieved(currentCombo);
            }

            Debug.Log($"[ComboSystem] Combo: {currentCombo}x (Multiplier: {GetCurrentMultiplier():F2}x)");
        }

        /// <summary>
        /// Countdown for combo timeout
        /// </summary>
        private IEnumerator ComboTimeoutCountdown()
        {
            yield return new WaitForSeconds(comboTimeWindow);

            // Time's up - break combo
            BreakCombo();
        }

        /// <summary>
        /// Breaks the current combo
        /// </summary>
        private void BreakCombo()
        {
            if (currentCombo == 0) return; // Already broken

            Debug.Log($"[ComboSystem] Combo broken! Final combo: {currentCombo}x");

            currentCombo = 0;
            comboActive = false;
            comboTimer = 0f;

            onComboBroken?.Invoke();
            onComboChanged?.Invoke(0);
        }

        /// <summary>
        /// Manually resets combo (for level restart, etc.)
        /// </summary>
        public void ResetCombo()
        {
            if (comboTimeoutCoroutine != null)
            {
                StopCoroutine(comboTimeoutCoroutine);
            }

            currentCombo = 0;
            highestComboThisLevel = 0;
            comboActive = false;
            comboTimer = 0f;

            onComboChanged?.Invoke(0);
        }

        /// <summary>
        /// Gets the current score multiplier based on combo
        /// </summary>
        public float GetCurrentMultiplier()
        {
            if (currentCombo < minComboForBonus)
            {
                return 1f; // No bonus
            }

            // Calculate multiplier: 1.0 + (combo * multiplier_per_combo)
            return 1f + ((currentCombo - minComboForBonus + 1) * comboScoreMultiplier);
        }

        /// <summary>
        /// Applies combo multiplier to a score value
        /// </summary>
        public int ApplyComboToScore(int baseScore)
        {
            float multiplier = GetCurrentMultiplier();

            if (multiplier > 1f)
            {
                int bonusScore = Mathf.RoundToInt(baseScore * multiplier);
                onComboMultiplierApplied?.Invoke(currentCombo, multiplier);
                return bonusScore;
            }

            return baseScore;
        }

        /// <summary>
        /// Checks if combo has reached any milestones
        /// </summary>
        private void CheckComboMilestones()
        {
            foreach (int milestone in comboMilestones)
            {
                if (currentCombo == milestone)
                {
                    onComboMilestoneReached?.Invoke(milestone);

                    // Play special effects
                    if (AudioManager.Instance != null)
                    {
                        AudioManager.Instance.PlaySFX("ComboMilestone");
                    }

                    if (SettingsManager.Instance != null)
                    {
                        SettingsManager.Instance.TriggerHaptic(SettingsManager.HapticType.Success);
                    }

                    Debug.Log($"[ComboSystem] Milestone reached: {milestone}x combo!");
                }
            }
        }

        /// <summary>
        /// Gets combo time remaining (for UI display)
        /// </summary>
        public float GetComboTimeRemaining()
        {
            return Mathf.Max(0f, comboTimer);
        }

        /// <summary>
        /// Gets combo time remaining as percentage (0-1)
        /// </summary>
        public float GetComboTimeRemainingPercent()
        {
            return comboActive ? Mathf.Clamp01(comboTimer / comboTimeWindow) : 0f;
        }

        /// <summary>
        /// Gets current combo count
        /// </summary>
        public int GetCurrentCombo()
        {
            return currentCombo;
        }

        /// <summary>
        /// Gets highest combo achieved this level
        /// </summary>
        public int GetHighestComboThisLevel()
        {
            return highestComboThisLevel;
        }

        /// <summary>
        /// Checks if combo is currently active
        /// </summary>
        public bool IsComboActive()
        {
            return comboActive && currentCombo > 0;
        }

        /// <summary>
        /// Extends combo time window (power-up or special ability)
        /// </summary>
        public void ExtendComboTime(float additionalTime)
        {
            comboTimer += additionalTime;
            Debug.Log($"[ComboSystem] Combo time extended by {additionalTime}s");
        }

        /// <summary>
        /// Gets color for combo display based on combo level
        /// </summary>
        public Color GetComboColor()
        {
            if (currentCombo < 5)
                return Color.white;
            else if (currentCombo < 10)
                return new Color(0.5f, 1f, 0.5f); // Light green
            else if (currentCombo < 20)
                return new Color(1f, 0.84f, 0f); // Gold
            else if (currentCombo < 30)
                return new Color(1f, 0.5f, 0f); // Orange
            else
                return new Color(1f, 0.2f, 0.2f); // Red
        }

        /// <summary>
        /// Gets display text for combo
        /// </summary>
        public string GetComboText()
        {
            if (currentCombo < minComboForBonus)
            {
                return $"{currentCombo}x";
            }

            return $"{currentCombo}x COMBO!";
        }

        /// <summary>
        /// Called when level ends - saves stats
        /// </summary>
        public void OnLevelEnd()
        {
            Debug.Log($"[ComboSystem] Level ended. Highest combo: {highestComboThisLevel}x");

            // Stop timeout coroutine
            if (comboTimeoutCoroutine != null)
            {
                StopCoroutine(comboTimeoutCoroutine);
            }
        }
    }
}
