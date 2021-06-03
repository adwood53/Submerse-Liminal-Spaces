using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIScaler : MonoBehaviour
{
    RectTransform rect;
    Vector2 uiStartScale;
    Vector2 screenStartRes;
    Vector2 screenChangedRes;

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        uiStartScale = rect.sizeDelta;
        screenStartRes = new Vector2(1920, 1080);
        screenChangedRes = screenStartRes;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 ScreenRes = new Vector2(Screen.width, Screen.height);

        if (screenChangedRes != ScreenRes)
        {
            Vector2 newScale = uiStartScale;
            screenChangedRes = ScreenRes;
            if(screenChangedRes.x < screenStartRes.x)
            {
               newScale.x = uiStartScale.x - (uiStartScale.x / 100) * (((screenStartRes.x - screenChangedRes.x) / screenStartRes.x) * 100);
            }
            else if (screenChangedRes.x > screenStartRes.x)
            {
                newScale.x = uiStartScale.x + (uiStartScale.x / 100) * (((screenChangedRes.x - screenStartRes.x) / screenStartRes.x) * 100);
            }
            if (screenChangedRes.y < screenStartRes.y)
            {
                newScale.y = uiStartScale.y - (uiStartScale.y / 100) * (((screenStartRes.y - screenChangedRes.y) / screenStartRes.y) * 100);
            }
            else if (screenChangedRes.y > screenStartRes.y)
            {
                newScale.y = uiStartScale.y + (uiStartScale.y / 100) * (((screenChangedRes.y - screenStartRes.y) / screenStartRes.y) * 100);
            }
            rect.sizeDelta = newScale;
        }
    }
}
