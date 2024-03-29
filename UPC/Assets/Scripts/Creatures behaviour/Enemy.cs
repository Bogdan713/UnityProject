﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : SlimeCreature
{
    public Transform target;
    private enum Dir { Towards, Backwards, Wait }
    private Dir dirrection;
    private Dir Dirrection { get => dirrection; set => dirrection = value; }
    public float stagnation;

    protected void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        health = 5; 
        maxHealth = health;
        speed = 6;
        attack = 1;
        reviewDistance = 20;
        regeneration = 1f;
        target = FindObjectOfType<Character>().transform;
        dirrection = Dir.Towards;
        stagnation = 1f;
    }

    void Wait()
    {
        Dirrection = Dir.Wait;
        stagnation = 0.5f;
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
            Wait();
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
                    Dirrection = Dir.Towards;
                }
            }
        }
        //progressive, smart AI
        if (health < maxHealth)
        {
            health += Time.deltaTime * regeneration;
            if (health > maxHealth)
            {
                health = maxHealth;
            }

            if (health < maxHealth * 0.25f)
            {
                Dirrection = Dir.Backwards;
            }
            else if (Dirrection != Dir.Wait && health > maxHealth * 0.75f)
            {
                Dirrection = Dir.Towards;
            }
        }
    }
}
