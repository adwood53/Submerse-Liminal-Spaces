using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{

    [SerializeField] bool _visualIcon;
    [SerializeField] bool _audioIcon;
    [SerializeField] bool _walkIcon;
    [SerializeField] bool _lookIcon;
    [SerializeField] [Range(0f, 100f)] float timeToFade = 5f;
    [SerializeField] [Range(0.1f, 100f)] float fadeSpeedMultiplier = 1f;
    float fadeSpeed = 0.01f;
    [Space]
    [SerializeField] Sprite[] sprites;
    Image[] icons;
    Image[] iconsBackground;
    TMP_Text[] iconText; 
    TMP_Text enterText;
    public string _visualText = "This is a Visual experience";
    public string _audioText = "This is an Audio experience";
    public string _walkText = "Walk around with the WASD keys";
    public string _lookText = "Look around with your Mouse";


    private void Start()
    {
        fadeSpeed = fadeSpeed * fadeSpeedMultiplier;
        icons = new Image[4];
        iconsBackground = new Image[4];
        iconText = new TMP_Text[4];

        FPSController player = GameObject.FindGameObjectWithTag("Player").GetComponent<FPSController>();
        player.canWalk = _walkIcon;
            
        icons[0] = GameObject.Find("Icons/Icon 1/IconImage").GetComponent<Image>();
        icons[1] = GameObject.Find("Icons/Icon 2/IconImage").GetComponent<Image>();
        icons[2] = GameObject.Find("Icons/Icon 3/IconImage").GetComponent<Image>();
        icons[3] = GameObject.Find("Icons/Icon 4/IconImage").GetComponent<Image>();

        iconsBackground[0] = GameObject.Find("Icons/Icon 1").GetComponent<Image>();
        iconsBackground[1] = GameObject.Find("Icons/Icon 2").GetComponent<Image>();
        iconsBackground[2] = GameObject.Find("Icons/Icon 3").GetComponent<Image>();
        iconsBackground[3] = GameObject.Find("Icons/Icon 4").GetComponent<Image>();

        iconText[0] = GameObject.Find("Icons/Icon 1/IconText").GetComponent<TMP_Text>();
        iconText[1] = GameObject.Find("Icons/Icon 2/IconText").GetComponent<TMP_Text>();
        iconText[2] = GameObject.Find("Icons/Icon 3/IconText").GetComponent<TMP_Text>();
        iconText[3] = GameObject.Find("Icons/Icon 4/IconText").GetComponent<TMP_Text>();
        enterText = GameObject.Find("OpenBrochureBTN/Text (TMP)").GetComponent<TMP_Text>();

        int i = 0;
        if(_visualIcon)
        {
            i++;
            icons[i-1].sprite = sprites[0];
            iconText[i-1].text = _visualText;
        }
        if (_audioIcon)
        {
            i++;
            icons[i-1].sprite = sprites[1];
            iconText[i-1].text = _audioText;
        }
        if (_walkIcon)
        {
            i++;
            icons[i-1].sprite = sprites[2];
            iconText[i-1].text = _walkText;
        }
        if (_lookIcon)
        {
            i++;
            icons[i-1].sprite = sprites[3];
            iconText[i - 1].text = _lookText;
        }

        switch (i)
        {
            default:
                Debug.LogWarning("Hud Manager Incorrect Value in Switch");
                GameObject.Find("Icons/Icon 1").SetActive(false);
                GameObject.Find("Icons/Icon 2").SetActive(false);
                GameObject.Find("Icons/Icon 3").SetActive(false);
                GameObject.Find("Icons/Icon 4").SetActive(false);
                break;
            case 0:
                GameObject.Find("Icons/Icon 1").SetActive(false);
                GameObject.Find("Icons/Icon 2").SetActive(false);
                GameObject.Find("Icons/Icon 3").SetActive(false);
                GameObject.Find("Icons/Icon 4").SetActive(false);
                break;
            case 1:
                GameObject.Find("Icons/Icon 2").SetActive(false);
                GameObject.Find("Icons/Icon 3").SetActive(false);
                GameObject.Find("Icons/Icon 4").SetActive(false);
                break;
            case 2:
                GameObject.Find("Icons/Icon 3").SetActive(false);
                GameObject.Find("Icons/Icon 4").SetActive(false);
                break;
            case 3:
                GameObject.Find("Icons/Icon 4").SetActive(false);
                break;             
            case 4:
                break; 
        }

        StartCoroutine("Fade");
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(timeToFade);
        for (float ft = 1f; ft >= 0; ft -= fadeSpeed)
        {
            for (int i = 0; i < 4; i++)
            {
                iconText[i].color = new Color(iconText[i].color.r, iconText[i].color.g, iconText[i].color.b, iconText[i].color.a - fadeSpeed);
                icons[i].color = new Color(icons[i].color.r, icons[i].color.g, icons[i].color.b, icons[i].color.a - fadeSpeed);
                iconsBackground[i].color = new Color(iconsBackground[i].color.r, iconsBackground[i].color.g, iconsBackground[i].color.b, iconsBackground[i].color.a - fadeSpeed);
            }
            enterText.color = new Color(enterText.color.r, enterText.color.g, enterText.color.b, enterText.color.a - fadeSpeed);
            yield return new WaitForSeconds(.1f);
        }
    }
}
