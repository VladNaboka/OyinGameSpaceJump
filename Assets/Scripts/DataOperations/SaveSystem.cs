using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Newtonsoft.Json;

public class SaveSystem
{
    public static string SAVE_PATH = Application.persistentDataPath + "/save" + ".json";

    public static void CreateDataFile(GameData saveData)
    {
        try
        {
            if(File.Exists(SAVE_PATH))
            {
                Debug.Log("Data already exists!");
                return;
            }
            else
            {
                Debug.Log("Data does not exist. Creating new file!");
            }
            using FileStream stream = File.Create(SAVE_PATH);
            stream.Close();
            File.WriteAllText(SAVE_PATH, JsonConvert.SerializeObject(saveData));
        }
        catch (Exception e)
        {
            Debug.LogError("Error while creating data: " + e.Message);
        }
    }

    public static void SaveData(GameData levelData)
    {
        try
        {
            if(File.Exists(SAVE_PATH))
            {
                Debug.Log("Data exists. Deleting old file and writing a new one!");
                File.Delete(SAVE_PATH);
            }
            else
            {
                Debug.Log("Data does not exist. Creating new file!");
            }
            using FileStream stream = File.Create(SAVE_PATH);
            stream.Close();
            File.WriteAllText(SAVE_PATH, JsonConvert.SerializeObject(levelData));
        }
        catch (Exception e)
        {
            Debug.LogError("Error while saving data: " + e.Message);
        }
    }

    public static GameData LoadData()
    {
        if(!File.Exists(SAVE_PATH))
        {
            Debug.LogError("Cannot load file at " + SAVE_PATH);
            throw new FileNotFoundException($"{SAVE_PATH} does not exist");
        }
        try
        {
            GameData data = JsonConvert.DeserializeObject<GameData>(File.ReadAllText(SAVE_PATH));
            return data;
        }
        catch (Exception e)
        {
            Debug.LogError("Error while loading data: " + e.Message);
            return null;
        }
    }

    public static void DeleteSave()
    {
        if(File.Exists(SAVE_PATH))
        {
            File.Delete(SAVE_PATH);
            SaveData(new GameData());
            Debug.Log("Save file deleted");
        }
        else
        {
            Debug.LogError("Save file not found in " + SAVE_PATH);
        }
    }
}

[Serializable]
public class GameData
{
    public int highscore;
    public int coins;
}