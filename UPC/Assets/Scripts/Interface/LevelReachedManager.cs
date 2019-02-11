using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelReachedManager
{
    private static int levelReached = 1;
    public const int levelsNumber = 4;
    public static int LevelReached { get => levelReached; }
    private static string path = Application.persistentDataPath + "/levelReached.txt";
    public static bool IsLevelReached(int level)
    {
        if (level > 0 && level <= levelsNumber && level <= levelReached)
        {
            return true;
        }
        return false;
    }
    public static void InitializeFromFile() {
        
        int levelFromFile = 1;
        try
        {
            if (File.Exists(path))
            {
                StreamReader streamReader = new StreamReader(path, System.Text.Encoding.Default);
                levelFromFile = Int32.Parse(streamReader.ReadLine());
                streamReader.Close();
            }
            else {
                File.Create(path);

                FixSuccess();
            }
        }
        catch (Exception e) {
            Debug.Log(e.Message);
        }
        finally {
            if (levelFromFile > 0 && levelFromFile <= levelsNumber)
            {
                levelReached = levelFromFile;
                
            }
        }
    }
    public static void ReachLevel(int level = 1)
    {
        Debug.Log("Reached level "+ level);
        if (level > 0 && level <= levelsNumber && level > levelReached)
        {
            levelReached = level;
        }
        FixSuccess();
    }

    private static void FixSuccess()
    {
        StreamWriter streamWriter = new StreamWriter(path, false, System.Text.Encoding.Default);
        streamWriter.Write(levelReached);
        streamWriter.Close();
    }

    public static void Reset() {
        levelReached = 1;
        FixSuccess();
    }
}
