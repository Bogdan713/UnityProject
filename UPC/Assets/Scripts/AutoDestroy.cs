using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float delay;
    private void Awake()
    {
        if (delay < 0) {
            delay = 1;
        }
    }
    void Update()
    {
        delay-= Time.deltaTime;
        if (delay <0) {
            Destroy(gameObject);
        }
    }
}
