using UnityEngine;

namespace TreasureExcavator.Vehicles
{
    /// <summary>
    /// Core vehicle controller handling movement, physics, and input.
    /// Supports touch and tilt controls for mobile.
    /// Phase 1: Foundation - Week 1
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class VehicleController : MonoBehaviour
    {
        [Header("Vehicle Stats")]
        [SerializeField] private float speed = 8f;
        [SerializeField] private float acceleration = 5f;
        [SerializeField] private float turningSpeed = 180f;
        [SerializeField] private float drag = 0.5f;
        [SerializeField] private float angularDrag = 2f;

        [Header("Ground Check")]
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float groundCheckDistance = 0.5f;
        [SerializeField] private Transform groundCheckPoint;

        [Header("Audio")]
        [SerializeField] private AudioSource engineAudioSource;
        [SerializeField] private float minEnginePitch = 0.8f;
        [SerializeField] private float maxEnginePitch = 1.5f;

        [Header("VFX")]
        [SerializeField] private ParticleSystem exhaustParticles;
        [SerializeField] private ParticleSystem dustParticles;

        // Components
        private Rigidbody rb;
        private InputManager inputManager;

        // State
        private bool isGrounded;
        private Vector2 moveInput;
        private float currentSpeed;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            inputManager = InputManager.Instance;

            // Configure rigidbody
            rb.mass = 1000f; // kg
            rb.drag = drag;
            rb.angularDrag = angularDrag;
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }

        private void Start()
        {
            // Start engine audio loop
            if (engineAudioSource != null)
            {
                engineAudioSource.loop = true;
                engineAudioSource.Play();
            }

            // Start exhaust particles
            if (exhaustParticles != null)
            {
                exhaustParticles.Play();
            }
        }

        private void Update()
        {
            // Get input from InputManager
            if (inputManager != null)
            {
                moveInput = inputManager.GetMoveInput();
            }

            // Update audio based on speed
            UpdateEngineAudio();

            // Update VFX
            UpdateVFX();
        }

        private void FixedUpdate()
        {
            // Check if grounded
            CheckGrounded();

            if (isGrounded)
            {
                // Apply movement
                Move();
                Turn();
            }

            // Track current speed
            currentSpeed = rb.velocity.magnitude;
        }

        private void Move()
        {
            // Forward/backward input (vertical axis)
            float forwardInput = moveInput.y;

            if (Mathf.Abs(forwardInput) > 0.1f)
            {
                // Calculate target velocity
                Vector3 targetVelocity = transform.forward * forwardInput * speed;

                // Smoothly accelerate towards target velocity
                Vector3 velocityChange = targetVelocity - rb.velocity;
                velocityChange.x = Mathf.Clamp(velocityChange.x, -acceleration, acceleration);
                velocityChange.z = Mathf.Clamp(velocityChange.z, -acceleration, acceleration);
                velocityChange.y = 0; // Don't modify vertical velocity

                rb.AddForce(velocityChange, ForceMode.VelocityChange);
            }
        }

        private void Turn()
        {
            // Left/right input (horizontal axis)
            float turnInput = moveInput.x;

            if (Mathf.Abs(turnInput) > 0.1f && currentSpeed > 0.5f)
            {
                // Turn only when moving
                float turnAmount = turnInput * turningSpeed * Time.fixedDeltaTime;
                Quaternion turnRotation = Quaternion.Euler(0f, turnAmount, 0f);
                rb.MoveRotation(rb.rotation * turnRotation);
            }
        }

        private void CheckGrounded()
        {
            // Raycast down to check if vehicle is on ground
            if (groundCheckPoint != null)
            {
                isGrounded = Physics.Raycast(
                    groundCheckPoint.position,
                    Vector3.down,
                    groundCheckDistance,
                    groundLayer
                );
            }
            else
            {
                // Fallback: check from vehicle center
                isGrounded = Physics.Raycast(
                    transform.position,
                    Vector3.down,
                    groundCheckDistance + 0.5f,
                    groundLayer
                );
            }
        }

        private void UpdateEngineAudio()
        {
            if (engineAudioSource == null) return;

            // Pitch increases with speed
            float speedNormalized = Mathf.Clamp01(currentSpeed / speed);
            float targetPitch = Mathf.Lerp(minEnginePitch, maxEnginePitch, speedNormalized);
            engineAudioSource.pitch = Mathf.Lerp(engineAudioSource.pitch, targetPitch, Time.deltaTime * 5f);
        }

        private void UpdateVFX()
        {
            // Show dust particles when moving on ground
            if (dustParticles != null)
            {
                if (isGrounded && currentSpeed > 1f)
                {
                    if (!dustParticles.isPlaying)
                        dustParticles.Play();
                }
                else
                {
                    if (dustParticles.isPlaying)
                        dustParticles.Stop();
                }
            }
        }

        // Public API for other systems
        public float GetSpeed() => currentSpeed;
        public float GetSpeedNormalized() => currentSpeed / speed;
        public bool IsGrounded() => isGrounded;
        public bool IsMoving() => currentSpeed > 0.5f;

        // Debug visualization
        private void OnDrawGizmosSelected()
        {
            // Draw ground check ray
            if (groundCheckPoint != null)
            {
                Gizmos.color = isGrounded ? Color.green : Color.red;
                Gizmos.DrawRay(groundCheckPoint.position, Vector3.down * groundCheckDistance);
            }
        }
    }
}
