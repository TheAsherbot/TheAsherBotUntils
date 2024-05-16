#if UNITY_EDITOR
using UnityEditor;

using UnityEngine;

namespace TheAshBot.TwoDimensional.TopDownCharacterMovement.Editors
{
    [CustomEditor(typeof(TopDownCharacterMovement2D))]
    public class TopDownCharacterMovement2DEditor : Editor
    {


        TopDownCharacterMovement2D topDownCharacterMovement;



        private void OnEnable()
        {
            topDownCharacterMovement = (TopDownCharacterMovement2D)target;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            if (!topDownCharacterMovement.TryGetComponent(out IMoveVelocity2D moveVelocity))
            {
                if (GUILayout.Button("Add Movement Velocity"))
                {
                    topDownCharacterMovement.AddMovementVelocity();
                }
            }
            if (!topDownCharacterMovement.TryGetComponent(out IPlayerMovementInput2D playerMovementInput))
            {
                if (GUILayout.Button("Add Movement Input"))
                {
                    topDownCharacterMovement.AddMovementInput();
                }
            }
            if (!topDownCharacterMovement.TryGetComponent(out IMovePosition2D movePosition))
            {
                if (GUILayout.Button("Add Movement Position"))
                {
                    topDownCharacterMovement.AddMovementPosition();
                }
            }

            serializedObject.ApplyModifiedProperties();
        }


    }
}
#endif