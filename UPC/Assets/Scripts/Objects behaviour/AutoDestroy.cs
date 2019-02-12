using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Destroy object after some time
public class AutoDestroy : MonoBehaviour
{
    public float delay;//time to destroy
    private void Awake()
    {
        if (delay < 0)
        {
            delay = 1;
        }
    }
    void Update()
    {
        delay -= Time.deltaTime;
        if (delay < 0)
        {
            Destroy(gameObject);
        }
    }
}
