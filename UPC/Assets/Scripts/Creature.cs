using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeCreature : MonoBehaviour
{
    protected enum AnimationState { Idle, Move }
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    public GameObject deathEffect;

    public float speed;
    public int health;
    public int attack;
    public int reviewDistance;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    protected AnimationState State
    {
        get { return (AnimationState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    protected void MoveTo(Vector3 direction)
    {
        transform.position = Vector3.MoveTowards(transform.position, direction, speed * Time.deltaTime);
        State = AnimationState.Move;
    }
}
