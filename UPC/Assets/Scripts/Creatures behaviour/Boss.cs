using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : SlimeCreature
{
    public Transform target;//Player transform
    private enum Dir { Towards, Backwards, Wait }//Behaviour types
    private Dir dirrection;//Behaviour
    private Dir Dirrection { get => dirrection; set => dirrection = value; }
    public float stagnation;//delay after attack
    public float potention;//how often generate children
    public GameObject Child;

    protected void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (health <= 0)
        {
            health = 30 + (SceneManager.GetActiveScene().buildIndex*20);
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
        if (potention <= 0)
        {
            potention = 0.5f;
        }
        if (attack <= 0)
        {
            regeneration = 1f;
        }
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
        //generate children with some chance
        if (Child != null && Random.value > potention)
        {
            Instantiate(Child, new Vector3(transform.position.x, transform.position.y, Child.transform.position.z), Quaternion.identity);
        }
        base.TakeDamage(damage);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Character character = collision.collider.GetComponent<Character>();
        //demand player on collision
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
        //behaviour
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
        //regeneration
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
