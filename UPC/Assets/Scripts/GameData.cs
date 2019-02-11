using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameData
{
    public float[] playerPosition;
    public float playerHealth;
    public int ENumber;
    public float[][] EPositions;
    public float[] EHealth;
    public int BNumber;
    public float[][] BPositions;
    public float[] BHealth;
    public int HNumber;
    public float[][] HPositions;
    public float[] HHealth;
    public int SceneNumber;
    public GameData(GameObject player, GameObject[] e, GameObject[] b, GameObject[] h, int scene) {
        SceneNumber = scene;
        playerPosition = new float[3];
        playerPosition[0] = player.transform.position.x;
        playerPosition[1] = player.transform.position.y;
        playerPosition[2] = player.transform.position.z;
        playerHealth = player.GetComponent<Character>().health;

        ENumber = e.Length;
        EPositions = new float[e.Length][];
        EHealth = new float[e.Length];
        for (int i = 0; i < e.Length; i++)
        {
            EPositions[i] = new float[3];
            EPositions[i][0] = e[i].transform.position.x;
            EPositions[i][1] = e[i].transform.position.y;
            EPositions[i][2] = e[i].transform.position.z;
            EHealth[i] = e[i].GetComponent<Enemy>().health;
        }

        BNumber = b.Length;
        BPositions = new float[b.Length][];
        BHealth = new float[b.Length];
        for (int i = 0; i < b.Length; i++)
        {
            BPositions[i] = new float[3];
            BPositions[i][0] = b[i].transform.position.x;
            BPositions[i][1] = b[i].transform.position.y;
            BPositions[i][2] = b[i].transform.position.z;
            BHealth[i] = b[i].GetComponent<Boss>().health;
        }

        HNumber = h.Length;
        HPositions = new float[h.Length][];
        HHealth = new float[h.Length];
        for (int i = 0; i < h.Length; i++)
        {
            HPositions[i] = new float[3];
            HPositions[i][0] = h[i].transform.position.x;
            HPositions[i][1] = h[i].transform.position.y;
            HPositions[i][2] = h[i].transform.position.z;
            HHealth[i] = h[i].GetComponent<Healer>().health;
        }
    }
}
