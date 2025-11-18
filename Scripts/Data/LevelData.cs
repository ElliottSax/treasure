using UnityEngine;

namespace TreasureExcavator
{
    /// <summary>
    /// ScriptableObject defining a level's configuration and requirements.
    /// Phase 1: Foundation - Data Structures
    /// </summary>
    [CreateAssetMenu(fileName = "LevelData", menuName = "TreasureExcavator/Level Data", order = 1)]
    public class LevelData : ScriptableObject
    {
        [Header("Level Info")]
        [SerializeField] private int levelNumber = 1;
        [SerializeField] private string levelName = "First Haul";
        [SerializeField, TextArea] private string description = "Your first treasure collection mission!";

        [Header("Score Requirements")]
        [SerializeField] private int oneStarScore = 100;
        [SerializeField] private int twoStarScore = 150;
        [SerializeField] private int threeStarScore = 200;

        [Header("Treasure Distribution")]
        [SerializeField] private int smallTreasureCount = 5;
        [SerializeField] private int mediumTreasureCount = 0;
        [SerializeField] private int largeTreasureCount = 0;

        [Header("Gates Available")]
        [SerializeField] private int bronzeGateCount = 1;
        [SerializeField] private int silverGateCount = 0;
        [SerializeField] private int goldGateCount = 0;
        [SerializeField] private int platinumGateCount = 0;

        [Header("Level Layout")]
        [SerializeField] private Vector2 levelSize = new Vector2(100f, 100f);
        [SerializeField] private string sceneName = "Level_01";

        [Header("Rewards")]
        [SerializeField] private int baseGoldReward = 50;
        [SerializeField] private VehicleData vehicleUnlock;

        [Header("Difficulty")]
        [SerializeField, Range(1, 10)] private int difficultyRating = 1;
        [SerializeField] private float estimatedCompletionTime = 180f; // seconds

        // Public getters
        public int LevelNumber => levelNumber;
        public string LevelName => levelName;
        public string Description => description;
        public int OneStarScore => oneStarScore;
        public int TwoStarScore => twoStarScore;
        public int ThreeStarScore => threeStarScore;
        public int SmallTreasureCount => smallTreasureCount;
        public int MediumTreasureCount => mediumTreasureCount;
        public int LargeTreasureCount => largeTreasureCount;
        public int BronzeGateCount => bronzeGateCount;
        public int SilverGateCount => silverGateCount;
        public int GoldGateCount => goldGateCount;
        public int PlatinumGateCount => platinumGateCount;
        public Vector2 LevelSize => levelSize;
        public string SceneName => sceneName;
        public int BaseGoldReward => baseGoldReward;
        public VehicleData VehicleUnlock => vehicleUnlock;
        public int DifficultyRating => difficultyRating;
        public float EstimatedCompletionTime => estimatedCompletionTime;

        /// <summary>
        /// Get total treasure value if all collected without multipliers.
        /// </summary>
        public int GetTotalTreasureValue()
        {
            return (smallTreasureCount * 10) +
                   (mediumTreasureCount * 50) +
                   (largeTreasureCount * 200);
        }

        /// <summary>
        /// Get total number of treasures.
        /// </summary>
        public int GetTotalTreasureCount()
        {
            return smallTreasureCount + mediumTreasureCount + largeTreasureCount;
        }

        /// <summary>
        /// Get total number of gates.
        /// </summary>
        public int GetTotalGateCount()
        {
            return bronzeGateCount + silverGateCount + goldGateCount + platinumGateCount;
        }

        /// <summary>
        /// Validate level configuration.
        /// </summary>
        public bool IsValid()
        {
            // Must have at least one treasure
            if (GetTotalTreasureCount() == 0)
            {
                Debug.LogWarning($"Level {levelNumber}: No treasures defined!");
                return false;
            }

            // Star requirements must be progressive
            if (twoStarScore <= oneStarScore || threeStarScore <= twoStarScore)
            {
                Debug.LogWarning($"Level {levelNumber}: Invalid star score progression!");
                return false;
            }

            // Three star score should be achievable (rough check)
            int maxPossible = GetTotalTreasureValue() * GetHighestMultiplier();
            if (threeStarScore > maxPossible * 2)
            {
                Debug.LogWarning($"Level {levelNumber}: Three star score may be too high! Max possible: {maxPossible}");
            }

            return true;
        }

        private int GetHighestMultiplier()
        {
            if (platinumGateCount > 0) return 10;
            if (goldGateCount > 0) return 5;
            if (silverGateCount > 0) return 3;
            if (bronzeGateCount > 0) return 2;
            return 1;
        }

        #if UNITY_EDITOR
        private void OnValidate()
        {
            // Auto-set level name based on number
            if (string.IsNullOrEmpty(levelName))
            {
                levelName = $"Level {levelNumber}";
            }

            // Auto-set scene name
            if (string.IsNullOrEmpty(sceneName))
            {
                sceneName = $"Level_{levelNumber:00}";
            }

            // Validate on changes
            IsValid();
        }
        #endif
    }
}
