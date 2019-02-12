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
    public float health;
    public float maxHealth;
    public float attack;
    public float reviewDistance;
    public float regeneration;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Instantiate(deathEffect, new Vector3(transform.position.x, transform.position.y, deathEffect.transform.position.z), Quaternion.identity);
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

    protected void MoveSubjTo(Vector3 direction)
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        State = AnimationState.Move;
    }

    protected void MoveOutOf(Vector3 direction)
    {
        MoveSubjTo(transform.position - direction);
    }
}
