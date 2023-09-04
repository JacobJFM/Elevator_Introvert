using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerInput playerInput;
    private CharacterController characterController;

    [Header("Movement Settings")]
    public float moveSpeed = 5.0f;
    // public float rotationSpeed = 2.0f;

    private Vector2 moveInput;
    // private Vector2 rotationInput;

    
    // [Header("Rotation Limits")]
    // public float maxLookUpAngle = 80.0f; // Maximum angle to look up
    // public float maxLookDownAngle = 80.0f; // Maximum angle to look down

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Get input from the new input system
        moveInput = playerInput.actions["Walk"].ReadValue<Vector2>();
        // rotationInput = playerInput.actions["Rotate"].ReadValue<Vector2>();

        // Calculate movement direction based on input
        Vector3 moveDirection = transform.forward * moveInput.y + transform.right * moveInput.x;
        moveDirection.Normalize();
        // lock y positon
        moveDirection.y = 0;

        // Move the player
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

        // // calculate rotation angles
        // float rotationX = Mathf.Clamp(-rotationInput.y, maxLookUpAngle, maxLookDownAngle);
        // float rotationY = rotationInput.x;
        // Vector3 rotationVector = new Vector3(rotationX, rotationY, 0);
        // rotationVector *= rotationSpeed * Time.deltaTime;

        // // rotate player
        // transform.localEulerAngles += rotationVector;
    }
}
