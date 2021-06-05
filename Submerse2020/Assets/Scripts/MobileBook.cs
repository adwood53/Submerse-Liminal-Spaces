using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileBook : MonoBehaviour
{

    public GameObject full;
    public GameObject half;
    public GameObject halfBtn;

    // Update is called once per frame
    void Update()
    {

        if (Screen.width < ((Screen.height / 100) * 80))
        {
            full.SetActive(false);
            half.SetActive(true);
            halfBtn.SetActive(true);
        }
        else
        {
            full.SetActive(true);
            half.SetActive(false);
            halfBtn.SetActive(false);
        }
    }
}
