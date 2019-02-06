using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayWeapon : MonoBehaviour
{
    public Transform player;
    public GameObject impactEffect;
    public LineRenderer lineRenderer;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        


        //Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        /*
        Vector2 playerPosition = player.position;
        float playerRadius = player.GetComponent<CircleCollider2D>().radius*20;// real player Radius with scale

        float dx = mousePosition.x - playerPosition.x;
        float dy = mousePosition.y - playerPosition.y;
        float xAdd = dx * playerRadius / (Mathf.Abs(dy) + Mathf.Abs(dx));
        float yAdd = dy * playerRadius / (Mathf.Abs(dy) + Mathf.Abs(dx));

        Vector2 origin = new Vector2(playerPosition.x + xAdd, playerPosition.y + yAdd);

        RaycastHit2D hitInfo = Physics2D.Raycast(origin, hit.point);
        if (hitInfo)
        { 

            lineRenderer.SetPosition(0, origin);
            lineRenderer.SetPosition(1, hitInfo.point);

            Debug.Log("origin " + origin);
            Debug.Log("hitInfo.point " + hitInfo.point);
        }
        */
        //Instantiate(impactEffect, hitInfo.point, Quaternion.identity);
    }
}
