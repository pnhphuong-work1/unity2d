using System;
using System.Collections;
using System.Collections.Generic;
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
    
    GameState state;

    private void Start()
    {
        Player.GetComponent<PlayerMovementController>().OnEncounteredBattle += StartBattle;
        battleSystem.OnBattleOver += EndBattle;
    }
    
    void StartBattle()
    {
        state = GameState.Battle;
        battleSystem.gameObject.SetActive(true);
        worldCamera.gameObject.SetActive(false);
        battleSystem.StartBattle();
    }
    
    void EndBattle(bool playerHasWon)
    {
        state = GameState.FreeRoam;
        battleSystem.gameObject.SetActive(false);
        worldCamera.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (state == GameState.FreeRoam)
        {
            Player.GetComponent<PlayerMovementController>().Update();
        }
        else if (state == GameState.Battle)
        {
            battleSystem.HandleUpdate();
        }
    }
}
