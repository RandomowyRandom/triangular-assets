using UnityEngine;
using UnityEngine.InputSystem;

namespace TriangularAssets
{
    public static class InputHandler
    {
        private static Camera _camera;
        
        /// <returns>
        /// returns position of mouse in world space
        /// </returns>
        public static Vector2 GetMousePositionInWorldSpace()
        {
            // check if camera is set, if not, set it
            if(_camera == null)
                _camera = Camera.main;

            // get mouse position in world space
            var position = new Vector2(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue());
            position = _camera.ScreenToWorldPoint(position);

            return position;
        }
    }
}