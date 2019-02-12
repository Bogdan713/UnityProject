using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetProgress : MonoBehaviour
{
    private Button button;
    public Transform levelsManager;

    private void Awake()
    {
        button = transform.gameObject.GetComponent<Button>();
        Refresh();
    }

    public void Refresh()
    {
        LevelReachedManager.InitializeFromFile();
        if (LevelReachedManager.LevelReached > 1)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
        if (levelsManager != null)
        {
            levelsManager.GetComponent<Levels>().Refresh();
        }
    }

    public void Reset()
    {
        LevelReachedManager.Reset();
        Refresh();
    }
}
