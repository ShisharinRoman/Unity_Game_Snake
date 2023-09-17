using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class FollowCamera : MonoBehaviour
{
    /// <summary>
    /// A rectangle in which can
    /// move the camera. 
    /// </summary>
    private Rect    boundary; 
    public Rect     Boundary
    {
        set { boundary = value; }
    }

    [SerializeField] GameObject player;
    
    /// <summary>
    ///  Maximum possible distance
    /// between the player and the center of the camera.
    /// When going beyond its limit,
    /// camera movement.
    /// </summary>
    [SerializeField] float      maxDistance;

    Vector3                     offset;

    float                       halfHeight;
    float                       halfWidth;

    void Awake()
    {
        var camera =    Camera.main;
        halfHeight =    camera.orthographicSize;
        halfWidth =     camera.aspect * halfHeight;

        var playerPosition = player.transform.position;
        offset = transform.position - playerPosition;
    }

    void inBoundary()
    {
        if ( transform.position.y - halfHeight < boundary.yMax )
        { 
            transform.position = new Vector3( transform.position.x, boundary.yMax + halfHeight, -10 );
        }
        if ( transform.position.y + halfHeight > boundary.yMin )
        {
            transform.position = new Vector3( transform.position.x, boundary.yMin - halfHeight, -10 );
        }
        if ( transform.position.x - halfWidth < boundary.xMin )
        {
            transform.position = new Vector3( boundary.xMin + halfWidth, transform.position.y, -10 );
        }
        if ( transform.position.x + halfWidth > boundary.xMax )
        {
            transform.position = new Vector3( boundary.xMax - halfWidth, transform.position.y, -10 );
        }
    }

    void LateUpdate()
    {
        var playerPosition = player.transform.position;

        // Checking whether the player has gone beyond the maximum distance from the center camera
        if (( playerPosition - transform.position ).sqrMagnitude < maxDistance * maxDistance * transform.position.z )
        {
            offset = transform.position - playerPosition;
        }
        else
        {
            transform.position = offset + playerPosition;
        }
        inBoundary();
    }
}
