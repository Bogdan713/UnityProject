using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : SlimeCreature
{
    public LivesBar livesBar;

    public new void TakeDamage(int damage)
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
        speed = 20;
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
        //State = CharacterState.AttackDown;
    }

    void Update()
    {
        State = AnimationState.Idle;
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            Move();
        }
        if (Input.GetKey("space"))
        {
            Attack();
        }
    }
}
