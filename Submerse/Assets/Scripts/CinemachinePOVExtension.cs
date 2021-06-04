using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachinePOVExtension : CinemachineExtension
{

    [SerializeField] private Vector2 clampInDegrees = new Vector2(360, 80);
    [SerializeField] [Range(0f, 2f)] private float horizontalSpeed = 1f;
    [SerializeField] [Range(0f, 2f)] private float verticleSpeed = 10f;
    [SerializeField] private Vector2 smoothing = new Vector2(3, 3);

    Vector2 _mouseAbsolute;
    Vector2 _smoothMouse;

    private InputManager inputManager;
    private Vector3 startingRotation;
    private Vector3 recenterRotation;

    protected override void Awake()
    {
        inputManager = InputManager.Instance;
        base.Awake();
        recenterRotation = new Vector3(0,0,0);
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam.Follow)
        {
            if(stage == CinemachineCore.Stage.Aim)
            {
                if (startingRotation == null) startingRotation = transform.localRotation.eulerAngles;
                Vector2 deltaInput = inputManager.GetMouseDelta();

                // Scale input against the sensitivity setting and multiply that against the smoothing value.
                deltaInput = Vector2.Scale(deltaInput, new Vector2(horizontalSpeed * smoothing.x, verticleSpeed * smoothing.y));

                // Interpolate mouse movement over time to apply smoothing delta.
                _smoothMouse.x = Mathf.Lerp(_smoothMouse.x, deltaInput.x, 1f / smoothing.x);
                _smoothMouse.y = Mathf.Lerp(_smoothMouse.y, deltaInput.y, 1f / smoothing.y);

                // Find the absolute mouse movement value from point zero.
                _mouseAbsolute += _smoothMouse;

                startingRotation.x += _smoothMouse.x * verticleSpeed * Time.deltaTime;
                startingRotation.y += _smoothMouse.y * horizontalSpeed *Time.deltaTime;
                startingRotation.y = Mathf.Clamp(startingRotation.y, -clampInDegrees.y, clampInDegrees.y);
                state.RawOrientation = Quaternion.Euler(-startingRotation.y, startingRotation.x, 0f);
            }
        }
    }

    public void Recenter()
    {
        startingRotation = recenterRotation;
    }
}

