using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

public class SaveSystem
{
    public readonly static string SAVE_PATH = Application.persistentDataPath + "/save" + ".json";
    private const string KEY = "Jt/qF7h5tPhKPcJhPYP38xxpo7/npwZ6F0xdopl7o9E=";
    private const string IV = "cagcP6zkzWBCdo+eJgQFOg==";
    private static bool encrypted = true;

    public static void CreateDataFile(GameData levelData)
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
            if(encrypted)
            {
                WriteEncryptedData(levelData, stream);
            }
            else
            {
                stream.Close();
                File.WriteAllText(SAVE_PATH, JsonConvert.SerializeObject(levelData));
            }
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
            if(encrypted)
            {
                WriteEncryptedData(levelData, stream);
            }
            else
            {
                stream.Close();
                File.WriteAllText(SAVE_PATH, JsonConvert.SerializeObject(levelData));
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error while saving data: " + e.Message);
        }
    }

    private static void WriteEncryptedData(GameData levelData, FileStream stream)
    {
        using Aes aesProvider = Aes.Create();

        aesProvider.Key = Convert.FromBase64String(KEY);
        aesProvider.IV = Convert.FromBase64String(IV);
        
        using ICryptoTransform cryptoTransform = aesProvider.CreateEncryptor();
        using CryptoStream cryptoStream = new CryptoStream(stream, cryptoTransform, CryptoStreamMode.Write);

        cryptoStream.Write(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(levelData)));
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
            GameData data;
            if(encrypted)
            {
                data = ReadEncryptedData();
            } 
            else
            {
                data = JsonConvert.DeserializeObject<GameData>(File.ReadAllText(SAVE_PATH));;   
            }
            return data;
        }
        catch (Exception e)
        {
            Debug.LogError("Error while loading data: " + e.Message);
            return null;
        }
    }

    private static GameData ReadEncryptedData()
    {
        byte[] fileBytes = File.ReadAllBytes(SAVE_PATH);
        using Aes aesProvider = Aes.Create();

        aesProvider.Key = Convert.FromBase64String(KEY);
        aesProvider.IV = Convert.FromBase64String(IV);

        using ICryptoTransform cryptoTransform = aesProvider.CreateDecryptor(aesProvider.Key, aesProvider.IV);
        using MemoryStream decryptionStream = new MemoryStream(fileBytes);
        using CryptoStream cryptoStream = new CryptoStream(decryptionStream, cryptoTransform, CryptoStreamMode.Read);
        using StreamReader reader = new StreamReader(cryptoStream);

        string result = reader.ReadToEnd();

        return JsonConvert.DeserializeObject<GameData>(result);
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