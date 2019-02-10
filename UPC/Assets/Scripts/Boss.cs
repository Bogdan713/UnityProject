using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : SlimeCreature
{
    public Transform target;
    private enum Dir { Towards, Backwards, Wait }
    private Dir dirrection;
    private Dir Dirrection { get => dirrection; set => dirrection = value; }
    public float stagnation;
    public GameObject Child;

    protected void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (health <= 0)
        {
            health = 50;
        }
        maxHealth = health;
        if (speed <= 0)
        {
            speed = 7;
        }
        if (attack <= 0)
        {
            attack = 2;
        }
        if (attack <= 0)
        {
            reviewDistance = 20;
        }
        if (attack <= 0)
        {
            regeneration = 1f;
        }
        target = FindObjectOfType<Character>().transform;
        dirrection = Dir.Wait;
        stagnation = 1f;
    }

    void Wait()
    {
        Dirrection = Dir.Wait;
        stagnation = 0.5f;
    }

    void Attack()
    {
        //State = CharacterState.AttackDown;
    }

    public new void TakeDamage(float damage)
    {
        if (Child != null && Random.value > 0.5)
        {
            Instantiate(Child, new Vector3(transform.position.x, transform.position.y + 1, Child.transform.position.z), Quaternion.identity);
        }
        base.TakeDamage(damage);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Character character = collision.collider.GetComponent<Character>();
        if (character != null)
        {
            character.TakeDamage(attack);
            Wait();
            //Debug.Log("Character takes " + attack + " damage from Enemy");
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

        //MoveTo(transform.position - target.position);//run away



        if (target != null)
        {

            if (TargetInRange())
            {
                if (Dirrection == Dir.Towards)
                {
                    MoveTo(target.position);//run to
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

        if (health < maxHealth)
        {
            health += Time.deltaTime * regeneration;
            if (health > maxHealth)
            {
                health = maxHealth;
            }
        }
    }
}
