using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
    public static DataPersistenceManager Instance { get; private set; }
    private List<IDataPersistence> _dataPersistences;
    private GameData _gameData;
    private FileDataHandler _dataHandler;
    public string _saveFileName;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than 1 DataPersistence exist in the scene");
        }

        Instance = this;
    }

    private List<IDataPersistence> FindAllDataPersistences()
    {
        IEnumerable<IDataPersistence> dataPersistences = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistences);
    }
    
    private void Start()
    {
        _dataHandler = new FileDataHandler(Application.persistentDataPath, _saveFileName);
        _dataPersistences = FindAllDataPersistences();
        LoadGame();
    }


    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public void NewGame()
    {
        _gameData = new GameData();
    }

    public void LoadGame()
    {
        _gameData = _dataHandler.Load();
        //Create new game if no save files found.
        if (Instance == null)
        {
            NewGame();
        }
        
        //Push loaded data to needed objs
        foreach (IDataPersistence dataPersistenceObj in _dataPersistences)
        {
            dataPersistenceObj.LoadData(_gameData);
        }
        Debug.Log("Testing if loadGame runs");
    }

    public void SaveGame()
    {
        //Pass data to other scripts to save
        foreach (IDataPersistence dataPersistenceObj in _dataPersistences)
        {
            dataPersistenceObj.SaveData(ref _gameData);
        }
        Debug.Log("Testing if saveGame runs");

        //TODO: Save data to save file using DataHandler
        _dataHandler.Save(_gameData);
    }
}
