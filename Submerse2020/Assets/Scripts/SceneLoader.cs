using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    
    private AudioSource ambientAudio; 
    bool isAmbientPlaying = true;
    [Range(0.01f,10.0f)] public float audioVolumeSpeed = 3.0f;
    [Range(0.0f,1.0f)] public float maxVolume = 1.0f;

    public Button enterBTN;

    void Start()
    {
        loadingIcon = GameObject.Find("Brochure/LoadingBox");
        loadingIcon.SetActive(false);
        book = GetComponent<Book>();
        anim = GetComponent<Animator>();
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        inputManager = InputManager.Instance;

        ambientAudio = Camera.main.GetComponent<AudioSource>();

        cam.PrioritiseUI(true);
        cam.gameObject.SetActive(false);
        enterBTN.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (inputManager.PlayerInteractedThisFrame())
        {
            toggle();
        }

        if(isAmbientPlaying)
        {
            if (ambientAudio.volume >= maxVolume) ambientAudio.volume = maxVolume;
            else ambientAudio.volume = Mathf.Lerp(ambientAudio.volume, maxVolume, audioVolumeSpeed / 100);
        }
        else
        {
            if (ambientAudio.volume <= 0.001f) ambientAudio.volume = 0.001f;
            else ambientAudio.volume = Mathf.Lerp(ambientAudio.volume, 0.0f, audioVolumeSpeed / 100);
        }

        if (book.currentPage >= 2 && book.currentPage <= 8)
        {
            book.interactable = true;
            enterBTN.gameObject.SetActive(true);
        }
        else if (book.currentPage < 2 || book.currentPage > 8) enterBTN.gameObject.SetActive(false);
    }

    public void toggle()
    {
        if (isOpen && !sceneLoaded)
        {
            if (SceneManager.LoadSceneAsync(book.currentPage.ToString(), LoadSceneMode.Additive) != null)
            {
                cam.gameObject.transform.position = new Vector3(0, -0.0938f, 0);
                loadingIcon.SetActive(true);
                isOpen = false;
                if (book.currentPage == 8) isAmbientPlaying = false;
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
        cam.PrioritiseUI(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        isOpen = true;
        SceneManager.UnloadSceneAsync(book.currentPage.ToString());
        if (book.currentPage == 8) isAmbientPlaying = true;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) //This method runs when a scene has been loaded
    {
        cam.GetComponentInChildren<CinemachinePOVExtension>().Recenter();
        cam.gameObject.SetActive(true);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(book.currentPage.ToString()));
        anim.SetTrigger("ToggleClose");
        sceneLoaded = true;
   
        cam.PrioritiseUI(false);
        loadingIcon.SetActive(false);
    }

    void OnSceneUnloaded(Scene scene) // This method runs when a scene is unloaded
    {
        //cam.GetComponentInChildren<CinemachinePOVExtension>().Recenter();
        cam.gameObject.SetActive(false);
        sceneLoaded = false;
    }

}
