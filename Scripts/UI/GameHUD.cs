using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TreasureExcavator.UI
{
    /// <summary>
    /// In-game HUD displaying score, cargo, and other gameplay info.
    /// Phase 1: Foundation - Week 3
    /// </summary>
    public class GameHUD : MonoBehaviour
    {
        [Header("Score Display")]
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI targetScoreText;
        [SerializeField] private Image scoreProgressBar;

        [Header("Cargo Display")]
        [SerializeField] private TextMeshProUGUI cargoText;
        [SerializeField] private Image cargoProgressBar;

        [Header("Star Progress")]
        [SerializeField] private Image[] starImages;
        [SerializeField] private Sprite starFilled;
        [SerializeField] private Sprite starEmpty;

        [Header("Target Score Reminder")]
        [SerializeField] private GameObject targetScoreReminder;
        [SerializeField] private float reminderDuration = 10f;

        [Header("Pause Button")]
        [SerializeField] private Button pauseButton;

        // References
        private GameManager gameManager;
        private CargoManager cargoManager;

        // State
        private float reminderTimer = 0f;

        private void Start()
        {
            // Get references
            gameManager = GameManager.Instance;
            cargoManager = FindObjectOfType<CargoManager>();

            // Subscribe to events
            if (gameManager != null)
            {
                gameManager.onScoreChanged.AddListener(UpdateScore);
                gameManager.onGameStateChanged.AddListener(OnGameStateChanged);
            }

            if (cargoManager != null)
            {
                cargoManager.onCargoChanged.AddListener(UpdateCargo);
            }

            // Setup pause button
            if (pauseButton != null)
            {
                pauseButton.onClick.AddListener(OnPauseClicked);
            }

            // Initialize displays
            UpdateScore(0);
            UpdateCargo(0);
            UpdateStars(0);

            // Show target score reminder
            ShowTargetScoreReminder();
        }

        private void Update()
        {
            // Update target score reminder timer
            if (targetScoreReminder != null && targetScoreReminder.activeSelf)
            {
                reminderTimer += Time.deltaTime;
                if (reminderTimer >= reminderDuration)
                {
                    targetScoreReminder.SetActive(false);
                }
            }
        }

        private void OnDestroy()
        {
            // Unsubscribe from events
            if (gameManager != null)
            {
                gameManager.onScoreChanged.RemoveListener(UpdateScore);
                gameManager.onGameStateChanged.RemoveListener(OnGameStateChanged);
            }

            if (cargoManager != null)
            {
                cargoManager.onCargoChanged.RemoveListener(UpdateCargo);
            }

            if (pauseButton != null)
            {
                pauseButton.onClick.RemoveListener(OnPauseClicked);
            }
        }

        private void UpdateScore(int score)
        {
            if (gameManager == null) return;

            // Update score text
            if (scoreText != null)
            {
                scoreText.text = $"Score: {score}";
            }

            // Update target score display
            if (targetScoreText != null)
            {
                targetScoreText.text = $"Target: {gameManager.GetTargetScore()}";
            }

            // Update progress bar
            if (scoreProgressBar != null)
            {
                scoreProgressBar.fillAmount = gameManager.GetScoreProgress();
            }

            // Update stars
            UpdateStars(gameManager.GetStarRating());
        }

        private void UpdateCargo(int cargoAmount)
        {
            if (cargoManager == null) return;

            // Update cargo text
            if (cargoText != null)
            {
                cargoText.text = $"Cargo: {cargoAmount}/{cargoManager.GetMaxCapacity()}";
            }

            // Update cargo progress bar
            if (cargoProgressBar != null)
            {
                cargoProgressBar.fillAmount = cargoManager.GetFillPercentage();
            }

            // Change color when full
            if (cargoProgressBar != null)
            {
                cargoProgressBar.color = cargoManager.IsFull() ? Color.red : Color.green;
            }
        }

        private void UpdateStars(int starCount)
        {
            if (starImages == null || starImages.Length == 0) return;

            for (int i = 0; i < starImages.Length; i++)
            {
                if (starImages[i] != null)
                {
                    starImages[i].sprite = i < starCount ? starFilled : starEmpty;
                }
            }
        }

        private void ShowTargetScoreReminder()
        {
            if (targetScoreReminder != null)
            {
                targetScoreReminder.SetActive(true);
                reminderTimer = 0f;
            }
        }

        private void OnPauseClicked()
        {
            if (gameManager != null)
            {
                gameManager.PauseGame();
            }
        }

        private void OnGameStateChanged(GameManager.GameState newState)
        {
            // Hide HUD when not playing
            bool shouldShow = newState == GameManager.GameState.Playing;
            gameObject.SetActive(shouldShow);
        }

        // Public API for showing notifications
        public void ShowNotification(string message, float duration = 2f)
        {
            // TODO: Implement notification system in Phase 2
            Debug.Log($"Notification: {message}");
        }
    }
}
