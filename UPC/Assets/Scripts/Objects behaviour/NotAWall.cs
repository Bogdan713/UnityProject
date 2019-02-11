using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotAWall : MonoBehaviour
{
    public GameObject impactEffect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("Character"))
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject[] targets2 = GameObject.FindGameObjectsWithTag("Boss");

            if (targets.Length == 0 && targets2.Length == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    Instantiate(impactEffect, new Vector3(transform.position.x + i, transform.position.y + 2f, -6f), Quaternion.identity);
                }
                Destroy(gameObject);
            }
        }
    }
}
