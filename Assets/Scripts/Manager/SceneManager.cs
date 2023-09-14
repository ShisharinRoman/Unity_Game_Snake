using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private string[]   scene;
    [SerializeField] private int        thisSceneIndex;

    public void goToScene( int index )
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene( scene[index] );
    }

    public void goToNextScene()
    {
        if ( thisSceneIndex + 1 < scene.Length )
            UnityEngine.SceneManagement.SceneManager.LoadScene( scene[thisSceneIndex + 1] );
        else
            UnityEngine.SceneManagement.SceneManager.LoadScene( scene[0] );
    }

    public void resetScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene( scene[thisSceneIndex] );
    }

}
