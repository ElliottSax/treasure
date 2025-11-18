using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace TreasureExcavator
{
    /// <summary>
    /// Main game manager singleton handling game state, score, and level flow.
    /// Phase 1: Foundation - Week 3
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Header("Game State")]
        [SerializeField] private GameState currentState = GameState.Menu;

        [Header("Score Settings")]
        [SerializeField] private int currentScore = 0;
        [SerializeField] private int targetScore = 100;
        [SerializeField] private int twoStarScore = 150;
        [SerializeField] private int threeStarScore = 200;

        [Header("Level Info")]
        [SerializeField] private int currentLevel = 1;

        [Header("Events")]
        public UnityEvent<int> onScoreChanged;
        public UnityEvent<int> onLevelComplete;
        public UnityEvent<GameState> onGameStateChanged;

        // Game state
        public enum GameState
        {
            Menu,
            Playing,
            Paused,
            LevelComplete,
            GameOver
        }

        private void Awake()
        {
            // Singleton pattern
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            // Load saved progress
            LoadProgress();
        }

        #region Game State Management

        public void SetGameState(GameState newState)
        {
            if (currentState == newState) return;

            currentState = newState;
            onGameStateChanged?.Invoke(newState);

            // Handle state transitions
            switch (newState)
            {
                case GameState.Menu:
                    Time.timeScale = 1f;
                    if (AudioManager.Instance != null)
                        AudioManager.Instance.PlayMainMenuMusic();
                    break;

                case GameState.Playing:
                    Time.timeScale = 1f;
                    if (AudioManager.Instance != null)
                        AudioManager.Instance.PlayGameplayMusic();
                    break;

                case GameState.Paused:
                    Time.timeScale = 0f;
                    break;

                case GameState.LevelComplete:
                    Time.timeScale = 0f;
                    HandleLevelComplete();
                    break;

                case GameState.GameOver:
                    Time.timeScale = 0f;
                    break;
            }

            Debug.Log($"Game State Changed: {newState}");
        }

        public GameState GetGameState() => currentState;

        #endregion

        #region Score Management

        public void AddScore(int amount)
        {
            currentScore += amount;
            onScoreChanged?.Invoke(currentScore);

            Debug.Log($"Score: {currentScore}/{targetScore}");

            // Check for level completion
            if (currentScore >= targetScore && currentState == GameState.Playing)
            {
                SetGameState(GameState.LevelComplete);
            }
        }

        public void ResetScore()
        {
            currentScore = 0;
            onScoreChanged?.Invoke(currentScore);
        }

        public int GetCurrentScore() => currentScore;
        public int GetTargetScore() => targetScore;
        public float GetScoreProgress() => (float)currentScore / targetScore;

        public int GetStarRating()
        {
            if (currentScore >= threeStarScore) return 3;
            if (currentScore >= twoStarScore) return 2;
            if (currentScore >= targetScore) return 1;
            return 0;
        }

        #endregion

        #region Level Management

        public void StartLevel(int levelNumber)
        {
            currentLevel = levelNumber;

            // Load level data (will implement in Phase 2)
            LoadLevelData(levelNumber);

            // Reset score
            ResetScore();

            // Start playing
            SetGameState(GameState.Playing);

            Debug.Log($"Starting Level {levelNumber}");
        }

        public void RestartLevel()
        {
            // Reload current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            // Reset score
            ResetScore();

            // Start playing
            SetGameState(GameState.Playing);
        }

        public void NextLevel()
        {
            int nextLevel = currentLevel + 1;

            // Check if next level exists
            if (nextLevel <= GetTotalLevels())
            {
                StartLevel(nextLevel);
            }
            else
            {
                // All levels completed - return to menu
                ReturnToMenu();
            }
        }

        public void ReturnToMenu()
        {
            // Load main menu scene (assuming scene index 0)
            SceneManager.LoadScene(0);
            SetGameState(GameState.Menu);
        }

        private void HandleLevelComplete()
        {
            int stars = GetStarRating();
            int goldEarned = CalculateGoldReward(stars);

            // Save level completion
            SaveLevelCompletion(currentLevel, stars, currentScore);

            // Award gold
            AddGold(goldEarned);

            // Invoke event
            onLevelComplete?.Invoke(stars);

            // Play victory music
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayVictoryMusic();
            }

            Debug.Log($"Level {currentLevel} Complete! Stars: {stars}, Gold: {goldEarned}");
        }

        private void LoadLevelData(int levelNumber)
        {
            // TODO: Load level data from ScriptableObject or JSON
            // For now, use placeholder values

            switch (levelNumber)
            {
                case 1:
                    targetScore = 100;
                    twoStarScore = 150;
                    threeStarScore = 200;
                    break;
                case 2:
                    targetScore = 150;
                    twoStarScore = 225;
                    threeStarScore = 300;
                    break;
                // Add more levels as needed
                default:
                    targetScore = 100 + (levelNumber - 1) * 50;
                    twoStarScore = (int)(targetScore * 1.5f);
                    threeStarScore = targetScore * 2;
                    break;
            }
        }

        private int GetTotalLevels()
        {
            // TODO: Get from level data configuration
            return 8; // 8 levels in MVP
        }

        #endregion

        #region Currency Management

        private int totalGold = 0;

        public void AddGold(int amount)
        {
            totalGold += amount;
            SaveProgress();
            Debug.Log($"Gold earned: +{amount} (Total: {totalGold})");
        }

        public bool SpendGold(int amount)
        {
            if (totalGold >= amount)
            {
                totalGold -= amount;
                SaveProgress();
                Debug.Log($"Gold spent: -{amount} (Remaining: {totalGold})");
                return true;
            }

            Debug.LogWarning($"Not enough gold! Need: {amount}, Have: {totalGold}");
            return false;
        }

        public int GetGold() => totalGold;

        private int CalculateGoldReward(int stars)
        {
            // Base reward per level
            int baseReward = 50 + (currentLevel - 1) * 10;

            // Multiply by stars
            return baseReward * stars;
        }

        #endregion

        #region Save/Load

        private void SaveProgress()
        {
            PlayerPrefs.SetInt("TotalGold", totalGold);
            PlayerPrefs.Save();
        }

        private void LoadProgress()
        {
            totalGold = PlayerPrefs.GetInt("TotalGold", 0);
        }

        private void SaveLevelCompletion(int level, int stars, int score)
        {
            // Save best stars for this level
            int savedStars = PlayerPrefs.GetInt($"Level_{level}_Stars", 0);
            if (stars > savedStars)
            {
                PlayerPrefs.SetInt($"Level_{level}_Stars", stars);
            }

            // Save best score for this level
            int savedScore = PlayerPrefs.GetInt($"Level_{level}_BestScore", 0);
            if (score > savedScore)
            {
                PlayerPrefs.SetInt($"Level_{level}_BestScore", score);
            }

            // Unlock next level
            int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
            if (level >= unlockedLevel)
            {
                PlayerPrefs.SetInt("UnlockedLevel", level + 1);
            }

            PlayerPrefs.Save();
        }

        public int GetLevelStars(int level)
        {
            return PlayerPrefs.GetInt($"Level_{level}_Stars", 0);
        }

        public int GetLevelBestScore(int level)
        {
            return PlayerPrefs.GetInt($"Level_{level}_BestScore", 0);
        }

        public bool IsLevelUnlocked(int level)
        {
            int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
            return level <= unlockedLevel;
        }

        #endregion

        #region Pause/Resume

        public void PauseGame()
        {
            if (currentState == GameState.Playing)
            {
                SetGameState(GameState.Paused);
            }
        }

        public void ResumeGame()
        {
            if (currentState == GameState.Paused)
            {
                SetGameState(GameState.Playing);
            }
        }

        #endregion
    }
}
