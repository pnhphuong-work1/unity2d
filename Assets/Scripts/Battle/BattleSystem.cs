using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ink.Parsed;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public enum BattleState
{
    Answer,
    Answered,
    PlayerAction,
    PlayerMove,
    EnemyMove,
    EnemyAsk,
    Busy
}

public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHub playerHub;
    [SerializeField] BattleHub enemyHub;
    [SerializeField] BattleDialogBox dialogBox;
    [SerializeField] private string enemyName;

    public event Action<bool> OnBattleOver;
    BattleState state;
    int currentAction;
    int currentMove;
    int currentAnswer;

    private string question;
    private List<string> answers = new();
    private string rightAnswer;
    private string choseAnswer;

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
        dialogBox.EnableMoveSelector(false);
        dialogBox.EnableAnswerSelector(false);
    }
    void AnswerAction()
    {
        state = BattleState.Answer;
        StartCoroutine(dialogBox.TypeDialog("Choose an answer!!"));
        //dialogBox.TypeDialog("Choose an answer!!");
        dialogBox.EnableActionSelector(false);
        dialogBox.EnableDialogText(false);
        dialogBox.EnableMoveSelector(false);
        dialogBox.EnableAnswerSelector(true);
    }
    
    void PlayerMove()
    {
        state = BattleState.PlayerMove;
        dialogBox.EnableActionSelector(false);
        dialogBox.EnableDialogText(false);
        dialogBox.EnableAnswerSelector(false);
        dialogBox.EnableMoveSelector(true);
    }

    IEnumerator PerformAnswerQuestion()
    {
        state = BattleState.Busy;
        var answer = answers[currentAnswer];
        choseAnswer = answer;
        if (choseAnswer == rightAnswer)
        {
            yield return dialogBox.TypeDialog($"{answer} is correct! You dodged the attack!");
        }
        else
        {
            yield return dialogBox.TypeDialog($"{answer} is wrong! You cannot dodge the enemy attack :(");
        }
        yield return new WaitForSeconds(1f);
        state = BattleState.Answered;
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
            GameEventsManager.instance.miscEvents.EnemyDefeated(enemyName);
            yield return dialogBox.TypeDialog($"{enemyUnit.Char.Base.Name} Fainted");
            //Send enemy defeated event
            
            PlayerPrefs.SetInt("CurrentHP", playerUnit.Char.HP);
            PlayerPrefs.Save();
            enemyUnit.PlayFaintAnimation();
            
            yield return new WaitForSeconds(2f);
            OnBattleOver!(true);
        }
        else
        {
            StartCoroutine(EnemyAskQuestion());
        }
    }

    IEnumerator EnemyAskQuestion()
    {
        state = BattleState.EnemyAsk;
        //Asking question
        int a = Random.Range(0, 30);
        int b = Random.Range(0, 30);
        int c;
        int operatorOps = Random.Range(0, 1);
        string question;
        switch (operatorOps)
        {
            //+
            case 0:
                c = a + b;
                rightAnswer = c.ToString();
                question = $"{a} + {b}";
                break;
            //-
            case 1:
                c = a - b;
                rightAnswer = c.ToString();
                question = $"{a} - {b}";
                break;
            default:
                c = a + b;
                rightAnswer = c.ToString();
                question = $"{a} + {b}";
                break;
        }
        //Generate answers
        
            int ra = int.Parse(rightAnswer);
            int lowerRange = ra - 5;
            int upperRange = ra + 5;
            int randomAns = Random.Range(lowerRange, upperRange);
            HashSet<string> answerSet = new();
            do
            {
                if (!rightAnswer.Equals(randomAns.ToString()))
                {
                    answerSet.Add(randomAns.ToString());
                }
                randomAns = Random.Range(lowerRange, upperRange);
            } while (answerSet.Count < 4);

            int answerPos = Random.Range(0, 3);
            answers = answerSet.ToList();
            answers[answerPos] = rightAnswer;
        

        yield return dialogBox.TypeDialog($"{enemyUnit.Char.Base.Name} ask you {question} = ?");
        yield return new WaitForSeconds(2f);
        
        AnswerAction();
        //state = BattleState.Answered;

        
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
        else if (state == BattleState.Answer)
        {
            dialogBox.SetAnswers(answers);
            HandleAnswerSelection();
        }
        if (choseAnswer != rightAnswer && state == BattleState.Answered)
        {
            StartCoroutine(EnemyMove());
        }
        else if (choseAnswer == rightAnswer && state == BattleState.Answered)
        {
            PlayerAction();
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
    private void HandleAnswerSelection()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentAnswer < answers.Count - 1)
                ++currentAnswer;
            
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentAnswer > 0)
                --currentAnswer;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentAnswer < answers.Count - 2)
                currentAnswer += 2;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentAnswer > 1)
                currentAnswer -= 2;
        }
        dialogBox.UpdateAnswerSelection(currentAnswer);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            dialogBox.EnableAnswerSelector(false);
            dialogBox.EnableDialogText(true);
            StartCoroutine(PerformAnswerQuestion());
        }
    }
    
}
