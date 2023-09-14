using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResultMenu : MenuSystem
{
    private const int                   MAX_MENU =  2;
    private static readonly int[]       MAX_STATE = { 0, 0 };
    private static readonly int[]       MIN_STATE = { 0, 0 };

    bool                                isWin;
    public bool IsWin
    {
        set { isWin = value; }
        get { return isWin; }
    }

    GameObject                          sceneManager;
    SceneManager                        actionSceneManager;

    [SerializeField] GameObject         prefubBackground;
    private GameObject                  background;
    ResultMenuBackground                actionBackground;

    [SerializeField] GameObject         prefubButton;

    private GameObject                  upButton;
    private Button                      actionUpButton;
    private GameObject                  downButton;
    private Button                      actionDownButton;

    private void Awake()
    {
        numbMenu =                      MAX_MENU;
        state =                         new int[numbMenu];
        maxState =                      new int[numbMenu];
        minState =                      new int[numbMenu];

        sceneManager =                  GameObject.FindGameObjectWithTag( "SceneManager" );
        actionSceneManager =            sceneManager.GetComponent<SceneManager>();

        for ( int i = 0; i < numbMenu; i++ )
        {
            maxState[i] =   MAX_STATE[i];
            minState[i] =   MIN_STATE[i];
            state[i] =      minState[i];
        }

        var position =      Camera.main.transform.position;
        position.z =        -2;
        background =        Instantiate( prefubBackground, new Vector3( position.x, position.y, -2 ), transform.rotation );
        actionBackground =  background.GetComponent<ResultMenuBackground>();

        upButton =          Instantiate( prefubButton, new Vector3( position.x, position.y, -3 ), transform.rotation );
        actionUpButton =    upButton.GetComponent<Button>();

        downButton =            Instantiate( prefubButton, new Vector3( position.x, position.y - 2, -3 ) , transform.rotation );
        actionDownButton =      downButton.GetComponent<Button>();
        actionDownButton.Str =  "Main Menu";

    }

    private void Start()
    {
        if ( isWin )
        {
            actionBackground.Color =    Color.yellow;
            actionBackground.Str =      "YOU WIN!!!";
            actionUpButton.Str =        "Next";
        }
        else
        {
            actionBackground.Color =    Color.red;
            actionBackground.Str =      "YOU LOSE!!!";
            actionUpButton.Str =        "Restart";
        }
    }

    protected override void triggerSelectMenu()
    {
        if ( targetMenu == 0 )
            if ( isWin )
                actionSceneManager.goToNextScene();
            else
                actionSceneManager.resetScene();
        if ( targetMenu == 1 )
            actionSceneManager.goToScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        actionUpButton.color =      Color.white;
        actionDownButton.color =    Color.white;

        switch ( targetMenu )
        {
            case 0:
                actionUpButton.color = Color.red;
                break;
            case 1:
                actionDownButton.color = Color.red;
                break;
        }
        chooseMenu();
        selectMenu();
    }
}
