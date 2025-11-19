using UnityEngine;
using UnityEngine.Events;

namespace TreasureExcavator
{
    /// <summary>
    /// Manages game settings including audio, graphics, controls, and accessibility.
    /// Integrates with SaveManager for persistence and provides real-time setting updates.
    /// </summary>
    public class SettingsManager : Singleton<SettingsManager>
    {
        [Header("Events")]
        public UnityEvent<float> onMasterVolumeChanged = new UnityEvent<float>();
        public UnityEvent<float> onMusicVolumeChanged = new UnityEvent<float>();
        public UnityEvent<float> onSFXVolumeChanged = new UnityEvent<float>();
        public UnityEvent<int> onGraphicsQualityChanged = new UnityEvent<int>();
        public UnityEvent<bool> onVibrationChanged = new UnityEvent<bool>();
        public UnityEvent<ControlScheme> onControlSchemeChanged = new UnityEvent<ControlScheme>();

        public enum ControlScheme
        {
            Touch,
            Tilt,
            Virtual Joystick
        }

        // Current settings (runtime cache)
        private float masterVolume;
        private float musicVolume;
        private float sfxVolume;
        private int graphicsQuality;
        private bool vibrationEnabled;
        private ControlScheme currentControlScheme;
        private int targetFrameRate;
        private bool batteryOptimizationMode;

        protected override void Awake()
        {
            base.Awake();
            LoadSettings();
            ApplySettings();
        }

        private void Start()
        {
            // Apply initial settings to relevant systems
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.SetMasterVolume(masterVolume);
                AudioManager.Instance.SetMusicVolume(musicVolume);
                AudioManager.Instance.SetSFXVolume(sfxVolume);
            }
        }

        /// <summary>
        /// Loads settings from SaveManager
        /// </summary>
        private void LoadSettings()
        {
            if (SaveManager.Instance != null)
            {
                GameSaveData saveData = SaveManager.Instance.GetSaveData();
                masterVolume = saveData.masterVolume;
                musicVolume = saveData.musicVolume;
                sfxVolume = saveData.sfxVolume;
                graphicsQuality = saveData.graphicsQuality;
                vibrationEnabled = saveData.enableVibration;
            }
            else
            {
                // Default settings
                masterVolume = 1f;
                musicVolume = 0.7f;
                sfxVolume = 1f;
                graphicsQuality = 2; // High quality
                vibrationEnabled = true;
            }

            // Load non-saved settings from PlayerPrefs
            currentControlScheme = (ControlScheme)PlayerPrefs.GetInt("ControlScheme", 0);
            targetFrameRate = PlayerPrefs.GetInt("TargetFrameRate", 60);
            batteryOptimizationMode = PlayerPrefs.GetInt("BatteryOptimization", 0) == 1;
        }

        /// <summary>
        /// Applies all settings to the game engine
        /// </summary>
        private void ApplySettings()
        {
            ApplyGraphicsSettings();
            ApplyPerformanceSettings();
        }

        /// <summary>
        /// Applies graphics quality settings
        /// </summary>
        private void ApplyGraphicsSettings()
        {
            QualitySettings.SetQualityLevel(graphicsQuality);

            // Custom quality adjustments for mobile
            switch (graphicsQuality)
            {
                case 0: // Low
                    QualitySettings.shadowDistance = 30f;
                    QualitySettings.shadowResolution = ShadowResolution.Low;
                    QualitySettings.antiAliasing = 0;
                    QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
                    break;

                case 1: // Medium
                    QualitySettings.shadowDistance = 50f;
                    QualitySettings.shadowResolution = ShadowResolution.Medium;
                    QualitySettings.antiAliasing = 2;
                    QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
                    break;

                case 2: // High
                    QualitySettings.shadowDistance = 75f;
                    QualitySettings.shadowResolution = ShadowResolution.High;
                    QualitySettings.antiAliasing = 4;
                    QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
                    break;
            }

            onGraphicsQualityChanged?.Invoke(graphicsQuality);
        }

        /// <summary>
        /// Applies performance and battery optimization settings
        /// </summary>
        private void ApplyPerformanceSettings()
        {
            if (batteryOptimizationMode)
            {
                Application.targetFrameRate = 30;
                QualitySettings.vSyncCount = 0;
            }
            else
            {
                Application.targetFrameRate = targetFrameRate;
                QualitySettings.vSyncCount = 1;
            }
        }

        // --- Audio Settings ---

        public void SetMasterVolume(float volume)
        {
            masterVolume = Mathf.Clamp01(volume);

            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.SetMasterVolume(masterVolume);
            }

            if (SaveManager.Instance != null)
            {
                SaveManager.Instance.GetSaveData().masterVolume = masterVolume;
                SaveManager.Instance.SaveGame();
            }

            onMasterVolumeChanged?.Invoke(masterVolume);
        }

        public void SetMusicVolume(float volume)
        {
            musicVolume = Mathf.Clamp01(volume);

            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.SetMusicVolume(musicVolume);
            }

            if (SaveManager.Instance != null)
            {
                SaveManager.Instance.GetSaveData().musicVolume = musicVolume;
                SaveManager.Instance.SaveGame();
            }

            onMusicVolumeChanged?.Invoke(musicVolume);
        }

        public void SetSFXVolume(float volume)
        {
            sfxVolume = Mathf.Clamp01(volume);

            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.SetSFXVolume(sfxVolume);
            }

            if (SaveManager.Instance != null)
            {
                SaveManager.Instance.GetSaveData().sfxVolume = sfxVolume;
                SaveManager.Instance.SaveGame();
            }

            onSFXVolumeChanged?.Invoke(sfxVolume);
        }

        public float GetMasterVolume() => masterVolume;
        public float GetMusicVolume() => musicVolume;
        public float GetSFXVolume() => sfxVolume;

        // --- Graphics Settings ---

        public void SetGraphicsQuality(int quality)
        {
            graphicsQuality = Mathf.Clamp(quality, 0, 2);
            ApplyGraphicsSettings();

            if (SaveManager.Instance != null)
            {
                SaveManager.Instance.GetSaveData().graphicsQuality = graphicsQuality;
                SaveManager.Instance.SaveGame();
            }
        }

        public int GetGraphicsQuality() => graphicsQuality;

        public string GetGraphicsQualityName()
        {
            switch (graphicsQuality)
            {
                case 0: return "Low";
                case 1: return "Medium";
                case 2: return "High";
                default: return "Unknown";
            }
        }

        // --- Control Settings ---

        public void SetControlScheme(ControlScheme scheme)
        {
            currentControlScheme = scheme;
            PlayerPrefs.SetInt("ControlScheme", (int)scheme);
            PlayerPrefs.Save();

            onControlSchemeChanged?.Invoke(scheme);
        }

        public ControlScheme GetControlScheme() => currentControlScheme;

        public void SetVibrationEnabled(bool enabled)
        {
            vibrationEnabled = enabled;

            if (SaveManager.Instance != null)
            {
                SaveManager.Instance.GetSaveData().enableVibration = enabled;
                SaveManager.Instance.SaveGame();
            }

            onVibrationChanged?.Invoke(enabled);
        }

        public bool IsVibrationEnabled() => vibrationEnabled;

        /// <summary>
        /// Triggers haptic feedback if vibration is enabled
        /// </summary>
        public void TriggerHaptic(HapticType type)
        {
            if (!vibrationEnabled) return;

#if UNITY_IOS || UNITY_ANDROID
            switch (type)
            {
                case HapticType.Light:
                    Handheld.Vibrate(); // iOS: Light impact
                    break;
                case HapticType.Medium:
                    Handheld.Vibrate(); // iOS: Medium impact
                    break;
                case HapticType.Heavy:
                    Handheld.Vibrate(); // iOS: Heavy impact
                    break;
                case HapticType.Success:
                    Handheld.Vibrate(); // iOS: Notification success
                    break;
                case HapticType.Warning:
                    Handheld.Vibrate(); // iOS: Notification warning
                    break;
                case HapticType.Error:
                    Handheld.Vibrate(); // iOS: Notification error
                    break;
            }
#endif
        }

        public enum HapticType
        {
            Light,
            Medium,
            Heavy,
            Success,
            Warning,
            Error
        }

        // --- Performance Settings ---

        public void SetTargetFrameRate(int frameRate)
        {
            targetFrameRate = frameRate;
            PlayerPrefs.SetInt("TargetFrameRate", frameRate);
            PlayerPrefs.Save();

            ApplyPerformanceSettings();
        }

        public int GetTargetFrameRate() => targetFrameRate;

        public void SetBatteryOptimizationMode(bool enabled)
        {
            batteryOptimizationMode = enabled;
            PlayerPrefs.SetInt("BatteryOptimization", enabled ? 1 : 0);
            PlayerPrefs.Save();

            ApplyPerformanceSettings();
        }

        public bool IsBatteryOptimizationEnabled() => batteryOptimizationMode;

        // --- Utility Methods ---

        /// <summary>
        /// Resets all settings to default values
        /// </summary>
        public void ResetToDefaults()
        {
            SetMasterVolume(1f);
            SetMusicVolume(0.7f);
            SetSFXVolume(1f);
            SetGraphicsQuality(2);
            SetVibrationEnabled(true);
            SetControlScheme(ControlScheme.Touch);
            SetTargetFrameRate(60);
            SetBatteryOptimizationMode(false);

            Debug.Log("[SettingsManager] All settings reset to defaults");
        }

        /// <summary>
        /// Auto-detects optimal graphics settings based on device performance
        /// </summary>
        public void AutoDetectGraphicsQuality()
        {
            // Check system memory
            int systemMemoryMB = SystemInfo.systemMemorySize;

            // Check GPU capability
            int graphicsMemoryMB = SystemInfo.graphicsMemorySize;

            // Determine quality based on device specs
            if (systemMemoryMB >= 4096 && graphicsMemoryMB >= 2048)
            {
                SetGraphicsQuality(2); // High
            }
            else if (systemMemoryMB >= 2048 && graphicsMemoryMB >= 1024)
            {
                SetGraphicsQuality(1); // Medium
            }
            else
            {
                SetGraphicsQuality(0); // Low
            }

            Debug.Log($"[SettingsManager] Auto-detected graphics quality: {GetGraphicsQualityName()} " +
                     $"(RAM: {systemMemoryMB}MB, VRAM: {graphicsMemoryMB}MB)");
        }

        /// <summary>
        /// Returns a summary of current settings for debugging
        /// </summary>
        public string GetSettingsSummary()
        {
            return $"Settings Summary:\n" +
                   $"- Master Volume: {masterVolume:F2}\n" +
                   $"- Music Volume: {musicVolume:F2}\n" +
                   $"- SFX Volume: {sfxVolume:F2}\n" +
                   $"- Graphics Quality: {GetGraphicsQualityName()}\n" +
                   $"- Control Scheme: {currentControlScheme}\n" +
                   $"- Vibration: {vibrationEnabled}\n" +
                   $"- Target FPS: {targetFrameRate}\n" +
                   $"- Battery Optimization: {batteryOptimizationMode}";
        }
    }
}
