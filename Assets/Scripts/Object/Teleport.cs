using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Teleport : MonoBehaviour
{

    [SerializeField] GameObject otherTeleport;
    Teleport                    actionOtherTeleport;

    Vector2 teleportPoint;
    public Vector2 TeleportPoint
    {
        get { return teleportPoint; }
    }

    Vector2 otherTeleportPoint;
    public Vector2 OtherTeleportPoint
    {
        get { return otherTeleportPoint; }
    }
    private float               rotation;
    public float                Rotation
    {
        get { return rotation; }
    }

    private bool isActive;
    public bool IsActive
    {
        get { return isActive; }
    }
    // Start is called before the first frame update
    void Start()
    {
        otherTeleportPoint =    actionOtherTeleport.TeleportPoint;
    }

    private void Awake()
    {
        actionOtherTeleport =   otherTeleport.GetComponent<Teleport>();
        teleportPoint =         transform.position;
        rotation =              transform.rotation.eulerAngles.z - actionOtherTeleport.transform.rotation.eulerAngles.z;
    }

    public void reactionToPlayer()
    {
        isActive =                      true;
        actionOtherTeleport.isActive =  true;
    }

    public void reactionToPlayerOut()
    {
        isActive = false;
    }
}
