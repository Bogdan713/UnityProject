using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Character : SlimeCreature
{
    public LivesBar livesBar;
    public float attackDistance;
    RayWeapon rayWeapon;
    bool hasToSayHello = true;
    public new void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        if (health < 0) {
            GameObject.FindGameObjectWithTag("MSG").GetComponent<MSGManager>().InstantiateMSG(transform.position, MSGManager.MSGType.YouDied);
        }
        RefreshLivesBar();
    }

    void Awake()
    {
        livesBar = FindObjectOfType<LivesBar>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        health = 5;
        maxHealth = health;
        speed = 20;
        attack = 2;
        regeneration = 0.2f;
        attackDistance = 15;
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
        //find all targets
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Healer"));
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Boss"));
        float minDistanse = attackDistance + 1;
        int id = 0;
        //find closest
        for (int i = 0; i < enemies.Count; i++)
        {
            if ((enemies[i].transform.position - transform.position).magnitude < minDistanse)
            {
                minDistanse = (enemies[i].transform.position - transform.position).magnitude;
                id = i;
            }
        }
        //attack if it's in range
        if (minDistanse <= attackDistance)
        {
            StartCoroutine(rayWeapon.Shoot(enemies[id].transform.position));
        }

    }
    //cheating
    void KillEveryone()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] bosses = GameObject.FindGameObjectsWithTag("Boss");
        foreach (GameObject enemy in enemies)
        {
            Enemy e1 = enemy.GetComponent<Enemy>();
            if (e1 != null)
            {
                e1.Die();
            }
        }
        foreach (GameObject boss in bosses)
        {
            Boss e1 = boss.GetComponent<Boss>();
            if (e1 != null)
            {
                e1.Die();
            }
        }
    }

    void Update()
    {
        if (hasToSayHello)
        {
            GameObject.FindGameObjectWithTag("MSG").GetComponent<MSGManager>().InstantiateMSG(transform.position, (MSGManager.MSGType)SceneManager.GetActiveScene().buildIndex);
            hasToSayHello = false;
        }
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
        if (Input.GetKeyDown(KeyCode.K))
        {
            KillEveryone();
        }
    }
}
