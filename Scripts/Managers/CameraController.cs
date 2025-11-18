using UnityEngine;

namespace TreasureExcavator
{
    /// <summary>
    /// Smooth camera follow system for vehicle tracking.
    /// Supports offset positioning and smooth damping.
    /// Phase 1: Foundation - Week 1
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        [Header("Target")]
        [SerializeField] private Transform target;

        [Header("Camera Settings")]
        [SerializeField] private Vector3 offset = new Vector3(0f, 8f, -10f);
        [SerializeField] private float followSpeed = 5f;
        [SerializeField] private float rotationSpeed = 3f;
        [SerializeField] private bool lookAtTarget = true;

        [Header("Bounds (Optional)")]
        [SerializeField] private bool useBounds = false;
        [SerializeField] private Vector3 boundsMin;
        [SerializeField] private Vector3 boundsMax;

        [Header("Shake Effect")]
        [SerializeField] private float shakeAmplitude = 0.1f;
        [SerializeField] private float shakeFrequency = 1f;

        // Shake state
        private float shakeTimer = 0f;
        private Vector3 shakeOffset;

        private void LateUpdate()
        {
            if (target == null) return;

            // Calculate desired position
            Vector3 desiredPosition = target.position + offset;

            // Apply bounds if enabled
            if (useBounds)
            {
                desiredPosition.x = Mathf.Clamp(desiredPosition.x, boundsMin.x, boundsMax.x);
                desiredPosition.y = Mathf.Clamp(desiredPosition.y, boundsMin.y, boundsMax.y);
                desiredPosition.z = Mathf.Clamp(desiredPosition.z, boundsMin.z, boundsMax.z);
            }

            // Update shake
            UpdateShake();

            // Smooth follow
            transform.position = Vector3.Lerp(
                transform.position,
                desiredPosition + shakeOffset,
                followSpeed * Time.deltaTime
            );

            // Look at target
            if (lookAtTarget)
            {
                Vector3 lookPosition = target.position;
                Quaternion targetRotation = Quaternion.LookRotation(lookPosition - transform.position);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime
                );
            }
        }

        private void UpdateShake()
        {
            if (shakeTimer > 0f)
            {
                shakeTimer -= Time.deltaTime;

                // Generate shake offset using Perlin noise
                float x = (Mathf.PerlinNoise(Time.time * shakeFrequency, 0f) - 0.5f) * 2f * shakeAmplitude;
                float y = (Mathf.PerlinNoise(0f, Time.time * shakeFrequency) - 0.5f) * 2f * shakeAmplitude;
                float z = (Mathf.PerlinNoise(Time.time * shakeFrequency, Time.time * shakeFrequency) - 0.5f) * 2f * shakeAmplitude;

                shakeOffset = new Vector3(x, y, z);
            }
            else
            {
                shakeOffset = Vector3.Lerp(shakeOffset, Vector3.zero, Time.deltaTime * 5f);
            }
        }

        // Public API
        public void SetTarget(Transform newTarget)
        {
            target = newTarget;
        }

        public void Shake(float duration, float amplitude = 0.1f)
        {
            shakeTimer = duration;
            shakeAmplitude = amplitude;
        }

        public void SetOffset(Vector3 newOffset)
        {
            offset = newOffset;
        }

        // Debug visualization
        private void OnDrawGizmosSelected()
        {
            if (useBounds)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireCube(
                    (boundsMin + boundsMax) / 2f,
                    boundsMax - boundsMin
                );
            }

            // Draw camera target line
            if (target != null)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawLine(transform.position, target.position);
            }
        }
    }
}
