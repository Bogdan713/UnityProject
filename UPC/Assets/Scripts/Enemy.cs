using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : SlimeCreature
{
    public Transform target;

    protected void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        health = 5;
        speed = 5;
        attack = 1;
        reviewDistance = 20;
        target = FindObjectOfType<Character>().transform;
    }
    void AvoidPlayer() {
        Vector3 targetPosition = target.position;
        if ((targetPosition - transform.position).magnitude < 20)
        {
            MoveTo(transform.position - targetPosition);
        }
    }
    void Attack()
    {
        //State = CharacterState.AttackDown;
    }
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        Character character = collision.GetComponent<Character>();
        if (character != null)
        {
            character.TakeDamage(attack);
            Debug.Log("Character takes " + attack + " damage from Enemy");
        }
        
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Character character = collision.collider.GetComponent<Character>();
        if (character != null)
        {
            character.TakeDamage(attack);
            Debug.Log("Character takes " + attack + " damage from Enemy");
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        State = AnimationState.Idle;

        //MoveTo(transform.position - target.position);//run away
        if (target!=null) {
            MoveTo(target.position);//run to
        }
    }
}
