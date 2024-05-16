using System;
using UnityEngine;

namespace TheAshBot
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// will Copy a component onto another Game Object
        /// </summary>
        /// <typeparam name="T">Is the type of the component being copied</typeparam>
        /// <param name="destination">is the Game Object that the component is being copied to.</param>
        /// <param name="original">is the original component</param>
        /// <returns>the copied component</returns>
        public static T CopyComponent<T>(this GameObject destination, T original) where T : Component
        {
            // get the type of the component;
            Type componentType = original.GetType();

            // adding the component to the Game Object
            Component copy = destination.AddComponent(componentType);

            // Getting the fields from the original component
            System.Reflection.FieldInfo[] fields = componentType.GetFields();

            // cycling through all the field in the original component
            foreach (System.Reflection.FieldInfo field in fields)
            {
                // setting the new component value to equal the same as the original component's value for that field
                field.SetValue(copy, field.GetValue(original));
            }

            // returning the new component
            return copy as T;
        }

        /// <summary>
        /// will Copy a component onto another Game Object
        /// </summary>
        /// <param name="destination">is the Game Object that the component is being copied to.</param>
        /// <param name="original">is the original component</param>
        /// <returns>the copied component</returns>
        public static Component CopyComponent(this GameObject destination, Component original)
        {
            // get the type of the component;
            Type type = original.GetType();

            // adding the component to the Game Object
            Component copy = destination.AddComponent(type);

            // Getting the fields from the original component
            System.Reflection.FieldInfo[] fields = type.GetFields();

            // cycling through all the field in the original component
            foreach (System.Reflection.FieldInfo field in fields)
            {
                // setting the new component value to equal the same as the original component's value for that field
                field.SetValue(copy, field.GetValue(original));
            }

            // returning the new component
            return copy;
        }

        /// <summary>
        /// will set the global scale of a transform
        /// </summary>
        /// <param name="transform">is the transform that the global scale is being set to</param>
        /// <param name="newGlobalScale">is the global scale that the transform is being set to.</param>
        public static void SetGlobalScale(this Transform transform, Vector3 newGlobalScale)
        {
            transform.localScale = Vector3.one;
            transform.localScale = new Vector3(newGlobalScale.x / transform.lossyScale.x, newGlobalScale.y / transform.lossyScale.y, newGlobalScale.z / transform.lossyScale.z);
        }

        /// <summary>
        /// will set the global scale of a transform
        /// </summary>
        /// <param name="transform">is the transform that the global scale is being set to</param>
        /// <param name="newGlobalScale">is the global scale that the transform is being set to.</param>
        public static void SetGlobalScale(this Transform transform, Vector2 newGlobalScale)
        {
            transform.localScale = Vector3.one;
            transform.localScale = new Vector3(newGlobalScale.x / transform.lossyScale.x, newGlobalScale.y / transform.lossyScale.y, 1);
        }

        /// <summary>
        /// will Rotate the vector3
        /// </summary>
        /// <param name="originalVector">is the vector being rotated</param>
        /// <param name="rotation">is the rotation that will be applied to the vector</param>
        /// <returns>the rotated vector</returns>
        public static Vector3 Rotate(this Vector3 originalVector, Quaternion rotation)
        {
            return rotation * originalVector;
        }
        
        /// <summary>
        /// will Rotate the vector2
        /// </summary>
        /// <param name="originalVector">is the vector being rotated</param>
        /// <param name="rotation">is the rotation that will be applied to the vector</param>
        /// <returns>the rotated vector</returns>
        public static Vector2 Rotate(this Vector2 originalVector, Quaternion rotation)
        {
            return rotation * originalVector;
        }

        /// <summary>
        /// Will Convert a Render Texture to a new Texture 2D
        /// </summary>
        /// <param name="renderTexture"></param>
        /// <returns></returns>
        public static Texture2D ToTexture2D(this RenderTexture renderTexture)
        {
            // Create New Texture 2D
            Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);

            // Copy old active Render Texture, and set the active render texture to the new render texture
            RenderTexture oldRenderTexture = RenderTexture.active;
            RenderTexture.active = renderTexture;

            // Copy Pixel Values, and Applying
            texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            texture.Apply();

            // Set Active Render Texture to old Active Render texture
            RenderTexture.active = oldRenderTexture;

            return texture;
        }
    }
}
