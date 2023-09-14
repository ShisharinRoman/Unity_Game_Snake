using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Item
{

    [SerializeField] private GameObject levelManager;
    private LevelManager                actionLevelManager;

    public void Awake()
    {
        actionLevelManager = levelManager.GetComponent<LevelManager>();
    }

    public override void reactionToPlayer( Head player )
    {
        actionLevelManager.EatenFood++;
        transform.gameObject.SetActive( false );
    }
}
