using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleDialogBox : MonoBehaviour
{
    [SerializeField] int letterPerSecond;
    [SerializeField] Color highlightColor;
    
    [SerializeField] TextMeshProUGUI dialogText;
    [SerializeField] GameObject acionSelector;
    [SerializeField] GameObject answerSelector;
    [SerializeField] GameObject moveSelector;
    [SerializeField] GameObject moveDetails;

    [SerializeField] List<TextMeshProUGUI> actionText;
    [SerializeField] List<TextMeshProUGUI> moveText;
    [SerializeField] List<TextMeshProUGUI> answerText;
    [SerializeField] TextMeshProUGUI ppText;

    public void SetDialog(string dialog)
    {
        dialogText.text = dialog;
    }

    public IEnumerator TypeDialog(string dialog)
    {
        dialogText.text = "";
        foreach (var letter in dialog.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / letterPerSecond);
        }
    }

    public void EnableDialogText(bool enable)
    {
        dialogText.enabled = enable;
    }
    
    public void EnableActionSelector(bool enable)
    {
        acionSelector.SetActive(enable);
    }
    public void EnableAnswerSelector(bool enable)
    {
        answerSelector.SetActive(enable);
    }
    
    public void EnableMoveSelector(bool enable)
    {
        moveSelector.SetActive(enable);
        moveDetails.SetActive(enable);
    }

    public void UpdateActionSelection(int selectedAction)
    {
        for (int i = 0; i < actionText.Count; ++i)
        {
            if (i == selectedAction)
                actionText[i].color = highlightColor;
            else
                actionText[i].color = Color.black;
        }
    }

    public void UpdateMoveSelection(int selectedMove, Move move)
    {
        for (int i = 0; i < moveText.Count; ++i)
        {
            if (i == selectedMove)
                moveText[i].color = highlightColor;
            else
                moveText[i].color = Color.black;
        }

        ppText.text = $"PP {move.PP}/{move.Base.PP}";
    }
    public void UpdateAnswerSelection(int selectedMove)
        {
            for (int i = 0; i < answerText.Count; ++i)
            {
                if (i == selectedMove)
                    answerText[i].color = highlightColor;
                else
                    answerText[i].color = Color.black;
            }
        }

    public void SetMoveNames(List<Move> moves)
    {
        for (int i = 0; i < moveText.Count; ++i)
        {
            if (i < moves.Count)
                moveText[i].text = moves[i].Base.Name;
            else
                moveText[i].text = "-";
        }
    }
    public void SetAnswers(List<string> answer)
    {
        for (int i = 0; i < answerText.Count; ++i)
        {
            if (i < answer.Count)
                answerText[i].text = answer[i];
            else
                answerText[i].text = "-";
        }
    }
    
}
