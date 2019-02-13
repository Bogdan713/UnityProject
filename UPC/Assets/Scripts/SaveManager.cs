using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SaveManager : MonoBehaviour
{
    public GameObject player;
    public GameObject healer;
    public GameObject enemy;
    public GameObject boss;

    public float saveDelay = 10f;
    public float timeToSave;
    private void Awake()
    {
        timeToSave = saveDelay;
        if (SaveSystem.shoodLoadTheGame)
        {
            Load();
        }
    }

    public void Save()
    {
        GameObject[] h = GameObject.FindGameObjectsWithTag("Healer");
        GameObject[] e = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] b = GameObject.FindGameObjectsWithTag("Boss");
        int level = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Game saving");
        SaveSystem.SaveGame(player, e, b, h, level);
        Debug.Log("Game saved");
        GameObject.FindGameObjectWithTag("MSG").GetComponent<MSGManager>().InstantiateMSG(new Vector3(transform.position.x, transform.position.y-10, transform.position.z), MSGManager.MSGType.Saved);
    }

    public void Load()
    {
        SaveSystem.shoodLoadTheGame = false;
        GameObject[] h = GameObject.FindGameObjectsWithTag("Healer");
        GameObject[] e = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] b = GameObject.FindGameObjectsWithTag("Boss");
        for (int i = 0; i < h.Length; i++)
        {
            Destroy(h[i]);
        }
        for (int i = 0; i < e.Length; i++)
        {
            Destroy(e[i]);
        }
        for (int i = 0; i < b.Length; i++)
        {
            Destroy(b[i]);
        }
        GameData data = SaveSystem.LoadGame();
        if (data != null)
        {
            player.transform.position = new Vector3(data.playerPosition[0], data.playerPosition[1], data.playerPosition[2]);
            player.GetComponent<Character>().health = data.playerHealth;

            for (int i = 0; i < data.ENumber; i++)
            {
                GameObject newE = Instantiate(enemy, new Vector3(data.EPositions[i][0], data.EPositions[i][1], data.EPositions[i][2]), Quaternion.identity);
                newE.GetComponent<Enemy>().health = data.EHealth[i];
            }
            for (int i = 0; i < data.HNumber; i++)
            {
                GameObject newH = Instantiate(healer, new Vector3(data.HPositions[i][0], data.HPositions[i][1], data.HPositions[i][2]), Quaternion.identity);
                newH.GetComponent<Healer>().health = data.HHealth[i];
            }
            for (int i = 0; i < data.BNumber; i++)
            {
                GameObject newB = Instantiate(boss, new Vector3(data.BPositions[i][0], data.BPositions[i][1], data.BPositions[i][2]), Quaternion.identity);
                newB.GetComponent<Boss>().health = data.BHealth[i];
                newB.GetComponent<Boss>().enabled = true;
                newB.GetComponent<CircleCollider2D>().enabled = true;
                newB.GetComponent<Animator>().enabled = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timeToSave > 0)
        {
            timeToSave -= Time.deltaTime;
        }
        else
        {
            Save();
            timeToSave = saveDelay;
        }
    }
}
