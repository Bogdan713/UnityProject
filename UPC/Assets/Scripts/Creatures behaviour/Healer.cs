using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : SlimeCreature
{
    public Transform target;
    private enum Dir { Towards, Backwards, Wait }
    private Dir dirrection;
    private Dir Dirrection { get => dirrection; set => dirrection = value; }
    public float stagnation;
    public float minSpeed;

    protected void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        health = 5;
        maxHealth = health;
        speed = 4;
        minSpeed = speed;
        attack = -2;
        reviewDistance = 16;
        regeneration = 3f;
        target = FindObjectOfType<Character>().transform;
        dirrection = Dir.Backwards;
        stagnation = 1f;
    }

    void Wait()
    {
        Dirrection = Dir.Wait;
        stagnation = 0.5f;
    }

    void UpdateSpeed()
    {
        if (health > 2)
        {
            speed = minSpeed * Mathf.Log(maxHealth, health);
        }
        else
        {
            speed = minSpeed * 3;
        }
        animator.speed = 1 + (speed / 10f);
    }

    public new void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Character character = collision.collider.GetComponent<Character>();
        if (character != null)
        {
            character.TakeDamage(attack);
            TakeDamage(-attack);
        }
    }

    bool TargetInRange()
    {
        return (target.position - transform.position).magnitude < reviewDistance;
    }

    // Update is called once per frame
    void Update()
    {
        State = AnimationState.Idle;

        if (target != null)
        {

            if (TargetInRange())
            {
                if (Dirrection == Dir.Towards)
                {
                    MoveTo(target.position);
                }
                if (Dirrection == Dir.Backwards)
                {
                    MoveOutOf(target.position);
                }
            }
            if (Dirrection == Dir.Wait)
            {
                if (stagnation > 0)
                {
                    stagnation -= Time.deltaTime;
                    MoveOutOf(target.position);
                }
                else
                {
                    Dirrection = Dir.Backwards;
                }
            }
            if (health < maxHealth)
            {
                health += Time.deltaTime * regeneration;
                if (health > maxHealth)
                {
                    health = maxHealth;
                }
                UpdateSpeed();
            }
        }
    }
}
