using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayWeapon : MonoBehaviour
{
    public Transform player;
    public GameObject shootEffect;
    public LineRenderer lineRenderer;

    public IEnumerator Shoot(Vector2 targetPosition)
    {

            //if (Input.GetButtonDown("Fire1"))

            //Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        Vector2 playerPosition = player.position;
        float playerRadius = player.GetComponent<CircleCollider2D>().radius*20;// real player Radius with scale

        float dx = targetPosition.x - playerPosition.x;
        float dy = targetPosition.y - playerPosition.y;
        float xAdd = dx * playerRadius / (Mathf.Abs(dy) + Mathf.Abs(dx));
        float yAdd = dy * playerRadius / (Mathf.Abs(dy) + Mathf.Abs(dx));

        Vector2 origin = new Vector2(playerPosition.x + xAdd, playerPosition.y + yAdd);

        RaycastHit2D hitInfo = Physics2D.Raycast(origin, new Vector2(dx, dy));
        if (hitInfo)
        { 
            lineRenderer.SetPosition(0, origin);
            lineRenderer.SetPosition(1, hitInfo.point);

            if (hitInfo.transform.tag.Equals("Enemy")) {
                hitInfo.transform.gameObject.GetComponent<Enemy>().TakeDamage(player.GetComponent<Character>().attack);
            }
        }
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.05f);
        lineRenderer.enabled = false;
        //Instantiate(impactEffect, hitInfo.point, Quaternion.identity);
    }
}
