using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


public class LevelDataHandler : DataHandler
{
    LevelManager levelManager;

    private void Awake()
    {
        this.levelManager = GetComponent<LevelManager>();
    }
    public override void Load()
    {
        string path = Application.persistentDataPath + ".player.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            LevelData data = formatter.Deserialize(stream) as LevelData;
            this.levelManager.LoadData(data);
            stream.Close();
        }
        else
        {
            Debug.LogError("File not found in " + path);
        }
    }

    public override void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + ".player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        LevelData data = new LevelData(levelManager);

        formatter.Serialize(stream, data);
        stream.Close();
    }
}
