using UnityEngine;

namespace TreasureExcavator.Gameplay
{
    /// <summary>
    /// Individual treasure item that can be collected by the vehicle.
    /// Handles collection animation and value.
    /// Phase 1: Foundation - Week 2
    /// </summary>
    public class Treasure : MonoBehaviour
    {
        [Header("Treasure Settings")]
        [SerializeField] private TreasureType type = TreasureType.Small;
        [SerializeField] private int value = 10;

        [Header("Visual Effects")]
        [SerializeField] private ParticleSystem collectParticles;
        [SerializeField] private float floatHeight = 0.3f;
        [SerializeField] private float floatSpeed = 2f;
        [SerializeField] private float rotationSpeed = 50f;

        [Header("Collection")]
        [SerializeField] private float collectionSpeed = 10f;
        [SerializeField] private AnimationCurve collectionCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        // State
        private Vector3 startPosition;
        private bool isCollected = false;
        private Transform collectTarget;
        private float collectionProgress = 0f;

        public enum TreasureType
        {
            Small,      // 10 points
            Medium,     // 50 points
            Large       // 200 points
        }

        private void Start()
        {
            startPosition = transform.position;
        }

        private void Update()
        {
            if (isCollected)
            {
                UpdateCollection();
            }
            else
            {
                UpdateIdle();
            }
        }

        private void UpdateIdle()
        {
            // Gentle floating animation
            float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            // Gentle rotation
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }

        private void UpdateCollection()
        {
            if (collectTarget == null)
            {
                Destroy(gameObject);
                return;
            }

            // Move towards collection target with curve
            collectionProgress += Time.deltaTime * collectionSpeed;
            float curvedProgress = collectionCurve.Evaluate(Mathf.Clamp01(collectionProgress));

            transform.position = Vector3.Lerp(
                startPosition,
                collectTarget.position,
                curvedProgress
            );

            // Scale down as it gets collected
            float scale = Mathf.Lerp(1f, 0.2f, curvedProgress);
            transform.localScale = Vector3.one * scale;

            // Destroy when collection complete
            if (collectionProgress >= 1f)
            {
                OnCollectionComplete();
            }
        }

        public void Collect(Transform target)
        {
            if (isCollected) return;

            isCollected = true;
            collectTarget = target;
            collectionProgress = 0f;

            // Play collection particle effect
            if (collectParticles != null)
            {
                collectParticles.Play();
            }

            // Play collection sound based on type
            if (AudioManager.Instance != null)
            {
                switch (type)
                {
                    case TreasureType.Small:
                        AudioManager.Instance.PlayCollectSmall();
                        break;
                    case TreasureType.Medium:
                        AudioManager.Instance.PlayCollectMedium();
                        break;
                    case TreasureType.Large:
                        AudioManager.Instance.PlayCollectLarge();
                        break;
                }
            }
        }

        private void OnCollectionComplete()
        {
            // Notify cargo system (will be implemented in Phase 2)
            // For now, just destroy
            Destroy(gameObject);
        }

        // Public getters
        public int GetValue() => value;
        public TreasureType GetTreasureType() => type;
        public bool IsCollected() => isCollected;

        // Trigger detection for auto-collection
        private void OnTriggerEnter(Collider other)
        {
            // Check if vehicle entered collection radius
            if (other.CompareTag("Vehicle") && !isCollected)
            {
                CargoManager cargoManager = other.GetComponent<CargoManager>();
                if (cargoManager != null && cargoManager.CanCollect())
                {
                    // Collect this treasure
                    Collect(other.transform);
                    cargoManager.AddTreasure(this);
                }
            }
        }
    }
}
