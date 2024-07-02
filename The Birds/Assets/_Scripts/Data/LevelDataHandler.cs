using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


public class LevelDataHandler : DataHandler
{
    LevelManager levelManager;

    public static LevelDataHandler instance;

    private void Awake()
    {
        instance = this;
        this.levelManager = GetComponent<LevelManager>();

        Load();
    }
    public override void Load()
    {
        string path = Application.persistentDataPath + "/level.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            stream.Position = 0;
            LevelData data = formatter.Deserialize(stream) as LevelData;
            this.levelManager.LoadData(data);
            stream.Close();
            //Debug.Log("Loaded");
        }
        else
        {
            Debug.LogError("File not found in " + path);
        }
    }

    public override void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/level.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        LevelData data = new LevelData(levelManager);

        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("Saved");
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}
