using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterState { Idle, Move }

public class Character : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public float speed = 4f;
    //public float jumpForce = 1f;
    public int lives;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        lives = 5;
    }

    private CharacterState State
    {
        get { return (CharacterState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    void Move()
    {
        Vector3 vectorH = Vector3.right * Input.GetAxis("Horizontal");
        Vector3 vectorV = Vector3.up * Input.GetAxis("Vertical");
        print(Input.GetAxis("Horizontal"));
        print(Input.GetAxis("Vertical"));
        transform.position = Vector3.MoveTowards(transform.position, transform.position + vectorH, speed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, transform.position + vectorV, speed * Time.deltaTime);
        State = CharacterState.Move;
    }

    void Attack()
    {
        //State = CharacterState.AttackDown;
    }

    bool canMove(int dir)
    {
        Vector3 position = transform.position;
        //change on grow

        //if(  (new Vector3(position.x+x, position.y+ y)) )
        transform.GetComponent<Collider2D>();
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        State = CharacterState.Idle;
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
