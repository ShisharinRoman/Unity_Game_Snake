using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class Body : Segment
{
    private Body                actionNextBody;
    public Body                 ActionNextBody
    {
        set { actionNextBody = value; }
    }
    private List<EventBody>    events;
    private void Awake()
    {
        events =            new List<EventBody>();
        spriteRenderer =    GetComponent<SpriteRenderer>();
        rb =                GetComponent<Rigidbody2D>();
        speed =             new Vector2();
        actionNextBody =    null;
    }
    public void addEvent( EventBody newEvent )
    {
        events.Add( newEvent );
    }

    public void triggerEvent()
    {
        switch ( events[0].id )
        {
            case EventBody.Id.Rotate:
                direction =     events[0].direction;
                rb.position =   events[0].posStart;
                break;
            case EventBody.Id.Teleport:
                direction =         events[0].direction;
                speed =             events[0].direction;
                rb.position =       events[0].posEnd.Value;
                break;
        }
        if ( actionNextBody != null )
            actionNextBody.addEvent( events[0] );
        events.RemoveAt( 0 );
    }

    // Update is called once per frame
    void Update()
    {
        if ( events.Count != 0 ) 
        {
            var distanceToEvent = rb.position - events[0].posStart;
            if 
                ( 
                Math.Abs( distanceToEvent.x ) <= Math.Abs( speed.x * Time.deltaTime ) && 
                Math.Abs( distanceToEvent.y ) <= Math.Abs( speed.y * Time.deltaTime )
                )
                triggerEvent();
        }
        move();
        rotation();
    }
}
