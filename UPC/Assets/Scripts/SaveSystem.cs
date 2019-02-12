using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveSystem
{
    static string path = Application.persistentDataPath + "/gameSaved.fun";
    public static bool shoodLoadTheGame = false;

    public static void SaveGame(GameObject player, GameObject[] e, GameObject[] b, GameObject[] h, int sceneNumber)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(player, e, b, h, sceneNumber);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameData LoadGame()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            GameData data;
            if (stream.Length != 0)
            {
                data = formatter.Deserialize(stream) as GameData;
            }
            else
            {
                Debug.Log("Save file is empty: " + path);
                data = null;
            }
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }
}
