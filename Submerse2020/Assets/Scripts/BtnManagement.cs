using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnManagement : MonoBehaviour
{
    private SceneLoader sceneloader;

    // Start is called before the first frame update
    void Start()
    {
        sceneloader = GameObject.Find("Book").GetComponent<SceneLoader>();
    }

    public void closeBook()
    {
        sceneloader.toggle();
    }
}
