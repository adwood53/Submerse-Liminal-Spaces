using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    Animator anim;
    Book book;
    bool isOpen = true;
    
    bool sceneLoaded = false;
    GameObject loadingIcon;

    public FPSController cam;
    private InputManager inputManager;

    void Start()
    {
        loadingIcon = GameObject.Find("Brochure/LoadingBox");
        loadingIcon.SetActive(false);
        book = GetComponent<Book>();
        anim = GetComponent<Animator>();
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        inputManager = InputManager.Instance;

        cam.PrioritiseUI(true);
    }

    private void Update()
    {
        if (inputManager.PlayerInteractedThisFrame())
        {
            toggle();
        }
    }

    public void toggle()
    {
        if (isOpen && !sceneLoaded)
        {
            if (SceneManager.LoadSceneAsync(book.currentPage.ToString(), LoadSceneMode.Additive) != null)
            {
                loadingIcon.SetActive(true);
                isOpen = false;
            }
            else { Debug.LogWarning("SceneLoader: Scene is currently loaded. Unload the scene before attempting to load in another scene."); }
        }
        else if (!isOpen && sceneLoaded)
        {
            anim.SetTrigger("ToggleOpen");
        }
    }

    public void UnloadScene()
    {
        //cam.sceneLoaded = false;
        cam.PrioritiseUI(true);
        isOpen = true;
        SceneManager.UnloadSceneAsync(book.currentPage.ToString());
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) //This method runs when a scene has been loaded
    {
        cam.enabled = true;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(book.currentPage.ToString()));
        anim.SetTrigger("ToggleClose");
        sceneLoaded = true;
        //cam.sceneLoaded = true;
        cam.PrioritiseUI(false);
        loadingIcon.SetActive(false);
    }

    void OnSceneUnloaded(Scene scene) // This method runs when a scene is unloaded
    {
        cam.GetComponentInChildren<CinemachinePOVExtension>().Recenter();
        cam.enabled = false;
        sceneLoaded = false;
    }

}
