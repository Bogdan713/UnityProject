using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : SlimeCreature
{
    public LivesBar livesBar;
    public float attackDistance;
    RayWeapon rayWeapon;
   // public bool isAttacking;

    public new void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        RefreshLivesBar();
    }
    protected void Awake()
    {
        livesBar = FindObjectOfType<LivesBar>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        health = 5;
        maxHealth = health;
        speed = 20;
        attack = 112;
        regeneration = 0.2f;
        attackDistance = 15;
        //isAttacking = false;
        rayWeapon = GetComponentInChildren<RayWeapon>();
    }

    public void RefreshLivesBar()
    {
        if (livesBar != null)
        {
            livesBar.Refresh();
        }
    }

    void Move()
    {
        Vector3 vectorH = Vector3.right * Input.GetAxis("Horizontal");
        Vector3 vectorV = Vector3.up * Input.GetAxis("Vertical");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + vectorH, speed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, transform.position + vectorV, speed * Time.deltaTime);
        State = AnimationState.Move;
    }

    void Attack()
    {
        List<GameObject> enemies = new List<GameObject>();
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Healer"));
        float minDistanse = attackDistance + 1;
        int id = 0;
        for (int i = 0; i < enemies.Count; i++)
        {
            if ((enemies[i].transform.position - transform.position).magnitude < minDistanse)
            {
                minDistanse = (enemies[i].transform.position - transform.position).magnitude;
                id = i;
            }
        }
        if (minDistanse <= attackDistance) {
                StartCoroutine(rayWeapon.Shoot(enemies[id].transform.position));
        }
        
    }

    void KillEveryone() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies) {
            Enemy e1 = enemy.GetComponent<Enemy>();
            Boss e2 = enemy.GetComponent<Boss>();
            Healer e3 = enemy.GetComponent<Healer>();
            if (e1 != null)
            {
                e1.Die();
            }
            if (e2 != null)
            {
                e2.Die();
            }
            if (e3 != null)
            {
                e3.Die();
            }

        }
    }

    void Update()
    {
        State = AnimationState.Idle;
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            Move();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
        if (health < maxHealth)
        {
            health += Time.deltaTime * regeneration;
            if (health > maxHealth)
            {
                health = maxHealth;
            }
            RefreshLivesBar();
        }
        if (Input.GetKeyDown(KeyCode.K)) {
            KillEveryone();
        }
    }
}
