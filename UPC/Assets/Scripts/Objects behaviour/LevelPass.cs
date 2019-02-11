using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelPass : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("Character")) {
            GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
            if (targets.Length == 0) {
                int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
                LevelReachedManager.ReachLevel(nextLevel);
                SceneManager.LoadScene(nextLevel);
            }
        }
    }
}
