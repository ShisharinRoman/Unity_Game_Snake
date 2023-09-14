using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour
{
    protected SpriteRenderer    spriteRenderer;
    public Color color
    {
        set { spriteRenderer.color = value; }
        get { return spriteRenderer.color; }
    }
    protected Rigidbody2D       rb;
    protected bool              isCanMoving;

    [SerializeField]protected float   speedForce;
    public float      SpeedForce
    {
        set { speedForce = value; }
        get { return speedForce; }
    }
    protected float multSpeedForce;
    public float    MultSpeedForce
    {
        set { multSpeedForce = value; }
        get { return multSpeedForce; }
    }
    protected Vector2   direction;
    public Vector2      Direction
    {
        set { direction = value; }
        get { return direction; }
    }
    protected Vector2 speed;

    protected void rotation()
    {
        float rotation = 0;

        if (direction.x < 0)        rotation = 90;
        else if (direction.x > 0)   rotation = 270;
        else if (direction.y < 0)   rotation = 180;
        else if (direction.y > 0)   rotation = 0;

        transform.rotation = Quaternion.Euler( 0f, 0f, rotation );
    }
    protected void move()
    {
        speed =         speedForce * direction * multSpeedForce;
        rb.position +=  speed * Time.deltaTime;
    }
}
