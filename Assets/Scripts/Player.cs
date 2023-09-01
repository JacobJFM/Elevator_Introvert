using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerInput playerInput;
    private CharacterController characterController;

    [Header("Movement Settings")]
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 5.0f;

    private Vector2 moveInput;
    private Vector2 rotationInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Get input from the new input system
        moveInput = playerInput.actions["Walk"].ReadValue<Vector2>();
        rotationInput = playerInput.actions["Rotate"].ReadValue<Vector2>();

        // Calculate movement direction based on input
        Vector3 moveDirection = transform.forward * moveInput.y + transform.right * moveInput.x;
        moveDirection.Normalize();

        // Move the player
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

        // calculate rotation angles
        float rotationX = -rotationInput.y * rotationSpeed;
        float rotationY = rotationInput.x * rotationSpeed;

        // rotate player
        transform.Rotate(rotationX, rotationY, 0);
    }
}
