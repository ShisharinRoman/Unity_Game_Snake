using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [SerializeField] private float topBoundary;
    [SerializeField] private float bottomBoundary;
    [SerializeField] private float leftBoundary;
    [SerializeField] private float rightBoundary;

    [SerializeField] private GameObject cameraObj;

    [SerializeField] private GameObject hud;
    private HUD                         actionHud;

    [SerializeField] private int        maxFood;

    [SerializeField] private GameObject prefubResultMenu;
    private GameObject                  resultMenu;
    private ResultMenu                  actionResultMenu;

    [SerializeField] private GameObject snake;
    private Head                        actionSnake;

    private bool isTimerWork;
    private bool isLevelEnd;
    public int MaxFood
    {
        get { return maxFood; }
    }
    private int                 eatenFood;
    public int EatenFood
    {
        set { eatenFood = value; } 
        get {  return eatenFood; } 
    }
    private float   time;
    public int Second
    {
        get { return ( int )time % 60; }
    }
    public int Minute
    {
        get { return ( int )time / 60; }
    }
    private int score;
    public int Score
    {
        get { return score; }
    }

    private IEnumerator timer()
    {
        while ( isTimerWork )
        {
            time += Time.deltaTime;
            yield return null;
        }
    }

    void Awake()
    {
        actionHud =         hud.GetComponent<HUD>();
        actionSnake =       snake.GetComponent<Head>();
        var actionCamera =  cameraObj.GetComponent<FollowCamera>();
        actionCamera.Boundary = new Rect
            ( 
            leftBoundary, 
            topBoundary, 
            rightBoundary - leftBoundary, 
            bottomBoundary - topBoundary 
            );
        isTimerWork =   true;
        isLevelEnd =    false;
        StartCoroutine(timer());
    }

    public void gameover()
    {
        isTimerWork =               false;
        actionHud.color =           Color.red;
        resultMenu =                Instantiate( prefubResultMenu );
        actionResultMenu =          resultMenu.GetComponent<ResultMenu>();
        actionResultMenu.IsWin =    false;
        isLevelEnd =                true;
    }

    private void levelComplete()
    {
        isTimerWork =               false;
        actionHud.color =           Color.yellow;
        resultMenu =                Instantiate( prefubResultMenu );
        actionResultMenu =          resultMenu.GetComponent<ResultMenu>();
        actionSnake.SpeedForce =    0;
        actionResultMenu.IsWin =    true;
        isLevelEnd =                true;
    }

    private void Update()
    {
        if ( eatenFood == maxFood && !isLevelEnd )
            levelComplete();
    }
}
