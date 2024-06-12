using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleHub : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    //[SerializeField] TextMeshProUGUI levelText;
    [SerializeField] HPBar hpBar;
    Character _levelChar;

    public void SetData(Character levelChar)
    {
        _levelChar = levelChar;
        nameText.text = levelChar.Base.Name;
        //levelText.text = "Lvl " + levelChar.Level;
        hpBar.SetHp((float) levelChar.HP / levelChar.MaxHp);
    }

    public IEnumerator UpdateHp()
    {
       yield return hpBar.SetHpSmooth((float) _levelChar.HP / _levelChar.MaxHp);
    }
}
