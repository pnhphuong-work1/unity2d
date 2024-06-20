using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState
{
    Start,
    PlayerAction,
    PlayerMove,
    EnemyMove,
    Busy
}

public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHub playerHub;
    [SerializeField] BattleHub enemyHub;
    [SerializeField] BattleDialogBox dialogBox;

    public event Action<bool> OnBattleOver;
    BattleState state;
    int currentAction;
    int currentMove;

    public void StartBattle()
    {
        StartCoroutine(SetUpBattle());
    }

    public IEnumerator SetUpBattle()
    {
        playerUnit.SetUp();
        enemyUnit.SetUp();
        playerHub.SetData(playerUnit.Char);
        enemyHub.SetData(enemyUnit.Char);
        
        dialogBox.SetMoveNames(playerUnit.Char.Moves);

        yield return dialogBox.TypeDialog($"An enemy {enemyUnit.Char.Base.Name} appeared");

        yield return new WaitForSeconds(1f);

        PlayerAction();
    }

    void PlayerAction()
    {
        state = BattleState.PlayerAction;
        StartCoroutine(dialogBox.TypeDialog("Choose an action!!"));
        dialogBox.EnableActionSelector(true);
    }
    
    void PlayerMove()
    {
        state = BattleState.PlayerMove;
        dialogBox.EnableActionSelector(false);
        dialogBox.EnableDialogText(false);
        dialogBox.EnableMoveSelector(true);
    }
    
    IEnumerator PerformPlayerMove()
    {
        state = BattleState.Busy;
        var move = playerUnit.Char.Moves[currentMove];
        yield return dialogBox.TypeDialog($"{playerUnit.Char.Base.Name} use {move.Base.Name}");

        playerUnit.PlayAttackAnimation();
        yield return new WaitForSeconds(1f);

        enemyUnit.PlayHitAnimation();
        bool isFainted = enemyUnit.Char.TakeDame(move, playerUnit.Char);
        StartCoroutine(enemyHub.UpdateHp());
        if (isFainted)
        {
            yield return dialogBox.TypeDialog($"{enemyUnit.Char.Base.Name} Fainted");
            PlayerPrefs.SetInt("CurrentHP", playerUnit.Char.HP);
            PlayerPrefs.Save();
            enemyUnit.PlayFaintAnimation();
            
            yield return new WaitForSeconds(2f);
            OnBattleOver!(true);
        }
        else
        {
            StartCoroutine(EnemyMove());
        }
    }

    IEnumerator EnemyMove()
    {
        state = BattleState.EnemyMove;
        var move = enemyUnit.Char.GetRandomMove();
        yield return dialogBox.TypeDialog($"{enemyUnit.Char.Base.Name} use {move.Base.Name}");
        
        enemyUnit.PlayAttackAnimation();
        yield return new WaitForSeconds(1f);

        playerUnit.PlayHitAnimation();
        bool isFainted = playerUnit.Char.TakeDame(move, enemyUnit.Char);
        StartCoroutine(playerHub.UpdateHp());
        if (isFainted)
        {
            yield return dialogBox.TypeDialog($"{playerUnit.Char.Base.Name} Fainted");
            PlayerPrefs.SetInt("CurrentHP", playerUnit.Char.MaxHp);
            playerUnit.PlayFaintAnimation();
            
            yield return new WaitForSeconds(2f);
            OnBattleOver!(false);
        }
        else
        {
            PlayerAction();
        }
    }

    public void HandleUpdate()
    {
        if (state == BattleState.PlayerAction)
        {
            StartCoroutine(HandleActionSelection());
        }
        else if (state == BattleState.PlayerMove)
        {
            HandleMoveSelection();
        }
    }

    IEnumerator HandleActionSelection()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentAction < 1)
                ++currentAction;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentAction > 0)
                --currentAction;
        }
        
        dialogBox.UpdateActionSelection(currentAction);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentAction == 0)
            {
                //Fight
                PlayerMove();
            }
            else if (currentAction == 1)
            {
                //run
                yield return dialogBox.TypeDialog($"{playerUnit.Char.Base.Name} Run!!");
            
                yield return new WaitForSeconds(2f);
                OnBattleOver!(false);
            }
        }
    }
    
    private void HandleMoveSelection()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentMove < playerUnit.Char.Moves.Count - 1)
                ++currentMove;
            
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentMove > 0)
                --currentMove;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentMove < playerUnit.Char.Moves.Count - 2)
                currentMove += 2;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentMove > 1)
                currentMove -= 2;
        }
        dialogBox.UpdateMoveSelection(currentMove, playerUnit.Char.Moves[currentMove]);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            dialogBox.EnableMoveSelector(false);
            dialogBox.EnableDialogText(true);
            StartCoroutine(PerformPlayerMove());
        }
    }
}
