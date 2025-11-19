using System.Collections.Generic;
using UnityEngine;

namespace TreasureExcavator
{
    /// <summary>
    /// Centralized manager for particle effects with pooling and performance optimization.
    /// Handles spawning, playing, and recycling of particle systems.
    /// </summary>
    public class ParticleEffectsManager : Singleton<ParticleEffectsManager>
    {
        [Header("Particle Effect Prefabs")]
        [SerializeField] private ParticleEffectData[] particleEffects;

        [Header("Pooling Settings")]
        [SerializeField] private int defaultPoolSize = 5;
        [SerializeField] private int maxPoolSize = 20;
        [SerializeField] private Transform effectsParent;

        private Dictionary<string, Queue<ParticleSystem>> effectPools;
        private Dictionary<string, GameObject> effectPrefabs;
        private List<ParticleSystem> activeEffects;

        protected override void Awake()
        {
            base.Awake();
            InitializeEffectPools();

            if (effectsParent == null)
            {
                effectsParent = new GameObject("ParticleEffects").transform;
                effectsParent.SetParent(transform);
            }

            activeEffects = new List<ParticleSystem>();
        }

        /// <summary>
        /// Initializes particle effect pools
        /// </summary>
        private void InitializeEffectPools()
        {
            effectPools = new Dictionary<string, Queue<ParticleSystem>>();
            effectPrefabs = new Dictionary<string, GameObject>();

            // Create default effect entries if none assigned
            if (particleEffects == null || particleEffects.Length == 0)
            {
                CreateDefaultEffectData();
            }

            // Create pools for each effect type
            foreach (ParticleEffectData effectData in particleEffects)
            {
                if (effectData.prefab != null)
                {
                    effectPrefabs[effectData.effectName] = effectData.prefab;
                    effectPools[effectData.effectName] = new Queue<ParticleSystem>();

                    // Pre-instantiate pool objects
                    for (int i = 0; i < defaultPoolSize; i++)
                    {
                        CreatePooledEffect(effectData.effectName);
                    }
                }
            }

            Debug.Log($"[ParticleEffectsManager] Initialized {effectPools.Count} particle effect pools");
        }

        /// <summary>
        /// Creates default particle effect data (placeholders for actual prefabs)
        /// </summary>
        private void CreateDefaultEffectData()
        {
            particleEffects = new ParticleEffectData[]
            {
                new ParticleEffectData { effectName = "TreasureCollect", prefab = null },
                new ParticleEffectData { effectName = "TreasureDeposit", prefab = null },
                new ParticleEffectData { effectName = "GateActivate", prefab = null },
                new ParticleEffectData { effectName = "PowerUpCollect", prefab = null },
                new ParticleEffectData { effectName = "LevelComplete", prefab = null },
                new ParticleEffectData { effectName = "Explosion", prefab = null },
                new ParticleEffectData { effectName = "Hit", prefab = null },
                new ParticleEffectData { effectName = "Sparkle", prefab = null },
                new ParticleEffectData { effectName = "Dust", prefab = null },
                new ParticleEffectData { effectName = "Smoke", prefab = null },
                new ParticleEffectData { effectName = "Stars", prefab = null },
                new ParticleEffectData { effectName = "ComboMilestone", prefab = null },
            };
        }

        /// <summary>
        /// Creates a pooled particle effect instance
        /// </summary>
        private ParticleSystem CreatePooledEffect(string effectName)
        {
            if (!effectPrefabs.ContainsKey(effectName))
            {
                Debug.LogWarning($"[ParticleEffectsManager] No prefab found for effect: {effectName}");
                return null;
            }

            GameObject prefab = effectPrefabs[effectName];
            GameObject effectObj = Instantiate(prefab, effectsParent);
            effectObj.name = $"{effectName}_Pooled";
            effectObj.SetActive(false);

            ParticleSystem particleSystem = effectObj.GetComponent<ParticleSystem>();
            if (particleSystem == null)
            {
                particleSystem = effectObj.AddComponent<ParticleSystem>();
            }

            // Configure for pooling
            var main = particleSystem.main;
            main.stopAction = ParticleSystemStopAction.Disable;

            effectPools[effectName].Enqueue(particleSystem);
            return particleSystem;
        }

        /// <summary>
        /// Plays a particle effect at a position
        /// </summary>
        public ParticleSystem PlayEffect(string effectName, Vector3 position, Quaternion rotation = default, Transform parent = null)
        {
            if (rotation == default)
            {
                rotation = Quaternion.identity;
            }

            ParticleSystem effect = GetPooledEffect(effectName);
            if (effect == null)
            {
                return null;
            }

            // Setup effect
            effect.transform.position = position;
            effect.transform.rotation = rotation;
            effect.transform.SetParent(parent ?? effectsParent);
            effect.gameObject.SetActive(true);

            // Play effect
            effect.Clear();
            effect.Play();

            activeEffects.Add(effect);

            // Schedule return to pool
            StartCoroutine(ReturnToPoolWhenFinished(effect, effectName));

            return effect;
        }

        /// <summary>
        /// Plays effect at position with color override
        /// </summary>
        public ParticleSystem PlayEffect(string effectName, Vector3 position, Color color)
        {
            ParticleSystem effect = PlayEffect(effectName, position);

            if (effect != null)
            {
                var main = effect.main;
                main.startColor = color;
            }

            return effect;
        }

        /// <summary>
        /// Plays effect and attaches to transform
        /// </summary>
        public ParticleSystem PlayEffectAttached(string effectName, Transform parent, Vector3 localPosition = default)
        {
            ParticleSystem effect = GetPooledEffect(effectName);
            if (effect == null)
            {
                return null;
            }

            effect.transform.SetParent(parent);
            effect.transform.localPosition = localPosition;
            effect.transform.localRotation = Quaternion.identity;
            effect.gameObject.SetActive(true);

            effect.Clear();
            effect.Play();

            activeEffects.Add(effect);
            StartCoroutine(ReturnToPoolWhenFinished(effect, effectName));

            return effect;
        }

        /// <summary>
        /// Gets a pooled effect or creates one if pool is empty
        /// </summary>
        private ParticleSystem GetPooledEffect(string effectName)
        {
            if (!effectPools.ContainsKey(effectName))
            {
                Debug.LogWarning($"[ParticleEffectsManager] No pool exists for effect: {effectName}");
                return null;
            }

            Queue<ParticleSystem> pool = effectPools[effectName];

            // Get from pool or create new if empty
            if (pool.Count > 0)
            {
                return pool.Dequeue();
            }
            else
            {
                // Check if we can create more
                if (pool.Count < maxPoolSize)
                {
                    Debug.Log($"[ParticleEffectsManager] Pool empty for {effectName}, creating new instance");
                    return CreatePooledEffect(effectName);
                }
                else
                {
                    Debug.LogWarning($"[ParticleEffectsManager] Max pool size reached for {effectName}");
                    return null;
                }
            }
        }

        /// <summary>
        /// Coroutine to return effect to pool when finished
        /// </summary>
        private System.Collections.IEnumerator ReturnToPoolWhenFinished(ParticleSystem effect, string effectName)
        {
            // Wait for particle system to finish
            while (effect.isPlaying)
            {
                yield return null;
            }

            // Return to pool
            ReturnEffectToPool(effect, effectName);
        }

        /// <summary>
        /// Returns effect to pool for reuse
        /// </summary>
        private void ReturnEffectToPool(ParticleSystem effect, string effectName)
        {
            if (effect == null) return;

            effect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            effect.gameObject.SetActive(false);
            effect.transform.SetParent(effectsParent);

            activeEffects.Remove(effect);

            if (effectPools.ContainsKey(effectName))
            {
                effectPools[effectName].Enqueue(effect);
            }
        }

        /// <summary>
        /// Stops all active particle effects
        /// </summary>
        public void StopAllEffects()
        {
            foreach (ParticleSystem effect in activeEffects.ToArray())
            {
                if (effect != null)
                {
                    effect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                }
            }

            activeEffects.Clear();
        }

        /// <summary>
        /// Clears all particle effect pools
        /// </summary>
        public void ClearAllPools()
        {
            StopAllEffects();

            foreach (var pool in effectPools.Values)
            {
                while (pool.Count > 0)
                {
                    ParticleSystem effect = pool.Dequeue();
                    if (effect != null)
                    {
                        Destroy(effect.gameObject);
                    }
                }
            }

            effectPools.Clear();
        }

        /// <summary>
        /// Gets pool statistics for debugging
        /// </summary>
        public string GetPoolStats()
        {
            string stats = "Particle Effect Pool Statistics:\n";

            foreach (var kvp in effectPools)
            {
                stats += $"- {kvp.Key}: {kvp.Value.Count} available\n";
            }

            stats += $"\nActive Effects: {activeEffects.Count}";

            return stats;
        }

        // --- Convenience Methods for Common Effects ---

        public void PlayTreasureCollectEffect(Vector3 position)
        {
            PlayEffect("TreasureCollect", position);
        }

        public void PlayTreasureDepositEffect(Vector3 position)
        {
            PlayEffect("TreasureDeposit", position);
        }

        public void PlayGateActivateEffect(Vector3 position, Color gateColor)
        {
            PlayEffect("GateActivate", position, gateColor);
        }

        public void PlayPowerUpCollectEffect(Vector3 position, Color powerUpColor)
        {
            PlayEffect("PowerUpCollect", position, powerUpColor);
        }

        public void PlayLevelCompleteEffect(Vector3 position)
        {
            PlayEffect("LevelComplete", position);
        }

        public void PlayExplosionEffect(Vector3 position)
        {
            PlayEffect("Explosion", position);
        }

        public void PlayHitEffect(Vector3 position)
        {
            PlayEffect("Hit", position);
        }

        public void PlayComboMilestoneEffect(Vector3 position)
        {
            PlayEffect("ComboMilestone", position);
        }

        public void PlaySparkleEffect(Transform parent)
        {
            PlayEffectAttached("Sparkle", parent);
        }

        public void PlayDustEffect(Vector3 position)
        {
            PlayEffect("Dust", position);
        }
    }

    /// <summary>
    /// Data structure for particle effect configuration
    /// </summary>
    [System.Serializable]
    public class ParticleEffectData
    {
        public string effectName;
        public GameObject prefab;
        public bool looping = false;
        public float lifetime = 2f;
        [Range(0f, 1f)]
        public float volume = 1f;
    }
}
