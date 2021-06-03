using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;

    private PlayerControls playerControls;

    public static InputManager Instance { 
    get
        {
            return _instance;
        }
    }

    // Use this for initialization
    void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        playerControls = new PlayerControls();
    }

    // Update is called once per frame
    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public Vector2 GetPlayerMovement()
    {
        return playerControls.Player.Movement.ReadValue<Vector2>();
    }

    public Vector2 GetMouseDelta()
    {
        return playerControls.Player.Look.ReadValue<Vector2>();
    }

    public Vector2 GetMousePosition()
    {
        return playerControls.Player.MousePosition.ReadValue<Vector2>();
    }

    public bool PlayerJumpedThisFrame()
    {
        return playerControls.Player.Jump.triggered;
    }

    public bool PlayerSelectedThisFrame()
    {
        return playerControls.Player.Select.triggered;
    }
    public bool PlayerInteractedThisFrame()
    {
        return playerControls.Player.Interact.triggered;
    }

    public bool PlayerExitedThisFrame()
    {
        return playerControls.Player.Exit.triggered;
    }
}
