using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    protected enum AnimationState { Idle, Move }
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    public float speed;
    public int lives;

    

    protected AnimationState State
    {
        get { return (AnimationState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    protected void MoveTo(Vector3 direction)
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        State = AnimationState.Move;
    }
}
