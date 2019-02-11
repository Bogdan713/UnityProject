using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesBar : MonoBehaviour
{
    private Transform[] hearts = new Transform[5];

    private Character character;

    public void Awake()
    {
        character = FindObjectOfType<Character>();

        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i] = transform.GetChild(i);
        }
    }

    public void Refresh()
    {
        if (character != null) {
            for (int i = 0; i < hearts.Length; i++)
            {
                if (i < character.health)
                {
                    hearts[i].gameObject.SetActive(true);
                }
                else
                {
                    hearts[i].gameObject.SetActive(false);

                }
            }
        }

    }
}
