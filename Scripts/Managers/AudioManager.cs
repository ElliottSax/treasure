using UnityEngine;
using System.Collections.Generic;

namespace TreasureExcavator
{
    /// <summary>
    /// Singleton audio manager for playing SFX and managing music.
    /// Handles audio mixing and volume control.
    /// Phase 1: Foundation - Week 1
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [Header("Audio Sources")]
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private int sfxPoolSize = 10;

        [Header("Volume Settings")]
        [SerializeField, Range(0f, 1f)] private float masterVolume = 1f;
        [SerializeField, Range(0f, 1f)] private float musicVolume = 0.7f;
        [SerializeField, Range(0f, 1f)] private float sfxVolume = 1f;

        [Header("Audio Clips - Gameplay")]
        [SerializeField] private AudioClip collectSmall;
        [SerializeField] private AudioClip collectMedium;
        [SerializeField] private AudioClip collectLarge;
        [SerializeField] private AudioClip passGateBronze;
        [SerializeField] private AudioClip passGateSilver;
        [SerializeField] private AudioClip passGateGold;
        [SerializeField] private AudioClip depositTreasure;

        [Header("Audio Clips - UI")]
        [SerializeField] private AudioClip buttonClick;
        [SerializeField] private AudioClip levelComplete;
        [SerializeField] private AudioClip starEarned;

        [Header("Music Tracks")]
        [SerializeField] private AudioClip mainMenuMusic;
        [SerializeField] private AudioClip gameplayMusic;
        [SerializeField] private AudioClip victoryMusic;

        // SFX pool for one-shots
        private List<AudioSource> sfxPool;
        private int currentPoolIndex = 0;

        // Music crossfade
        private bool isCrossfading;
        private float crossfadeDuration = 1f;

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

            // Initialize SFX pool
            InitializeSFXPool();

            // Load saved volume settings
            LoadVolumeSettings();
        }

        private void Start()
        {
            // Apply volume settings to sources
            UpdateVolumes();
        }

        private void InitializeSFXPool()
        {
            sfxPool = new List<AudioSource>();

            for (int i = 0; i < sfxPoolSize; i++)
            {
                GameObject sfxObject = new GameObject($"SFX_Pool_{i}");
                sfxObject.transform.SetParent(transform);
                AudioSource source = sfxObject.AddComponent<AudioSource>();
                source.playOnAwake = false;
                sfxPool.Add(source);
            }
        }

        #region SFX Playback

        public void PlaySFX(AudioClip clip, float volumeMultiplier = 1f)
        {
            if (clip == null) return;

            // Get next available audio source from pool
            AudioSource source = sfxPool[currentPoolIndex];
            currentPoolIndex = (currentPoolIndex + 1) % sfxPoolSize;

            // Play clip
            source.clip = clip;
            source.volume = sfxVolume * masterVolume * volumeMultiplier;
            source.Play();
        }

        public void PlaySFXAtPosition(AudioClip clip, Vector3 position, float volumeMultiplier = 1f)
        {
            if (clip == null) return;

            AudioSource.PlayClipAtPoint(clip, position, sfxVolume * masterVolume * volumeMultiplier);
        }

        // Gameplay SFX convenience methods
        public void PlayCollectSmall() => PlaySFX(collectSmall);
        public void PlayCollectMedium() => PlaySFX(collectMedium);
        public void PlayCollectLarge() => PlaySFX(collectLarge);
        public void PlayPassGate(int multiplier)
        {
            switch (multiplier)
            {
                case 2: PlaySFX(passGateBronze); break;
                case 3: PlaySFX(passGateSilver); break;
                case 5: PlaySFX(passGateGold); break;
                default: PlaySFX(passGateBronze); break;
            }
        }
        public void PlayDepositTreasure() => PlaySFX(depositTreasure);

        // UI SFX convenience methods
        public void PlayButtonClick() => PlaySFX(buttonClick);
        public void PlayLevelComplete() => PlaySFX(levelComplete);
        public void PlayStarEarned() => PlaySFX(starEarned);

        #endregion

        #region Music Playback

        public void PlayMusic(AudioClip clip, bool loop = true, bool crossfade = true)
        {
            if (clip == null) return;

            if (crossfade && musicSource.isPlaying)
            {
                StartCoroutine(CrossfadeMusic(clip, loop));
            }
            else
            {
                musicSource.clip = clip;
                musicSource.loop = loop;
                musicSource.volume = musicVolume * masterVolume;
                musicSource.Play();
            }
        }

        private System.Collections.IEnumerator CrossfadeMusic(AudioClip newClip, bool loop)
        {
            isCrossfading = true;
            float elapsed = 0f;
            float startVolume = musicSource.volume;

            // Fade out current music
            while (elapsed < crossfadeDuration / 2f)
            {
                elapsed += Time.deltaTime;
                musicSource.volume = Mathf.Lerp(startVolume, 0f, elapsed / (crossfadeDuration / 2f));
                yield return null;
            }

            // Switch to new track
            musicSource.clip = newClip;
            musicSource.loop = loop;
            musicSource.Play();

            // Fade in new music
            elapsed = 0f;
            while (elapsed < crossfadeDuration / 2f)
            {
                elapsed += Time.deltaTime;
                musicSource.volume = Mathf.Lerp(0f, musicVolume * masterVolume, elapsed / (crossfadeDuration / 2f));
                yield return null;
            }

            musicSource.volume = musicVolume * masterVolume;
            isCrossfading = false;
        }

        public void StopMusic(bool fadeOut = true)
        {
            if (fadeOut)
            {
                StartCoroutine(FadeOutMusic());
            }
            else
            {
                musicSource.Stop();
            }
        }

        private System.Collections.IEnumerator FadeOutMusic()
        {
            float elapsed = 0f;
            float startVolume = musicSource.volume;

            while (elapsed < crossfadeDuration)
            {
                elapsed += Time.deltaTime;
                musicSource.volume = Mathf.Lerp(startVolume, 0f, elapsed / crossfadeDuration);
                yield return null;
            }

            musicSource.Stop();
            musicSource.volume = musicVolume * masterVolume;
        }

        // Music track convenience methods
        public void PlayMainMenuMusic() => PlayMusic(mainMenuMusic, loop: true);
        public void PlayGameplayMusic() => PlayMusic(gameplayMusic, loop: true);
        public void PlayVictoryMusic() => PlayMusic(victoryMusic, loop: false);

        #endregion

        #region Volume Control

        public void SetMasterVolume(float volume)
        {
            masterVolume = Mathf.Clamp01(volume);
            UpdateVolumes();
            SaveVolumeSettings();
        }

        public void SetMusicVolume(float volume)
        {
            musicVolume = Mathf.Clamp01(volume);
            UpdateVolumes();
            SaveVolumeSettings();
        }

        public void SetSFXVolume(float volume)
        {
            sfxVolume = Mathf.Clamp01(volume);
            UpdateVolumes();
            SaveVolumeSettings();
        }

        private void UpdateVolumes()
        {
            if (musicSource != null)
            {
                musicSource.volume = musicVolume * masterVolume;
            }

            // SFX sources will use sfxVolume * masterVolume when played
        }

        public float GetMasterVolume() => masterVolume;
        public float GetMusicVolume() => musicVolume;
        public float GetSFXVolume() => sfxVolume;

        #endregion

        #region Settings Persistence

        private void LoadVolumeSettings()
        {
            masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
            musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.7f);
            sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        }

        private void SaveVolumeSettings()
        {
            PlayerPrefs.SetFloat("MasterVolume", masterVolume);
            PlayerPrefs.SetFloat("MusicVolume", musicVolume);
            PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
            PlayerPrefs.Save();
        }

        #endregion

        // Mute toggle
        public void ToggleMute()
        {
            bool isMuted = masterVolume == 0f;
            SetMasterVolume(isMuted ? 1f : 0f);
        }
    }
}
