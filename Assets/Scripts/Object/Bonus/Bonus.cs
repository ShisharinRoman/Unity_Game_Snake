using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : Item
{

    protected const int DEFAULT_TIME_ACTIVITY = 5;

    protected Head      actionPlayer;
    protected float     timeActive;
    private void Awake()
    {
        timeActive = 0;
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
        actionPlayer = player;

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
