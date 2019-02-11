using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Transform player;
    Vector3 positionTarget;
    public void LateUpdate()
    {
        if (player != null)
        {
            positionTarget = player.position;
            positionTarget.z = transform.position.z;
            transform.position = positionTarget;
        }
    }
}
