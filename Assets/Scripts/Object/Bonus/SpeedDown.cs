using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedDown : Bonus
{
    

    public void Awake()
    {
        timeActive = DEFAULT_TIME_ACTIVITY;
    }

    public override void giveEffect( Head player )
    {
        if ( actionPlayer.MultSpeedForce > 1 )
            actionPlayer.MultSpeedForce--;
        else
            actionPlayer.MultSpeedForce /= 2;

        actionPlayer.color -= new Color( 0, 0.25F, -0.25F, 0 );

    }

    public override void loseEffect()
    {
        if ( actionPlayer.MultSpeedForce >= 1 )
            actionPlayer.MultSpeedForce++;
        else
            actionPlayer.MultSpeedForce *= 2;

        actionPlayer.color -= new Color(0, -0.25F, 0.25F, 0);

        transform.gameObject.SetActive( false );
    }
}
