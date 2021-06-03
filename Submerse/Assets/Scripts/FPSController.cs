using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private InputManager inputManager;
    private Transform cameraTransform;

    [Header("Movement Settings")]
    [SerializeField] public bool canWalk = true;
    [SerializeField] private float playerSpeed = 2.0f;
    [Space]
    [SerializeField] public bool canJump = false;
    [SerializeField] [Range(0f, 50f)] private float jumpHeight = 1.0f;
    [SerializeField] [Range(-50f, 50f)] private float gravityValue = -9.81f;
    [Space]
    [SerializeField] private bool canSelect = true;
    [SerializeField] private bool infiniteRange = true;
    [SerializeField] [Range(0.01f, 100f)] private float selectRange = 10f;

    private bool uiPriority = false;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        inputManager = InputManager.Instance;
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        if (uiPriority)
        {
            playerVelocity = new Vector3(0f,0f,0f);
            transform.position = new Vector3(0f,1f,0f);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            if (canWalk)
            {
                Vector2 movement = inputManager.GetPlayerMovement();
                Vector3 move = new Vector3(movement.x, 0f, movement.y);
                move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
                move.y = 0f;
                controller.Move(move * Time.deltaTime * playerSpeed);

                //Changes the height position of the player..
                if (inputManager.PlayerJumpedThisFrame() && groundedPlayer && canJump)
                {
                    playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                }

                playerVelocity.y += gravityValue * Time.deltaTime;
                controller.Move(playerVelocity * Time.deltaTime);
            }
        }

    }
    public void PrioritiseUI(bool i)
    {
        uiPriority = i;
    }

    private void FixedUpdate()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit hit;

        // Hide and lock cursor when right mouse button pressed
        if (inputManager.PlayerSelectedThisFrame() && uiPriority == false && canSelect)
        {
            float range;
            if (infiniteRange) range = Mathf.Infinity;
            else range = selectRange;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, range, layerMask))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                Debug.Log("Did Hit");
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                Debug.Log("Did not Hit");
            }

        }
    }
}