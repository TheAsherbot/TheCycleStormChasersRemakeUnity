using System;

using UnityEngine;

namespace Player.Controls
{
    public class PlayerMovement : MonoBehaviour
    {

        public struct PlayerInput
        {
            public Vector2 movement;
            public float yRotation;
            public bool isSprinting;
            public bool isCrouching;
            public bool isJumping;
        }


        [SerializeField] private float jumpHeight;
        private bool isJumping;

        
        [SerializeField] private float rotationSpeed;


        [SerializeField] private float movementSpeed;
        [SerializeField] private float sprintMovementSpeed;
        private float currentMovementSpeed;




        private PlayerInput playerInput;
        private CharacterController characterController;




        private void Start()
        {
            playerInput = new PlayerInput();
            characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            GetPlayerInput();

            // Jump();
            Rotate();

            CalculateCurrentMovementSpeed();
            Movement();
        }


        private void GetPlayerInput()
        {
            playerInput.movement = Vector2.zero;
            playerInput.isSprinting = false;
            playerInput.isCrouching = false;
            playerInput.isJumping = false;

            // Movement
            if (Input.GetKey(KeyCode.W))
            {
                playerInput.movement = new Vector2(playerInput.movement.x, 1);
            }
            if (Input.GetKey(KeyCode.A))
            {
                playerInput.movement = new Vector2(-1, playerInput.movement.y);
            }
            if (Input.GetKey(KeyCode.S))
            {
                playerInput.movement = new Vector2(playerInput.movement.x, -1);
            }
            if (Input.GetKey(KeyCode.D))
            {
                playerInput.movement = new Vector2(1, playerInput.movement.y);
            }

            // Rotation
            playerInput.yRotation = Input.GetAxis("Mouse X");

            // Jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerInput.isJumping = true;
            }

            // Sprint
            if (Input.GetKey(KeyCode.LeftShift))
            {
                playerInput.isSprinting = true;
            }

            // Crouch
            if (Input.GetKey(KeyCode.LeftControl))
            {
                playerInput.isCrouching = true;
            }
        }

        // IN DEVELOPMENTY
        private void Jump()
        {
            if (playerInput.isJumping && characterController.isGrounded)
            {
                isJumping = true;
            }

            if (isJumping)
            {
                characterController.Move(jumpHeight * Vector3.up);
            }
        }

        private void Rotate()
        {
            transform.Rotate(playerInput.yRotation * rotationSpeed * Time.deltaTime * Vector3.up);
        }


        private void CalculateCurrentMovementSpeed()
        {
            if (playerInput.isSprinting)
            {
                currentMovementSpeed = sprintMovementSpeed;
            }
            else
            {
                currentMovementSpeed = movementSpeed;
            }
        }

        private void Movement()
        {
            Vector3 movement = (transform.right * playerInput.movement.x) + (transform.forward * playerInput.movement.y);
            characterController.Move(currentMovementSpeed * Time.deltaTime * movement);
        }


    }
}
