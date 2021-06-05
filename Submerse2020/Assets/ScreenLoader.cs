using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ScreenLoader : MonoBehaviour
{
    public VideoPlayer vp;

    // Start is called before the first frame update
    void Start()
    {
        vp.clip = Resources.Load<VideoClip>("Azure Playkit Clip") as VideoClip;
        vp.Play();
    }
}
