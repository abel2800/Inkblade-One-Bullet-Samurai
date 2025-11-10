using UnityEngine;

namespace Inkblade.Utils
{
    /// <summary>
    /// Extension methods for common Unity operations.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Clamps a Vector2 to a maximum magnitude.
        /// </summary>
        public static Vector2 ClampMagnitude(this Vector2 vector, float maxMagnitude)
        {
            if (vector.magnitude > maxMagnitude)
            {
                return vector.normalized * maxMagnitude;
            }
            return vector;
        }

        /// <summary>
        /// Returns a random point inside a circle.
        /// </summary>
        public static Vector2 RandomInsideCircle(float radius)
        {
            return Random.insideUnitCircle * radius;
        }

        /// <summary>
        /// Checks if a GameObject has a component of type T.
        /// </summary>
        public static bool HasComponent<T>(this GameObject obj) where T : Component
        {
            return obj.GetComponent<T>() != null;
        }

        /// <summary>
        /// Gets or adds a component of type T.
        /// </summary>
        public static T GetOrAddComponent<T>(this GameObject obj) where T : Component
        {
            T component = obj.GetComponent<T>();
            if (component == null)
            {
                component = obj.AddComponent<T>();
            }
            return component;
        }

        /// <summary>
        /// Smoothly moves towards a target position.
        /// </summary>
        public static Vector2 SmoothDamp(this Vector2 current, Vector2 target, ref Vector2 velocity, float smoothTime)
        {
            return Vector2.SmoothDamp(current, target, ref velocity, smoothTime);
        }

        /// <summary>
        /// Converts a Vector3 to Vector2 (ignoring Z).
        /// </summary>
        public static Vector2 ToVector2(this Vector3 vector3)
        {
            return new Vector2(vector3.x, vector3.y);
        }

        /// <summary>
        /// Converts a Vector2 to Vector3 (Z = 0).
        /// </summary>
        public static Vector3 ToVector3(this Vector2 vector2)
        {
            return new Vector3(vector2.x, vector2.y, 0f);
        }

        /// <summary>
        /// Checks if a layer is in a LayerMask.
        /// </summary>
        public static bool ContainsLayer(this LayerMask layerMask, int layer)
        {
            return (layerMask.value & (1 << layer)) != 0;
        }

        /// <summary>
        /// Destroys all children of a transform.
        /// </summary>
        public static void DestroyChildren(this Transform parent)
        {
            for (int i = parent.childCount - 1; i >= 0; i--)
            {
                Object.Destroy(parent.GetChild(i).gameObject);
            }
        }
    }
}

