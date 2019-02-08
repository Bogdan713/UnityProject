using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactEffect : MonoBehaviour
{
    private float delay = 1f;
    void Update()
    {
        delay-= Time.deltaTime;
        if (delay <0) {
            Destroy(gameObject);
        }
    }
}
