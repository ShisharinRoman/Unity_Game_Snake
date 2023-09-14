using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class HUD : MonoBehaviour
{
    [SerializeField] private GameObject cameraObj;

    [SerializeField] private GameObject levelManager;
    LevelManager                        actionLevelManager;

    [SerializeField] private GameObject prefubTextBox;

    private GameObject                  timerText;
    private TextBox                     actionTimerText;

    private GameObject                  foodText;
    TextBox                             actionFoodText;

    private int                         eatenFood
    {
        get { return actionLevelManager.EatenFood; }
    }
    private int                         maxFood
    {
        get { return actionLevelManager.MaxFood; }
    }
    private int second
    {
        get { return actionLevelManager.Second; }
    }
    private int minute
    {
        get { return actionLevelManager.Minute; }
    }
    public Color color
    {
        set
        {
            actionTimerText.color = value;
            actionFoodText.color =  value;
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        actionLevelManager =        levelManager.GetComponent<LevelManager>();

        timerText =                 Instantiate( prefubTextBox, new Vector3( 0, 0 ,-2 ), transform.rotation );
        actionTimerText =           timerText.GetComponent<TextBox>();
        actionTimerText.FontSize =  5;

        foodText=                   Instantiate( prefubTextBox, new Vector3(0, 0, -2), transform.rotation);
        actionFoodText =            foodText.GetComponent<TextBox>();
        actionFoodText.FontSize =   5;

    }   
    private void LateUpdate()
    {
        var cameraPosition = Camera.main.transform.position;

        timerText.transform.position =  cameraPosition + new Vector3( 0, 4.5F, 1 );
        actionTimerText.Str =           string.Format("Time {0:d2} : {1:d2}", minute, second);



        foodText.transform.position =   cameraPosition + new Vector3( 5, 4.5F, 1 );
        actionFoodText.Str =            string.Format("Food : {0} / {1}", eatenFood, maxFood); ;
    }
}
