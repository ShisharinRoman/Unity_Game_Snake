using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventId
{
    Rotate,
    Teleport
}

public struct EventBody
{
    public enum Id
    {
        Rotate,
        Teleport
    }

    public Id       id;
    public Vector2  posStart;
    public Vector2  direction;
    public Vector2? posEnd;

    public EventBody( Vector2 position, Vector2 _direction )
    {
        id =            Id.Rotate;
        posStart =      position;
        posEnd =        null;
        direction =     _direction;
    }

    public EventBody( Vector2 positionStart, Vector2 positionEnd, Vector2 _direction )
    {
        id =        Id.Teleport;
        posStart =  positionStart;
        posEnd =    positionEnd;
        direction = _direction;
    }

}