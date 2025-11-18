using UnityEngine;

namespace TreasureExcavator.Utilities
{
    /// <summary>
    /// Static utility class for common game calculations and helpers.
    /// Phase 1: Foundation - Utilities
    /// </summary>
    public static class GameUtilities
    {
        /// <summary>
        /// Calculate star rating based on score and targets.
        /// </summary>
        public static int CalculateStarRating(int currentScore, int oneStarTarget, int twoStarTarget, int threeStarTarget)
        {
            if (currentScore >= threeStarTarget) return 3;
            if (currentScore >= twoStarTarget) return 2;
            if (currentScore >= oneStarTarget) return 1;
            return 0;
        }

        /// <summary>
        /// Calculate gold reward based on level and stars.
        /// </summary>
        public static int CalculateGoldReward(int level, int stars)
        {
            int baseReward = 50 + ((level - 1) * 10);
            return baseReward * stars;
        }

        /// <summary>
        /// Format large numbers with abbreviations (K, M, B).
        /// </summary>
        public static string FormatNumber(int number)
        {
            if (number >= 1000000000)
                return (number / 1000000000f).ToString("0.#") + "B";
            if (number >= 1000000)
                return (number / 1000000f).ToString("0.#") + "M";
            if (number >= 1000)
                return (number / 1000f).ToString("0.#") + "K";
            return number.ToString();
        }

        /// <summary>
        /// Format time in MM:SS format.
        /// </summary>
        public static string FormatTime(float seconds)
        {
            int minutes = Mathf.FloorToInt(seconds / 60f);
            int secs = Mathf.FloorToInt(seconds % 60f);
            return string.Format("{0:00}:{1:00}", minutes, secs);
        }

        /// <summary>
        /// Lerp with custom easing curve.
        /// </summary>
        public static float EasedLerp(float a, float b, float t, AnimationCurve curve)
        {
            if (curve == null)
                return Mathf.Lerp(a, b, t);

            return Mathf.Lerp(a, b, curve.Evaluate(t));
        }

        /// <summary>
        /// Check if a layer is in a layer mask.
        /// </summary>
        public static bool LayerInMask(int layer, LayerMask mask)
        {
            return mask == (mask | (1 << layer));
        }

        /// <summary>
        /// Get random point within a circle (for spawning).
        /// </summary>
        public static Vector3 RandomPointInCircle(Vector3 center, float radius)
        {
            Vector2 randomPoint = Random.insideUnitCircle * radius;
            return center + new Vector3(randomPoint.x, 0f, randomPoint.y);
        }

        /// <summary>
        /// Clamp angle to -180 to 180 range.
        /// </summary>
        public static float ClampAngle(float angle)
        {
            while (angle > 180f) angle -= 360f;
            while (angle < -180f) angle += 360f;
            return angle;
        }

        /// <summary>
        /// Calculate velocity needed to reach target height with gravity.
        /// </summary>
        public static float CalculateJumpVelocity(float targetHeight)
        {
            return Mathf.Sqrt(2f * Mathf.Abs(Physics.gravity.y) * targetHeight);
        }

        /// <summary>
        /// Smooth damp angle (for rotation).
        /// </summary>
        public static float SmoothDampAngle(float current, float target, ref float velocity, float smoothTime)
        {
            return Mathf.SmoothDampAngle(current, target, ref velocity, smoothTime);
        }
    }
}
