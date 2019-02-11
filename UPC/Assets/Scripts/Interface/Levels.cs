using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    public GameObject[] levels = new GameObject[LevelReachedManager.levelsNumber];
    public GameObject levelList;
    private void Awake()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i] = levelList.transform.GetChild(i).gameObject;
        }
        Refresh();
    }
    public void Refresh()
    {
        LevelReachedManager.InitializeFromFile();
        for (int i = 0; i < levels.Length; i++)
        {
            if (LevelReachedManager.IsLevelReached(i + 1))
            {
                levels[i].GetComponent<Button>().interactable = true;
            }
            else
            {
                levels[i].GetComponent<Button>().interactable = false;
            }
        }
    }
    public void OnLevelClick(int scene)
    {
        LevelReachedManager.InitializeFromFile();
        if (LevelReachedManager.IsLevelReached(scene))
        {
            SceneManager.LoadScene(scene);
        }
    }
}
