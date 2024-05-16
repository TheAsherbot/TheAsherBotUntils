using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

namespace TheAshBot.ThreeDimensional
{
    public static class Mouse3D
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

        #region GetObjectAtMousePosition

        #region Normal

        /// <summary>
        /// This will try to get a Game Object at the mouse World Position.
        /// </summary>
        /// <param name="camera">This is the camera that it takes the mouse position from.</param>
        /// <param name="hit">This is th Game Object that the mouse is over.</param>
        /// <returns>true if the mouse is over a Game Object.</returns>
        public static bool TryGetObjectAtMousePosition(Camera camera, out GameObject hit)
        {
            Ray ray = camera.ScreenPointToRay(MousePosition);
            UnityEngine.Physics.Raycast(ray, out RaycastHit rayCastHit);

            if (rayCastHit.transform != null)
            {
                hit = rayCastHit.transform.gameObject;
                return true;
            }

            hit = null;
            return false;
        }

        /// <summary>
        /// This will try to get a Game Object at the mouse World Position. 
        /// </summary>
        /// <param name="hit">This is th Game Object that the mouse is over.</param>
        /// <returns>true if the mouse is over a Game Object.</returns>
        public static bool TryGetObjectAtMousePosition(out GameObject hit)
        {
            return TryGetObjectAtMousePosition(Camera.main, out hit);
        }

        #endregion

        #region Ingore Component

        /// <summary>
        /// This will try to get a Game Object at the mouse World Position that does not has a component.
        /// </summary>
        /// <typeparam name="T">This is the competent it ignores</typeparam>
        /// <param name="camera">This is the camera that it takes the mouse position from.</param>
        /// <param name="hit">This is th Game Object that the mouse is over.</param>
        /// <returns>true if the mouse is over a Game Object.</returns>
        public static bool TryGetObjectAtMousePositionIgnoreComponent<T>(Camera camera, out GameObject hit) where T : Component
        {
            Ray ray = camera.ScreenPointToRay(MousePosition);
            RaycastHit[] rayCastHitArray = Physics.RaycastAll(ray);

            foreach (RaycastHit rayCastHit in rayCastHitArray)
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
        /// <typeparam name="T">This is the component it ignores</typeparam>
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
        /// <returns>true if the mouse is over a Game Object.</returns>
        public static bool TryGetObjectAtMousePositionWithComponent<T>(Camera camera, out GameObject hit) where T : Component
        {
            Ray ray = camera.ScreenPointToRay(MousePosition);
            RaycastHit[] rayCastHitArray = Physics.RaycastAll(ray);

            foreach (RaycastHit rayCastHit in rayCastHitArray)
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
        /// This will try to get a Game Object at the mouse World Position that has a component.
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

        #region Mouse Position Vector3

        #region GetMousePosition
        /// <summary>
        /// This gets the mouse position in 3D using a ray cast
        /// </summary>
        /// <param name="camera">This is the camera that the ray casts shoots from</param>
        /// <param name="layerMask">This is the layers that the ray cast ignores</param>
        /// <returns>This return the the point that the ray cast hits</returns>
        public static Vector3 GetMousePosition3D(Camera camera, LayerMask layerMask)
        {
            Ray ray = camera.ScreenPointToRay(MousePosition);
            UnityEngine.Physics.Raycast(ray, out RaycastHit rayCastHit, float.MaxValue, layerMask);
            return rayCastHit.point;
        }

        /// <summary>
        /// This gets the mouse position in 3D using a ray cast
        /// </summary>
        /// <param name="camera">This is the camera that the ray casts are shoot from</param>
        /// <returns>This return the the point that the ray cast hits</returns>
        public static Vector3 GetMousePosition3D(Camera camera)
        {
            Ray ray = camera.ScreenPointToRay(MousePosition);
            UnityEngine.Physics.Raycast(ray, out RaycastHit rayCastHit, float.MaxValue);
            return rayCastHit.point;
        }

        /// <summary>
        /// This gets the mouse position in 3D using a ray cast
        /// </summary>
        /// <param name="layerMask">This is the layers that the ray cast ignores</param>
        /// <returns>This return the the point that the ray cast hits</returns>
        public static Vector3 GetMousePosition3D(LayerMask layerMask)
        {
            return GetMousePosition3D(Camera.main, layerMask);
        }

        /// <summary>
        /// This gets the mouse position in 3D using a ray cast
        /// </summary>
        /// <returns>This return the the point that the ray cast hits</returns>
        public static Vector3 GetMousePosition3D()
        {
            return GetMousePosition3D(Camera.main);
        }
        #endregion

        #region DebugLogMousePosition
        /// <summary>
        /// This logs the mouse position in 3D using a ray cast
        /// </summary>
        /// <param name="camera">This is the camera that the ray casts shoots from</param>
        /// <param name="layerMask">This is the layers that the ray cast ignores</param>
        public static void DebugLogMousePosition3D(Camera camera, LayerMask layerMask)
        {
            Debug.Log("MousePosition3D = " + GetMousePosition3D(camera, layerMask));
        }

        /// <summary>
        /// This logs the mouse position in 3D using a ray cast
        /// </summary>
        /// <param name="camera">This is the camera that the ray casts shoots from</param>
        public static void DebugLogMousePosition3D(Camera camera)
        {
            Debug.Log("MousePosition3D = " + GetMousePosition3D(camera));
        }

        /// <summary>
        /// This logs the mouse position in 3D using a ray cast
        /// </summary>
        /// <param name="layerMask">This is the layers that the ray cast ignores</param>
        public static void DebugLogMousePosition3D(LayerMask layerMask)
        {
            Debug.Log("MousePosition3D = " + GetMousePosition3D(layerMask));
        }

        /// <summary>
        /// This logs the mouse position in 3D using a ray cast
        /// </summary>
        public static void DebugLogMousePosition3D()
        {
            Debug.Log("MousePosition3D = " + GetMousePosition3D());
        }
        #endregion

        #endregion

        #region Mouse Position Vector3Int

        #region GetMousePositionInt
        /// <summary>
        /// Using a ray cast to gets this the mouse position in 3D and round it to an int
        /// </summary>
        /// <param name="camera">This is the camera that the ray casts shoots from</param>
        /// <param name="layerMask">This is the layers that the ray cast ignores</param>
        /// <returns>This return the the point that the ray cast hits Rounded to and int</returns>
        public static Vector3Int GetMousePositionInt3D(Camera camera, LayerMask layerMask)
        {
            return Vector3Int.RoundToInt(GetMousePosition3D(camera, layerMask));
        }

        /// <summary>
        /// Using a ray cast to gets this the mouse position in 3D and round it to an int
        /// </summary>
        /// <param name="camera">This is the camera that the ray casts shoots from</param>
        /// <returns>This return the the point that the recast hits Rounded to and int</returns>
        public static Vector3Int GetMousePositionInt3D(Camera camera)
        {
            return Vector3Int.RoundToInt(GetMousePosition3D(camera));
        }

        /// <summary>
        /// Using a ray cast to gets this the mouse position in 3D and round it to an int
        /// </summary>
        /// <param name="layerMask">This is the layers that the ray cast ignores</param>
        /// <returns>This return the the point that the ray cast hits Rounded to and int</returns>
        public static Vector3Int GetMousePositionInt3D(LayerMask layerMask)
        {
            return Vector3Int.RoundToInt(GetMousePosition3D(layerMask));
        }

        /// <summary>
        /// Using a ray cast to gets this the mouse position in 3D and round it to an int
        /// </summary>
        /// <returns>This return the the point that the ray cast hits Rounded to and int</returns>
        public static Vector3Int GetMousePositionInt3D()
        {
            return Vector3Int.RoundToInt(GetMousePosition3D());
        }
        #endregion

        #region DebugLogMousePositionInt
        /// <summary>
        ///  using a ray cast this logs the mouse position in 3D rounded to a vector3 int
        /// </summary>
        /// <param name="camera">This is the camera that the ray casts shoots from</param>
        /// <param name="layerMask">This is the layers that the ray cast ignores</param>
        public static void DebugLogMousePositionInt3D(Camera camera, LayerMask layerMask)
        {
            Debug.Log("MousePositionInt3D = " + GetMousePositionInt3D(camera, layerMask));
        }

        /// <summary>
        ///  using a ray cast this logs the mouse position in 3D rounded to a vector3 int
        /// </summary>
        /// <param name="camera">This is the camera that the ray casts shoots from</param>
        public static void DebugLogMousePositionInt3D(Camera camera)
        {
            Debug.Log("MousePositionInt3D = " + GetMousePositionInt3D(camera));
        }

        /// <summary>
        ///  using a ray cast this logs the mouse position in 3D rounded to a vector3 int
        /// </summary>
        /// <param name="layerMask">This is the layers that the ray cast ignores</param>
        public static void DebugLogMousePositionInt3D(LayerMask layerMask)
        {
            Debug.Log("MousePositionInt3D = " + GetMousePositionInt3D(layerMask));
        }

        /// <summary>
        ///  using a ray cast this logs the mouse position in 3D rounded to a vector3 int
        /// </summary>
        public static void DebugLogMousePositionInt3D()
        {
            Debug.Log("MousePositionInt3D = " + GetMousePositionInt3D());
        }
        #endregion

        #endregion

        #region IsMouseOverUI

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
