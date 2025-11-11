using UnityEngine;

namespace Inkblade.Utils
{
    /// <summary>
    /// Debug helper utilities for development.
    /// </summary>
    public static class DebugHelper
    {
        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void Log(string message)
        {
            Debug.Log($"[INKBLADE] {message}");
        }

        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void LogWarning(string message)
        {
            Debug.LogWarning($"[INKBLADE] {message}");
        }

        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void LogError(string message)
        {
            Debug.LogError($"[INKBLADE] {message}");
        }

        /// <summary>
        /// Draw a debug circle in the scene view.
        /// </summary>
        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void DrawCircle(Vector3 center, float radius, Color color, float duration = 0f)
        {
#if UNITY_EDITOR
            int segments = 32;
            float angleStep = 360f / segments;

            for (int i = 0; i < segments; i++)
            {
                float angle1 = i * angleStep * Mathf.Deg2Rad;
                float angle2 = (i + 1) * angleStep * Mathf.Deg2Rad;

                Vector3 point1 = center + new Vector3(Mathf.Cos(angle1), Mathf.Sin(angle1), 0) * radius;
                Vector3 point2 = center + new Vector3(Mathf.Cos(angle2), Mathf.Sin(angle2), 0) * radius;

                Debug.DrawLine(point1, point2, color, duration);
            }
#endif
        }

        /// <summary>
        /// Draw a debug line with arrow.
        /// </summary>
        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void DrawArrow(Vector3 from, Vector3 to, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20f)
        {
#if UNITY_EDITOR
            Debug.DrawLine(from, to, color);

            Vector3 direction = (to - from).normalized;
            Vector3 right = Quaternion.Euler(0, 0, arrowHeadAngle) * -direction;
            Vector3 left = Quaternion.Euler(0, 0, -arrowHeadAngle) * -direction;

            Debug.DrawLine(to, to + right * arrowHeadLength, color);
            Debug.DrawLine(to, to + left * arrowHeadLength, color);
#endif
        }
    }
}

