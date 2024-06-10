using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] CharBase _base;
    [SerializeField] int level;
    [SerializeField] private bool isPlayerUnit;

    public Character Char { get; set; } 
    
    public void SetUp()
    {
        Char = new Character(_base, level);
        if (isPlayerUnit)
            GetComponent<Image>().sprite = Char.Base.FrontSprite;
        else
            GetComponent<Image>().sprite = Char.Base.FrontSprite;
    }
}
