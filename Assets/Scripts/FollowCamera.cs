using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    /*
    * A rectangle in which can
    * move the camera.
     */
    private Rect    boundary; 
    public Rect     Boundary
    {
        set { boundary = value; }
    }

    [SerializeField] GameObject player;

    /*
    * Maximum possible distance
    * between the player and the center of the camera.
    * When going beyond its limit ,
    * camera movement.
     */
    [SerializeField] float      maxDistance;

    Vector3                     offset;
    float halfHeight;
    float halfWidth;

    void Start()
    {
        var camera =    Camera.main;
        halfHeight =    camera.orthographicSize;
        halfWidth =     camera.aspect * halfHeight;
    }

    void inBoundary()
    {
        if ( transform.position.y - halfHeight < boundary.yMax  )
            transform.position = new Vector3( transform.position.x, boundary.yMax + halfHeight, -10 );
        if ( transform.position.y + halfHeight > boundary.yMin )
            transform.position = new Vector3( transform.position.x, boundary.yMin - halfHeight, -10 );
        if ( transform.position.x - halfWidth < boundary.xMin )
            transform.position = new Vector3( boundary.xMin + halfWidth, transform.position.y, -10 );
        if ( transform.position.x + halfWidth > boundary.xMax )
            transform.position = new Vector3( boundary.xMax - halfWidth, transform.position.y, -10 );
    }

    private void Update()
    {
    }
    void LateUpdate()
    {
        // Checking whether the player has gone beyond the maximum distance from the center camera
        if ((( Vector2 )player.transform.position - ( Vector2 )transform.position ).sqrMagnitude <= maxDistance * maxDistance )
            offset = transform.position - player.transform.position;
        else
            transform.position = ( offset + player.transform.position );
        inBoundary();
    }
}
