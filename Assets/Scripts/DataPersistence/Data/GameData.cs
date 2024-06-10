using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameData
{
    public Vector2 playerPos;
    public string currScene;
    public GameData()
    {
        playerPos = Vector2.zero;
        currScene = "Scene 0 - Start";
    }
}
