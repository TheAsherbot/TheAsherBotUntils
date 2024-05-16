using UnityEngine;

namespace TheAshBot.ThreeDimensional
{
    public class PlayerControllers3D
    {
        [Header("Gravity & Jump")]
        private static Vector3 velocity;

        [Header("Third Person")]
        private static float turnSmoothVelocity;

        [Header("First Person Mouse")]
        private static float xRotation = 0f;

        #region First Person

        #region Charcter Controller

        #region Mouse Look
        /// <summary>
        /// This rotates the and the camera for 1st person view
        /// </summary>
        /// <param name="mouseX">This is the mouse movement on the x axis</param>
        /// <param name="mouseY">This is the mouse movement on the y axis</param>
        /// <param name="playerBody">This is the player body that is rotated on the y axis</param>
        /// <param name="mouseSensitivity">This is how fast the player rotates</param>
        /// <param name="camera">This is the camera that is moved on the x axis</param>
        public static void FirstPersonCharterControllerMouseLook(float mouseX, float mouseY, GameObject playerBody, float mouseSensitivity, Transform camera)
        {
            mouseX *= mouseSensitivity * Time.deltaTime;
            mouseY *= mouseSensitivity * Time.deltaTime;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90, 90);

            camera.localRotation = Quaternion.Euler(PlayerControllers3D.xRotation, 0, 0);
            playerBody.transform.Rotate(Vector3.up * mouseX);
        }

        /// <summary>
        /// This rotates the and the camera for 1st person view
        /// </summary>
        /// <param name="mouseX">This is the mouse movement on the x axis</param>
        /// <param name="mouseY">This is the mouse movement on the y axis</param>
        /// <param name="playerBody">This is the player body that is rotated on the y axis</param>
        /// <param name="mouseSensitivity">This is how fast the player rotates</param>
        public static void FirstPersonCharterControllerMouseLook(float mouseX, float mouseY, GameObject playerBody, float mouseSensitivity)
        {
            FirstPersonCharterControllerMouseLook(mouseX, mouseY, playerBody, mouseSensitivity, Camera.main.transform);
        }

        /// <summary>
        /// This rotates the and the camera for 1st person view
        /// </summary>
        /// <param name="mouseX">This is the mouse movement on the x axis</param>
        /// <param name="mouseY">This is the mouse movement on the y axis</param>
        /// <param name="playerBody">This is the player body that is rotated on the y axis</param>
        /// <param name="camera">This is the camera that is moved on the x axis</param>
        public static void FirstPersonCharterControllerMouseLook(float mouseX, float mouseY, GameObject playerBody, Transform camera)
        {
            float mouseSensitivity = 100f;
            FirstPersonCharterControllerMouseLook(mouseX, mouseY, playerBody, mouseSensitivity, camera);
        }
        
        /// <summary>
        /// This rotates the and the camera for 1st person view
        /// </summary>
        /// <param name="mouseX">This is the mouse movement on the x axis</param>
        /// <param name="mouseY">This is the mouse movement on the y axis</param>
        /// <param name="playerBody">This is the player body that is rotated on the y axis</param>
        public static void FirstPersonCharterControllerMouseLook(float mouseX, float mouseY, GameObject playerBody)
        {
            float mouseSensitivity = 100f;
            FirstPersonCharterControllerMouseLook(mouseX, mouseY, playerBody, mouseSensitivity, Camera.main.transform);
        }
        #endregion

        #region Movement
        /// <summary>
        /// This moves the player in all directions at the same speed
        /// </summary>
        /// <param name="x">This is the movement on the x axis</param>
        /// <param name="z">This is the movement on the z axis</param>
        /// <param name="thisObject">This is the object that is being moved</param>
        /// <param name="controller">This is the Character Controller</param>
        /// <param name="speed">This is the speed that the player moves forward</param>
        public static void FirstPersonCharterControllerMovement(float x, float z, GameObject thisObject, CharacterController controller, float speed)
        {
            Vector3 moveX = Vector3.right * x;
            Vector3 moveZ = thisObject.transform.forward * z;
            Vector3 move = new Vector3(x, 0, z);

            controller.Move(speed * Time.deltaTime * move.normalized);
        }

        /// <summary>
        /// This move the player at a different speed when going forward than going any other direction
        /// </summary>
        /// <param name="x">This is the movement on the x axis<</param>
        /// <param name="z">This is the movement on the z axis</param>
        /// <param name="thisObject">This is the object that is being moved</param>
        /// <param name="controller">This is the Character Controller</param>
        /// <param name="speed">This is the speed that the player moves forward</param>
        /// <param name="strafeSpeed">This the speed that the player strafes at</param>
        public static void FirstPersonCharterControllerStrafeMovement(float x, float z, GameObject thisObject, CharacterController controller, float speed, float strafeSpeed)
        {
            Vector3 moveX = strafeSpeed * x * thisObject.transform.right;
            Vector3 moveZ = Vector3.zero;
            if (z >= 0.1)
            {
                moveZ = speed * z * thisObject.transform.forward;
            }
            else if (z <= -0.1)
            {
                moveZ = strafeSpeed * z * thisObject.transform.forward;
            }
            Vector3 move = moveX + moveZ;

            controller.Move(move * Time.deltaTime);
        }

        /// <summary>
        /// This move the player at a different speed when going forward than going any other direction
        /// </summary>
        /// <param name="x">This is the movement on the x axis<</param>
        /// <param name="z">This is the movement on the z axis</param>
        /// <param name="thisObject">This is the object that is being moved</param>
        /// <param name="controller">This is the Character Controller</param>
        /// <param name="speed">This is the speed that the player moves forward</param>
        public static void FirstPersonCharterControllerStrafeMovement(float x, float z, GameObject thisObject, CharacterController controller, float speed)
        {
            float strafeSpeed = speed / 2;
            FirstPersonCharterControllerStrafeMovement(x, z, thisObject, controller, speed, strafeSpeed);
        }
        #endregion

        /// <summary>
        /// This has every thing for 1st person character controller movement
        /// </summary>
        /// <param name="x">This is the movement on the x axis</param>
        /// <param name="z">This is the movement on the z axis</param>
        /// <param name="thisObject">This is the object that is being moved</param>
        /// <param name="controller">This is the Character Controller</param>
        /// <param name="speed">This is the speed that the player moves forward</param>
        /// <param name="strafeSpeed">This the speed that the player strafes at</param>
        /// <param name="sprintSpeed">This is that speed that the player sprints at</param>
        /// <param name="mouseX">This is the mouse movement on the x axis</param>
        /// <param name="mouseY">This is the mouse movement on the y axis</param>
        /// <param name="playerBody">This is the player body that is rotated on the y axis</param>
        /// <param name="mouseSensitivity">This is how fast the player rotates</param>
        /// <param name="camera">This is the camera that is moved on the x axis</param>
        /// <param name="gravity">This is the force gravity that the player is being pulled at</param>
        /// <param name="jumpHeight">This is the height that the player jumps</param>
        /// <param name="isJumping">This is the trigger that triggers the jump</param>
        /// <param name="isSprinting">This is the trigger that make the player sprint</param>
        public static void FirstPersonCharterControllerEverything(float x, float z, GameObject thisObject, CharacterController controller, float speed, float strafeSpeed, float sprintSpeed,
            float mouseX, float mouseY, GameObject playerBody, int mouseSensitivity, Transform camera, float gravity, float jumpHeight, bool isJumping, bool isSprinting)
        {
            if (isJumping)
            {
                CharterControllerJump(controller, jumpHeight, gravity);
            }
            if (isSprinting)
            {
                CharterControllerSprint(controller, thisObject.transform, sprintSpeed);
            }
            else if (!isSprinting)
            {
                FirstPersonCharterControllerStrafeMovement(x, z, thisObject, controller, speed, strafeSpeed);
            }
            FirstPersonCharterControllerMouseLook(mouseX, mouseY, playerBody, mouseSensitivity, camera);
            CharterControllerGravity(controller, gravity);
        }
        #endregion

        #endregion

        #region Third Person

        #region Character Controller

        #region Strafe Movement
        /// <summary>
        /// This move the player at a different speed when going forward than going any other direction
        /// </summary>
        /// <param name="x">This is the movement on the x axis</param>
        /// <param name="z">This is the movement on the z axis</param>
        /// <param name="thisObject">This is the object that is being moved</param>
        /// <param name="controller">This is the Character Controller</param>
        /// <param name="speed">This is the speed that the player moves forward</param>
        /// <param name="camera">The camera that determines the forward direction</param>
        /// <param name="turnSmoothTime">This is the time it takes to move 360 Degrees</param>
        /// <param name="strafeSpeed">This is that speed that the player sprints at</param>
        public static void ThirdPersonCharterControllerStrafeMovement(float x, float z, GameObject thisObject, CharacterController controller, float speed, Transform camera, float turnSmoothTime, float strafeSpeed)
        {
            Vector3 moveX = strafeSpeed * x * thisObject.transform.right;
            Vector3 moveZ = Vector3.zero;
            if (z >= 0.1)
            {
                moveZ = speed * z * thisObject.transform.forward;
            }
            else if (z <= -0.1)
            {
                moveZ = strafeSpeed * z * thisObject.transform.forward;
            }
            Vector3 direction = moveX + moveZ;

            if (direction.magnitude >= 0.1)
            {
                float angle = Mathf.SmoothDampAngle(thisObject.transform.eulerAngles.y, camera.eulerAngles.y, ref turnSmoothVelocity, turnSmoothTime);
                thisObject.transform.rotation = Quaternion.Euler(0, angle, 0);

                controller.Move(direction * Time.deltaTime);
            }
        }

        /// <summary>
        /// This move the player at a different speed when going forward than going any other direction
        /// </summary>
        /// <param name="x">This is the movement on the x axis</param>
        /// <param name="z">This is the movement on the z axis</param>
        /// <param name="thisObject">This is the object that is being moved</param>
        /// <param name="controller">This is the Character Controller</param>
        /// <param name="speed">This is the speed that the player moves forward</param>
        /// <param name="camera">The camera that determines the forward direction</param>
        /// <param name="turnSmoothTime">This is the time it takes to move 360 Degrees</param>
        public static void ThirdPersonCharterControllerStrafeMovement(float x, float z, GameObject thisObject, CharacterController controller, float speed, Transform camera, float turnSmoothTime)
        {
            float strafeSpeed = speed / 2;
            ThirdPersonCharterControllerStrafeMovement(x, z, thisObject, controller, speed,camera, turnSmoothTime, strafeSpeed);
        }

        /// <summary>
        /// This move the player at a different speed when going forward than going any other direction
        /// </summary>
        /// <param name="x">This is the movement on the x axis</param>
        /// <param name="z">This is the movement on the z axis</param>
        /// <param name="thisObject">This is the object that is being moved</param>
        /// <param name="controller">This is the Character Controller</param>
        /// <param name="speed">This is the speed that the player moves forward</param>
        /// <param name="camera">The camera that determines the forward direction</param>
        /// <param name="strafeSpeed">This is that speed that the player sprints at</param>
        public static void ThirdPersonCharterControllerStrafeMovement(float x, float z, GameObject thisObject, CharacterController controller, float speed, float strafeSpeed, Transform camera)
        {
            float turnSmoothTime = 0.05f;
            ThirdPersonCharterControllerStrafeMovement(x, z, thisObject, controller, speed, camera, turnSmoothTime, strafeSpeed);
        }

        /// <summary>
        /// This move the player at a different speed when going forward than going any other direction
        /// </summary>
        /// <param name="x">This is the movement on the x axis</param>
        /// <param name="z">This is the movement on the z axis</param>
        /// <param name="thisObject">This is the object that is being moved</param>
        /// <param name="controller">This is the Character Controller</param>
        /// <param name="speed">This is the speed that the player moves forward</param>
        /// <param name="camera">The camera that determines the forward direction</param>
        public static void ThirdPersonCharterControllerStrafeMovement(float x, float z, GameObject thisObject, CharacterController controller, float speed, Transform camera)
        {
            float turnSmoothTime = 0.05f;
            float strafeSpeed = speed / 2;
            ThirdPersonCharterControllerStrafeMovement(x, z, thisObject, controller, speed, camera, turnSmoothTime, strafeSpeed);
        }

        /// <summary>
        /// This move the player at a different speed when going forward than going any other direction
        /// </summary>
        /// <param name="x">This is the movement on the x axis</param>
        /// <param name="z">This is the movement on the z axis</param>
        /// <param name="thisObject">This is the object that is being moved</param>
        /// <param name="controller">This is the Character Controller</param>
        /// <param name="speed">This is the speed that the player moves forward</param>
        /// <param name="strafeSpeed">This is that speed that the player sprints at</param>
        public static void ThirdPersonCharterControllerStrafeMovement(float x, float z, GameObject thisObject, CharacterController controller, float speed, float strafeSpeed)
        {
            float turnSmoothTime = 0.05f;
            ThirdPersonCharterControllerStrafeMovement(x, z, thisObject, controller, speed, Camera.main.transform, turnSmoothTime, strafeSpeed);
        }

        /// <summary>
        /// This move the player at a different speed when going forward than going any other direction
        /// </summary>
        /// <param name="x">This is the movement on the x axis</param>
        /// <param name="z">This is the movement on the z axis</param>
        /// <param name="thisObject">This is the object that is being moved</param>
        /// <param name="controller">This is the Character Controller</param>
        /// <param name="speed">This is the speed that the player moves forward</param>
        public static void ThirdPersonCharterControllerStrafeMovement(float x, float z, GameObject thisObject, CharacterController controller, float speed)
        {
            float turnSmoothTime = 0.05f;
            float strafeSpeed = speed / 2;
            ThirdPersonCharterControllerStrafeMovement(x, z, thisObject, controller, speed, Camera.main.transform, turnSmoothTime, strafeSpeed);
        }
        #endregion

        #region Movement
        /// <summary>
        /// This moves the player in all directions at the same speed
        /// </summary>
        /// <param name="x">This is the movement on the x axis</param>
        /// <param name="z">This is the movement on the z axis</param>
        /// <param name="thisObject">This is the object that is being moved</param>
        /// <param name="controller">This is the Character Controller</param>
        /// <param name="speed">This is the speed that the player moves forward</param>
        /// <param name="camera">The camera that determines the forward direction</param>
        /// <param name="turnSmoothTime">This is the time it takes to move 360 Degrees</param>
        public static void ThirdPersonCharterControllerMovement(float x, float z, GameObject thisObject, CharacterController controller, float speed, Transform camera, float turnSmoothTime)
        {
            Vector3 moveDirection = new Vector3(x, 0, z);

            if (moveDirection.magnitude >= 0.1)
            {
                float angle = Mathf.SmoothDampAngle(thisObject.transform.eulerAngles.y, camera.eulerAngles.y, ref turnSmoothVelocity, turnSmoothTime);
                thisObject.transform.rotation = Quaternion.Euler(0, angle, 0);

                controller.Move(speed * Time.deltaTime * moveDirection.normalized);
            }
        }

        /// <summary>
        /// This moves the player in all directions at the same speed
        /// </summary>
        /// <param name="x">This is the movement on the x axis</param>
        /// <param name="z">This is the movement on the z axis</param>
        /// <param name="thisObject">This is the object that is being moved</param>
        /// <param name="controller">This is the Character Controller</param>
        /// <param name="speed">This is the speed that the player moves forward</param>
        /// <param name="camera">The camera that determines the forward direction</param>
        public static void ThirdPersonCharterControllerMovement(float x, float z, GameObject thisObject, CharacterController controller, float speed, Transform camera)
        {
            float turnSmoothTime = 0.05f;
            ThirdPersonCharterControllerMovement(x, z, thisObject, controller, speed, camera, turnSmoothTime);
        }
        #endregion

        /// <summary>
        /// This has every thing for 3rd person character controller movement
        /// </summary>
        /// <param name="x">This is the movement on the x axis</param>
        /// <param name="z">This is the movement on the z axis</param>
        /// <param name="thisObject">This is the object that is being moved</param>
        /// <param name="controller">This is the Character Controller</param>
        /// <param name="speed">This is the speed that the player moves forward</param>
        /// <param name="strafeSpeed">This the speed that the player strafes at</param>
        /// <param name="sprintSpeed">This is that speed that the player sprints at</param>
        /// <param name="camera">The camera that determents the forward direction</param>
        /// <param name="turnSmoothTime">This is the time it takes to move 360 Degrees</param>
        /// <param name="jumpHeight">This is the height that the player jumps</param>
        /// <param name="gravity">This is the force gravity that the player is being pulled at</param>
        /// <param name="isJumping">This is the trigger that triggers the jump</param>
        /// <param name="isSprinting">This is the trigger that make the player sprint</param>
        public static void ThirdPersonCharterControllerEverything(float x, float z, GameObject thisObject, CharacterController controller, float speed, float sprintSpeed,
            float strafeSpeed, Transform camera, float turnSmoothTime, float jumpHeight, float gravity, bool isJumping, bool isSprinting)
        {
            if (isJumping)
            {
                CharterControllerJump(controller, jumpHeight, gravity); 
            }
            if (isSprinting == true)
            {
                CharterControllerSprint(controller, thisObject.transform, sprintSpeed);
            }
            else if (isSprinting == false)
            {
                ThirdPersonCharterControllerStrafeMovement(x, z, thisObject, controller, speed, camera, turnSmoothTime, strafeSpeed);
            }
            CharterControllerGravity(controller, gravity);
        }
        #endregion

        #endregion

        #region Any Person

        #region Character Controller

        #region Jump
        /// <summary>
        /// This make the player jump
        /// </summary>
        /// <param name="controller">this is the Character Controller that is used to check if the player is grounded</param>
        /// <param name="jumpHeight">This is the jump height</param>
        /// <param name="gravity">This is the gravity scale</param>
        public static void CharterControllerJump(CharacterController controller, float jumpHeight, float gravity)
        {
            if (controller.isGrounded == true)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
                controller.Move(velocity * Time.deltaTime);
            }
        }

        /// <summary>
        /// This make the player jump
        /// </summary>
        /// <param name="controller">this is the Character Controller that is used to check if the player is grounded</param>
        /// <param name="jumpHeight">This is the jump height</param
        public static void CharterControllerJump(CharacterController controller, float jumpHeight)
        {
            float gravity = -19.62f;
            CharterControllerJump(controller, jumpHeight, gravity);
        }
        #endregion

        #region Gravity
        /// <summary>
        /// This applies gravity to the player
        /// </summary>
        /// <param name="controller">this is the Character Controller that is used to check if the player is grounded</param>
        /// <param name="gravity">This is the gravity scale</param>
        public static void CharterControllerGravity(CharacterController controller, float gravity)
        {
            if (controller.isGrounded == false)
            {
                velocity.y += gravity * Time.deltaTime;
                controller.Move(velocity * Time.deltaTime);
            }
            else if (controller.isGrounded == true)
            {
                velocity.y = -10;
                controller.Move(velocity * Time.deltaTime);
            }
        }

        /// <summary>
        /// This applies gravity to the player
        /// </summary>
        /// <param name="controller">this is the Character Controller that is used to check if the player is grounded</param>
        public static void CharterControllerGravity(CharacterController controller)
        {
            float gravity = -19.82f;
            CharterControllerGravity(controller, gravity);
        }
        #endregion

        #region Push Rigidbody
        /// <summary>
        /// This pushes a object that has a Rigged Body
        /// Note: To use this you have tou use "private void OnControllerColliderHit(ControllerColliderHit hit) { }"
        /// </summary>
        /// <param name="thisObject">This is the player</param>
        /// <param name="hit">This is the object that is being hit</param>
        /// <param name="forceMagnitude">This is the force that is being applied</param>
        public static void CharterControllerPushRigidbody(GameObject thisObject, ControllerColliderHit hit, float forceMagnitude)
        {
            Rigidbody rigidbody = hit.collider.attachedRigidbody;

            if (rigidbody != null)
            {
                Vector3 forceDirection = hit.gameObject.transform.position - thisObject.transform.position;
                forceDirection.Normalize();

                rigidbody.AddForceAtPosition(forceDirection * forceMagnitude, thisObject.transform.position, ForceMode.Impulse);
            }
        }

        /// <summary>
        /// This pushes a object that has a Rigged Body
        /// Note: To use this you have tou use "private void OnControllerColliderHit(ControllerColliderHit hit) { }"
        /// </summary>
        /// <param name="thisObject">This is the player</param>
        /// <param name="hit">This is the object that is being hit</param>
        public static void CharterControllerPushRigidbody(GameObject thisObject, ControllerColliderHit hit)
        {
            float forceMagnitude = 1;
            CharterControllerPushRigidbody(thisObject, hit, forceMagnitude);
        }
        #endregion

        #region Sprint/Crouch
        /// <summary>
        /// This makes the player move forward faster than normal
        /// </summary>
        /// <param name="controller">This is the character controller used to move the player</param>
        /// <param name="forward">This is the forward direction</param>
        /// <param name="sprintSpeed">this is the speed that the player is sprinting at</param>
        public static void CharterControllerSprint(CharacterController controller, Transform forward, float sprintSpeed)
        {
            Vector3 move;
            move = forward.forward;
            controller.Move(sprintSpeed * Time.deltaTime * move);
        }
        #endregion

        #endregion

        #endregion
    }
}
