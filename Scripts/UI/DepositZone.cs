using UnityEngine;
using UnityEngine.Events;

namespace TreasureExcavator.Gameplay
{
    /// <summary>
    /// Deposit zone where players deliver treasures to score points.
    /// Phase 1: Foundation - Week 2
    /// </summary>
    public class DepositZone : MonoBehaviour
    {
        [Header("Visual Effects")]
        [SerializeField] private ParticleSystem depositParticles;
        [SerializeField] private Light glowLight;
        [SerializeField] private float pulsateSpeed = 2f;
        [SerializeField] private float pulsateAmount = 0.5f;

        [Header("Audio")]
        [SerializeField] private AudioClip depositSound;

        [Header("Events")]
        public UnityEvent<int> onDeposit;

        // State
        private float baseLightIntensity;
        private float baseParticleRate;

        private void Start()
        {
            // Store base values
            if (glowLight != null)
            {
                baseLightIntensity = glowLight.intensity;
            }

            if (depositParticles != null)
            {
                var emission = depositParticles.emission;
                baseParticleRate = emission.rateOverTime.constant;
            }
        }

        private void Update()
        {
            // Pulsating glow effect
            if (glowLight != null)
            {
                float pulsate = Mathf.Sin(Time.time * pulsateSpeed) * pulsateAmount;
                glowLight.intensity = baseLightIntensity + pulsate;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            // Check if vehicle entered deposit zone
            if (other.CompareTag("Vehicle"))
            {
                CargoManager cargoManager = other.GetComponent<CargoManager>();
                if (cargoManager != null && !cargoManager.IsEmpty())
                {
                    // Deposit cargo
                    ProcessDeposit(cargoManager);
                }
            }
        }

        private void ProcessDeposit(CargoManager cargoManager)
        {
            // Get score from cargo (no multiplier in deposit zone)
            int scoreEarned = cargoManager.DepositCargo(multiplier: 1);

            // Add score to game
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(scoreEarned);
            }

            // Play effects
            PlayDepositEffects();

            // Invoke event
            onDeposit?.Invoke(scoreEarned);

            Debug.Log($"Deposited cargo for {scoreEarned} points!");
        }

        private void PlayDepositEffects()
        {
            // Play particle burst
            if (depositParticles != null)
            {
                depositParticles.Play();
            }

            // Play sound
            if (depositSound != null && AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySFX(depositSound);
            }
            else if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayDepositTreasure();
            }

            // Flash light
            if (glowLight != null)
            {
                StartCoroutine(FlashLight());
            }
        }

        private System.Collections.IEnumerator FlashLight()
        {
            float originalIntensity = glowLight.intensity;
            glowLight.intensity = originalIntensity * 2f;

            yield return new WaitForSeconds(0.2f);

            glowLight.intensity = originalIntensity;
        }

        // Debug visualization
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, 5f); // Approximate trigger radius

            // Draw label
            #if UNITY_EDITOR
            UnityEditor.Handles.Label(
                transform.position + Vector3.up * 3f,
                "DEPOSIT ZONE"
            );
            #endif
        }
    }
}
