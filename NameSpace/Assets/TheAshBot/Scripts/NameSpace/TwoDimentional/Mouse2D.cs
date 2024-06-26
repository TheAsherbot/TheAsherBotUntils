using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

namespace TheAshBot.TwoDimensional
{
    public static class Mouse2D
    {

        private static Vector2 MousePosition
        {
            get
            {
#if ENABLE_INPUT_SYSTEM
                return UnityEngine.InputSystem.Mouse.current.position.ReadValue();
#elif ENABLE_LEGACY_INPUT_MANAGER
                return Input.mousePosition;
#else
                return Vector2.zero;
#endif
            }
        }

        #region Get Object At Mouse Position

        #region Normal

        /// <summary>
        /// This will try to get a Game Object at the mouse World Position.
        /// </summary>
        /// <param name="camera">This is the camera that it takes the mouse position from.</param>
        /// <param name="hit">This is th Game Object that the mouse is over.</param>
        /// <returns>true if the mouse is over a Game Object .</returns>
        public static bool TryGetObjectAtMousePosition(Camera camera, out GameObject hit)
        {
            Ray ray = camera.ScreenPointToRay(MousePosition);

            RaycastHit2D rayCastHit = Physics2D.Raycast(ray.origin, ray.direction);

            if (rayCastHit.transform != null)
            {
                hit = rayCastHit.transform.gameObject;
                return true;
            }
            
            hit = null;
            return false;
        }

        /// <summary>
        /// This will try to get a Game Object  at the mouse World Position. 
        /// </summary>
        /// <param name="hit">This is th Game Object  that the mouse is over.</param>
        /// <returns>true if the mouse is over a Game Object .</returns>
        public static bool TryGetObjectAtMousePosition(out GameObject hit)
        {
            return TryGetObjectAtMousePosition(Camera.main, out hit);
        }

        #endregion

        #region Ingore Component

        /// <summary>
        /// This will try to get a game object at the mouse World Position that does not has a component.
        /// </summary>
        /// <typeparam name="T">This is the competent it ignores</typeparam>
        /// <param name="camera">This is the camera that it takes the mouse position from.</param>
        /// <param name="hit">This is th Game Object that the mouse is over.</param>
        /// <returns>true if the mouse is over a Game Object .</returns>
        public static bool TryGetObjectAtMousePositionIgnoreComponent<T>(Camera camera, out GameObject hit) where T : Component
        {
            Ray ray = camera.ScreenPointToRay(MousePosition);

            RaycastHit2D[] rayCastHitArray = Physics2D.RaycastAll(ray.origin, ray.direction);

            foreach (RaycastHit2D rayCastHit in rayCastHitArray)
            {
                if (!rayCastHit.transform.TryGetComponent(out T t))
                {
                    hit = rayCastHit.transform.gameObject;
                    return true;
                }
            }
            
            hit = null;
            return false;
        }

        /// <summary>
        /// This will try to get a Game Object at the mouse World Position that does not has a component. 
        /// </summary>
        /// <typeparam name="T">This is the competent it ignores</typeparam>
        /// <param name="hit">This is th Game Object that the mouse is over.</param>
        /// <returns>true if the mouse is over a Game Object.</returns>
        public static bool TryGetObjectAtMousePositionIgnoreComponent<T>(out GameObject hit) where T : Component
        {
            return TryGetObjectAtMousePositionIgnoreComponent<T>(Camera.main, out hit);
        }

        #endregion

        #region With Component

        /// <summary>
        /// This will try to get a Game Object at the mouse World Position that has a component.
        /// </summary>
        /// <typeparam name="T">This is the component that it has to have</typeparam>
        /// <param name="camera">This is the camera that it takes the mouse position from.</param>
        /// <param name="hit">This is th Game Object that the mouse is over.</param>
        /// <returns>true if the mouse is over a Game Object .</returns>
        public static bool TryGetObjectAtMousePositionWithComponent<T>(Camera camera, out GameObject hit) where T : Component
        {
            Ray ray = camera.ScreenPointToRay(MousePosition);

            RaycastHit2D[] rayCastHitArray = Physics2D.RaycastAll(ray.origin, ray.direction);

            foreach (RaycastHit2D rayCastHit in rayCastHitArray)
            {
                if (rayCastHit.transform.TryGetComponent(out T t))
                {
                    hit = rayCastHit.transform.gameObject;
                    return true;
                }
            }
            
            hit = null;
            return false;
        }

        /// <summary>
        /// This will try to get a cat the mouse World Position that has a component.
        /// </summary>
        /// <typeparam name="T">This is the component that it has to have</typeparam>
        /// <param name="camera">This is the camera that it takes the mouse position from.</param>
        /// <param name="hit">This is th Game Object that the mouse is over.</param>
        /// <returns>true if the mouse is over a Game Object.</returns>
        public static bool TryGetObjectAtMousePositionWithComponent<T>(out GameObject hit) where T : Component
        {
            return TryGetObjectAtMousePositionIgnoreComponent<T>(Camera.main, out hit);
        }

        #endregion

        #endregion

        #region Mouse Position Vector2

        #region Get Mouse Position
        /// <summary>
        /// This gets the mouse position in 2D
        /// </summary>
        /// <param name="camera">This is the camera that the it gets the position from</param>
        /// <param name="zPosition">This is the z position</param>
        /// <returns>This return the mouse position</returns>
        public static Vector2 GetMousePosition2D(Camera camera, float zPosition)
        {
            Vector3 mouseWorldPosition = camera.ScreenToWorldPoint(MousePosition);
            mouseWorldPosition.z = zPosition;
            return mouseWorldPosition;
        }

        /// <summary>
        /// This gets the mouse position in 2D
        /// </summary>
        /// <param name="camera">This is the camera that the it gets the position from</param>
        /// <returns>This return the mouse position</returns>
        public static Vector2 GetMousePosition2D(Camera camera)
        {
            return GetMousePosition2D(camera, 0);
        }

        /// <summary>
        /// This gets the mouse position in 2D
        /// </summary>
        /// <param name="zPosition">This is the z position</param>
        /// <returns>This return the mouse position</returns>
        public static Vector2 GetMousePosition2D(float zPosition)
        {
            return GetMousePosition2D(Camera.main, zPosition);
        }

        /// <summary>
        /// This gets the mouse position in 2D
        /// </summary>
        /// <returns>This return the mouse position</returns>
        public static Vector2 GetMousePosition2D()
        {
            return GetMousePosition2D(Camera.main, 0);
        }
        #endregion

        #region Debug Log Mouse Position 2D
        /// <summary>
        /// This logs the mouse position in 2D
        /// </summary>
        /// <param name="camera">This is the camera that the it gets the position from</param>
        /// <param name="zPosition">This is the z position</param>
        public static void DebugLogMousePosition2D(Camera camera, float zPosition)
        {
            Vector3 mousePos = GetMousePosition2D(camera, zPosition);
            Debug.Log("Mouse Position Float 2D = " + mousePos);
        }

        /// <summary>
        /// This logs the mouse position in 2D
        /// </summary>
        /// <param name="camera">This is the camera that the it gets the position from</param>
        public static void DebugLogMousePosition2D(Camera camera)
        {
            DebugLogMousePosition2D(camera, 0);
        }

        /// <summary>
        /// This logs the mouse position in 2D
        /// </summary>
        /// <param name="zPosition">This is the z position</param>
        public static void DebugLogMousePosition2D(float zPosition)
        {
            DebugLogMousePosition2D(Camera.main, zPosition);
        }

        /// <summary>
        /// This logs the mouse position in 2D
        /// </summary>
        public static void DebugLogMousePosition2D()
        {
            DebugLogMousePosition2D(Camera.main, 0);
        }
        #endregion

        #endregion

        #region Mouse Position Vector2Int

        #region Get Mouse Position Int
        /// <summary>
        /// This gets the mouse position in 2D rounded to an Int
        /// </summary>
        /// <param name="camera">This is the camera that the it gets the position from</param>
        /// <param name="zPosition">This is the z position</param>
        /// <returns>This return the mouse position rounded to an Int</returns>
        public static Vector2Int GetMousePositionInt2D(Camera camera, int zPosition)
        {
            return Vector2Int.RoundToInt(GetMousePosition2D(camera, zPosition));
        }

        /// <summary>
        /// This gets the mouse position in 2D rounded to an Int
        /// </summary>
        /// <param name="camera">This is the camera that the it gets the position from</param>
        /// <returns>This return the mouse position rounded to an Int</returns>
        public static Vector2Int GetMousePositionInt2D(Camera camera)
        {
            return GetMousePositionInt2D(camera, 0);
        }

        /// <summary>
        /// This gets the mouse position in 2D rounded to an Int
        /// </summary>
        /// <param name="zPosition">This is the z position</param>
        /// <returns>This return the mouse position rounded to an Int</returns>
        public static Vector2Int GetMousePositionInt2D(int zPosition)
        {
            return GetMousePositionInt2D(Camera.main, zPosition);
        }

        /// <summary>
        /// This gets the mouse position in 2D rounded to an Int
        /// </summary>
        /// <returns>This return the mouse position rounded to an Int</returns>
        public static Vector2Int GetMousePositionInt2D()
        {
            return GetMousePositionInt2D(Camera.main, 0);
        }
        #endregion

        #region Debug Log Mouse Position Int
        /// <summary>
        /// This logs the mouse position in 2D rounded to an Int
        /// </summary>
        /// <param name="camera">This is the camera that the it gets the position from</param>
        /// <param name="zPosition">This is the z position<</param>
        public static void DebugLogMousePositionInt2D(Camera camera, int zPosition)
        {
            Vector2Int mousePos = GetMousePositionInt2D(camera, zPosition);
            Debug.Log("Mouse Position Int 2D = " + mousePos);
        }

        /// <summary>
        /// THis logs the mouse position in 2D rounded to an Int
        /// </summary>
        /// <param name="camera">This is the camera that the it gets the position from</param>
        public static void DebugLogMousePositionInt2D(Camera camera)
        {
            DebugLogMousePositionInt2D(camera, 0);
        }

        /// <summary>
        /// THis logs the mouse position in 2D rounded to an Int
        /// </summary>
        /// <param name="zPosition">This is the z position<</param>
        public static void DebugLogMousePositionInt2D(int zPosition)
        {
            DebugLogMousePositionInt2D(Camera.main, zPosition);
        }

        /// <summary>
        /// THis logs the mouse position in 2D rounded to an Int
        /// </summary>
        public static void DebugLogMousePositionInt2D()
        {
            DebugLogMousePositionInt2D(Camera.main, 0);
        }
        #endregion

        #endregion

        #region Is Mouse Over UI

        /// <summary>
        /// This checks to see if the mouse is over UI while ignoring a script.
        /// </summary>
        /// <typeparam name="T">This is the Component that the is being ignored.</typeparam>
        /// <returns>true if the mouse is over the UI without the script.</returns>
        public static bool IsMouseOverUIIgnoreComponent<T>() where T : Component
        {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = MousePosition;

            List<RaycastResult> rayCastResultList = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, rayCastResultList);
            for (int rayCastNumber = 0; rayCastNumber < rayCastResultList.Count; rayCastNumber++)
            {
                if (rayCastResultList[rayCastNumber].gameObject.GetComponent<T>() != null)
                {
                    rayCastResultList.RemoveAt(rayCastNumber);
                    rayCastNumber--;
                }
            }

            return rayCastResultList.Count > 0;
        }
        
        /// <summary>
        /// This checks to see if the mouse is over UI with a script.
        /// </summary>
        /// <typeparam name="T">This is the Component that the UI has to have.</typeparam>
        /// <returns>true if the mouse is over the UI with the script.</returns>
        public static bool IsMouseOverUIWithComponent<T>() where T : Component
        {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = MousePosition;

            List<RaycastResult> rayCastResultList = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, rayCastResultList);
            for (int rayCastNumber = 0; rayCastNumber < rayCastResultList.Count; rayCastNumber++)
            {
                if (rayCastResultList[rayCastNumber].gameObject.GetComponent<T>() != null)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// This checks to see if the mouse is over UI.
        /// </summary>
        /// <returns>true if the mouse is over the UI.</returns>
        public static bool IsMouseOverUI()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }

        #endregion

    }
}
