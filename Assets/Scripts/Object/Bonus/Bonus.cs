using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : Item
{
    protected const int DEFAULT_TIME_ACTIVITY = 5;


    private new BoxCollider2D   collider;
    private SpriteRenderer      spriteRenderer;
    protected Head              actionPlayer;
    protected float             timeActive;

    private void Start()
    {
        collider =          GetComponent<BoxCollider2D>();
        spriteRenderer =    GetComponent<SpriteRenderer>();
    }

    protected IEnumerator timer()
    {
        while ( timeActive > 0 )
        {
            timeActive -= Time.deltaTime;
            yield return null;
        }
        loseEffect();
    }

    public override void reactionToPlayer( Head player )
    {
        actionPlayer =              player;
        collider.enabled =          false;
        spriteRenderer.enabled =    false;

        giveEffect( player );

        StartCoroutine( timer() );
    }

    public virtual void giveEffect( Head player )
    {
    }

    public virtual void loseEffect()
    {
    }
}
