using UnityEngine;

namespace Inkblade.Utils
{
    /// <summary>
    /// Input helper utilities for consistent input handling.
    /// </summary>
    public static class InputHelper
    {
        /// <summary>
        /// Get movement input as Vector2.
        /// </summary>
        public static Vector2 GetMovementInput()
        {
            return new Vector2(
                Input.GetAxisRaw("Horizontal"),
                Input.GetAxisRaw("Vertical")
            ).normalized;
        }

        /// <summary>
        /// Check if dash input is pressed.
        /// </summary>
        public static bool GetDashInput()
        {
            return Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump");
        }

        /// <summary>
        /// Check if shoot input is pressed.
        /// </summary>
        public static bool GetShootInput()
        {
            return Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire1");
        }

        /// <summary>
        /// Check if retrieve input is pressed.
        /// </summary>
        public static bool GetRetrieveInput()
        {
            return Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Interact");
        }

        /// <summary>
        /// Get mouse position in world space.
        /// </summary>
        public static Vector3 GetMouseWorldPosition()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            return Camera.main.ScreenToWorldPoint(mousePos);
        }

        /// <summary>
        /// Get mouse position in world space (2D).
        /// </summary>
        public static Vector2 GetMouseWorldPosition2D()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = -Camera.main.transform.position.z;
            return Camera.main.ScreenToWorldPoint(mousePos);
        }

        /// <summary>
        /// Get direction from position to mouse.
        /// </summary>
        public static Vector2 GetDirectionToMouse(Vector2 fromPosition)
        {
            Vector2 mousePos = GetMouseWorldPosition2D();
            return (mousePos - fromPosition).normalized;
        }
    }
}

