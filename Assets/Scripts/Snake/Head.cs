using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
public class Head : Segment
{
    [SerializeField] GameObject         levelManager;
    LevelManager                        actionLevelManager;

    [SerializeField] GameObject         prefubBody;
    [SerializeField] float              distanceBody;
    List<GameObject>                    body;
    List<Body>                          actionBody;
    Body                                actionTail;
    Vector2                             lastRotatePosition;

    void Awake()
    {
        isCanMoving =           true;
        speed =                 new Vector2();
        lastRotatePosition =    new Vector2( float.MaxValue, float.MaxValue );
        body =                  new List<GameObject>();
        actionBody =            new List<Body>();
        rb =                    GetComponent<Rigidbody2D>();
        spriteRenderer =        GetComponent<SpriteRenderer>();
        actionLevelManager =    levelManager.GetComponent<LevelManager>();
        direction =             Vector2.up;
        multSpeedForce =        1;
        actionTail =            null;
    }

    private void addSegment()
    {
        if ( body.Count == 0 )
        {
            body.Add( Instantiate
                ( 
                prefubBody, 
                ( Vector2 )transform.position - direction * distanceBody, 
                transform.rotation
                ));
            actionBody.Add( body.Last().GetComponent<Body>() );
        }
        else
        {
            body.Add( Instantiate
                (
                prefubBody, 
                ( Vector2 )actionTail.transform.position - actionTail.Direction * distanceBody, 
                transform.rotation
                ));
            actionBody.Add( body.Last().GetComponent<Body>() );
            actionTail.ActionNextBody = actionBody.Last();
        }
        var prevActionTail =    actionTail;
        actionTail =            actionBody.Last();
        actionTail.color =      spriteRenderer.color;
        actionTail.SpeedForce = prevActionTail?.SpeedForce ?? speedForce;
        actionTail.Direction =  prevActionTail?.Direction ?? direction;
    }

    private void reactionToBody( Collider2D other ) 
    {
        if ( body.Count > 1 && other.gameObject != body[0] && other.gameObject != body[1] )
            gameover();
    }

    private void reactionToWall() 
    {
        gameover();
    }

    private void gameover()
    {
        spriteRenderer.color =  Color.red;
        speedForce =            0;
        isCanMoving =           false;
        actionLevelManager.gameover();
    }
    private void OnTriggerEnter2D( Collider2D other )
    {
        switch ( other.gameObject.tag )
        {
            case "Food":
                other.GetComponent<Item>().reactionToPlayer( this );
                addSegment();
                break;
            case "Bonus":
                other.GetComponent<Item>().reactionToPlayer( this );
                break;
            case "Body":
                reactionToBody( other );
                break;
            case "Teleport":
                var actionOther = other.GetComponent<Teleport>();
                teleport( actionOther );
                actionOther.reactionToPlayer();
                break;
        }
    }

    private void OnTriggerExit2D( Collider2D other )
    {
        if ( other.gameObject.CompareTag( "Teleport" ))
            other.GetComponent<Teleport>().reactionToPlayerOut();
    }

    private void OnCollisionEnter2D( Collision2D other )
    {
        if ( other.gameObject.tag == "Wall" ) 
            reactionToWall();
    }
    private void rotate()
    {
        bool isRotate = false;

        if ( direction.y == 0 && Math.Abs( lastRotatePosition.x - rb.position.x ) > 1.1 )
        {
            if ( Input.GetKey( KeyCode.UpArrow ))
            {
                direction = Vector2.up;
                isRotate =  true;
            }
            if ( Input.GetKey( KeyCode.DownArrow ))
            {
                direction = Vector2.down;
                isRotate =  true;
            }
        }
        else if ( direction.x == 0 && Math.Abs( lastRotatePosition.y - rb.position.y ) > 1.1 )
        {
            if ( Input.GetKey( KeyCode.LeftArrow ))
            {
                direction = Vector2.left;
                isRotate =  true;
            }
            if ( Input.GetKey( KeyCode.RightArrow ))
            {
                direction = Vector2.right;
                isRotate =  true;
            }
        }

        if ( isRotate )
        {
            lastRotatePosition = rb.position;
            if ( body.Count > 0 )
                actionBody[0].addEvent( new EventBody( rb.position, direction ));
        }
    }

    private void teleport( Teleport actionOther )
    {
        if ( actionOther.IsActive )
            return;

        Vector2 startPosition =             rb.position;
        Vector2 distanceToStartPosition =   startPosition - actionOther.TeleportPoint;

        switch ( actionOther.Rotation )
        {
            case -90:
            case 270:
                direction =                 new Vector2( direction.y, -direction.x );
                distanceToStartPosition =   new Vector2( distanceToStartPosition.y, distanceToStartPosition.x );
                break;
            case 90:
            case -270:
                direction =                 new Vector2( -direction.y, direction.x );
                distanceToStartPosition =   new Vector2( distanceToStartPosition.y, distanceToStartPosition.x );
                break;
            case 180:
            case -180:
                break;
            case 0:
                direction = -direction;
                break;
        }

        rb.position = actionOther.OtherTeleportPoint + distanceToStartPosition;

        if ( body.Count > 0 )
            actionBody[0].addEvent( new EventBody( startPosition, rb.position, direction ));
    }
    void Update()
    {
        if ( isCanMoving )
        {
            rotate();
            move();
            rotation();
        }
        foreach( var item in actionBody )
        {
            item.MultSpeedForce =   multSpeedForce;
            item.SpeedForce =       speedForce;
            item.color =            spriteRenderer.color;
        }
    }
}
