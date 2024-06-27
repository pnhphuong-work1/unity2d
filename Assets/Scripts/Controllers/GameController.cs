using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum GameState
{
    FreeRoam,
    Battle
}

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] BattleSystem battleSystem;
    [SerializeField] Camera worldCamera;
    //[SerializeField] Camera battleCamera;
    public bool isMoveable { get; private set; }
    public bool enemyFainted { get; set; } = false;
    public GameState state;
    private static GameController instance;
    
    private void Awake() 
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;
    }

    public static GameController GetInstance()
    {
        return instance;
    }
    private void Start()
    {
        Player.GetComponent<MovementController>().OnEncounteredBattle += StartBattle;
        battleSystem.OnBattleOver += EndBattle;
    }
    
    void StartBattle()
    {
        state = GameState.Battle;
        battleSystem.gameObject.SetActive(true);
        worldCamera.gameObject.SetActive(false);
        //battleCamera.gameObject.SetActive(true);
        battleSystem.StartBattle();
    }
    
    void EndBattle(bool playerHasWon)
    {
        state = GameState.FreeRoam;
        battleSystem.gameObject.SetActive(false);
        //battleCamera.gameObject.SetActive(false);
        worldCamera.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (state == GameState.FreeRoam)
        {
            isMoveable = true;
        }
        else if (state == GameState.Battle)
        {
            isMoveable = false;
            battleSystem.HandleUpdate();
        }
    }
}
