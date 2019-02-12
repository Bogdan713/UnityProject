using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelPass : MonoBehaviour
{
    bool open = false;
    float delay = 3f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("Character"))
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
            if (targets.Length == 0)
            {
                GameObject.FindGameObjectWithTag("MSG").GetComponent<MSGManager>().InstantiateMSG(transform.position, MSGManager.MSGType.LevelCompleted);
                open = true;
            }
        }
    }

    void LoadNextLevel() {
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        LevelReachedManager.ReachLevel(nextLevel);
        SceneManager.LoadScene(nextLevel);
    }
    private void Update()
    {
        if (open) {
            delay -= Time.deltaTime;
            if (delay < 0) {
                LoadNextLevel();
            }
        }
    }
}
