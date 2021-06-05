using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class playonvid : MonoBehaviour
{

    public Animator anim;
    public VideoPlayer vp;
    public SkinnedMeshRenderer skin;

    // Update is called once per frame
    void Update()
    {
        if (vp.isPrepared)
        {
            anim.SetBool("isPrepared", true);
            StartCoroutine("appear");
        }
    }

    IEnumerator appear()
    {
        yield return new WaitForSeconds(0.2f);
        skin.enabled = true;
    }
}
