using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class playonvid : MonoBehaviour
{

    public Animator anim;
    public VideoPlayer vp;
    public SkinnedMeshRenderer skin;
    [Range(0.0f, 2.0f)]public float waitTime = 0.3f;

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
        yield return new WaitForSeconds(waitTime);
        skin.enabled = true;
        Destroy(this);
    }
}
