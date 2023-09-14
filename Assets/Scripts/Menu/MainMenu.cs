using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainMenu : MenuSystem
{
    private const int                   MAX_MENU =  2;
    private static readonly int[]       MAX_STATE = { 4, 0 };
    private static readonly int[]       MIN_STATE = { 1, 0 };

    [SerializeField] GameObject         gameManager;
    SceneManager                        actionGameManager;

    [SerializeField] GameObject         buttonLevelSelect;
    Button                              actionButtonLevelSelect;
    [SerializeField] GameObject         buttonExit;
    Button                              actionButtonExit;
   
    private void Awake()
    {
        numbMenu =                      MAX_MENU;
        state =                         new int[numbMenu];
        maxState =                      new int[numbMenu];
        minState =                      new int[numbMenu];
        actionGameManager =             gameManager.GetComponent<SceneManager>();
        actionButtonLevelSelect =       buttonLevelSelect.GetComponent<Button>();
        actionButtonExit =              buttonExit.GetComponent<Button>();

        for ( int i = 0; i < numbMenu; i++ )
        {
            maxState[i] =   MAX_STATE[i];
            minState[i] =   MIN_STATE[i];
            state[i] =      minState[i];
        }
    }

    private void Start()
    {
        actionButtonLevelSelect.Str =   "Level : 1";
        actionButtonExit.Str =          "Exit";
    }
    protected override void triggerSelectMenu()
    {
        if ( targetMenu == 1 )
            Application.Quit();
        if ( targetMenu == 0 )
        {
            actionGameManager.goToScene( state[targetMenu] );
        }
    }

    // Update is called once per frame
    void Update()
    {
        actionButtonExit.color =        Color.white;
        actionButtonLevelSelect.color = Color.white;

        switch ( targetMenu )
        {
            case 0:
                actionButtonLevelSelect.Str =   "Level : " + state[0];
                actionButtonLevelSelect.color = Color.red;
                break;
            case 1: 
                actionButtonExit.color =        Color.red;
                break;
        }
        chooseMenu();
        selectMenu();
    }
}
