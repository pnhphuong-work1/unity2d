using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;


public class FileDataHandler
{
    private string dirDataPath = "";
    private string dataFileName = "";

    public FileDataHandler(string dataPath, string fileName)
    {
        this.dirDataPath = dataPath;
        this.dataFileName = fileName;
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(dirDataPath, dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                //Deserialize data
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error at FileDataHandler.Load()" + e);
            }
        }
        
        return new GameData();
    }

    public void Save(GameData gameData)
    {
        string fullPath = Path.Combine(dirDataPath, dataFileName);
        try
        {
            //create save file Directory if it not exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            
            //serialize GameData to JSON
            string dataToStore = JsonUtility.ToJson(gameData, true);
            
            //Create a file stream
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                //Create a writer to that stream and write data
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error at FileDataHandler.Save() "+ e);
        }
    }
}
