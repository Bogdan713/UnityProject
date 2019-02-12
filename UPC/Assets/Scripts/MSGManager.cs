using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSGManager : MonoBehaviour
{
    public GameObject MSG;
    public enum MSGType { Idle, Level1, Level2, Level3, Level4, LevelCompleted, YouWin, YouDied, Saved };

    protected void SetSate(Animator animator, MSGType value)
    {
        animator.SetInteger("State", (int)value);
    }

    public void InstantiateMSG(Vector3 position, MSGType type)
    {
        if (MSG != null)
        {
            GameObject msg = Instantiate(MSG, new Vector3(position.x, position.y + 2, -7f), Quaternion.identity);
            SetSate(msg.GetComponent<Animator>(), type);
        }
    }
}