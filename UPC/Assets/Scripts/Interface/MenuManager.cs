using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour {
    public GameObject continueButton;
    private void Awake()
    {
        LevelReachedManager.InitializeFromFile();
        CheckContinue();
    }

    public void OnExitPressed()
    {
        Application.Quit();
    }

    void CheckContinue() {
        GameData gameData = SaveSystem.LoadGame();
        if (gameData != null)
        {
            continueButton.GetComponent<Button>().interactable = true;
        }
        else {
            continueButton.GetComponent<Button>().interactable = false;
        }
    }

    public void OnContinuePressed()
    {
        Debug.Log("OnContinuePressed");
        CheckContinue();
        GameData gameData = SaveSystem.LoadGame();
        if (gameData != null) {
            SaveSystem.shoodLoadTheGame = true;
            Debug.Log("LoadScene" + gameData.SceneNumber);
            SceneManager.LoadScene(gameData.SceneNumber);
        }
    }



}
