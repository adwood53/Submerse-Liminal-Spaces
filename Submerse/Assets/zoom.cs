using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoom : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera cam;
    InputManager im;
    [Range(1.0f, 0.0f)] public float smoothing;
    public float multiplier = 1;
    public int zoomLimit = 20;
    float scroll;
    float zoomVal;
    float startFOV;
    // Start is called before the first frame update
    void Start()
    {
        im = InputManager.Instance;
        startFOV = cam.m_Lens.FieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        scroll = im.getScroll() / 120;
        
        zoomVal += (scroll * multiplier);
        
        if (zoomVal <= 0) zoomVal = 0;
        if (zoomVal > zoomLimit) zoomVal = zoomLimit;

        cam.m_Lens.FieldOfView = Mathf.Lerp(cam.m_Lens.FieldOfView, startFOV + (-zoomVal), smoothing);

    }
}
