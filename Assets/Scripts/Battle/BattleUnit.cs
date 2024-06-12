using System;
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
    
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void SetUp()
    {
        Char = new Character(_base, level);
        if (isPlayerUnit)
            _image.sprite = Char.Base.FrontSprite;
        else
            _image.sprite = Char.Base.FrontSprite;
    }
    
    public void PlayBattleEnterAnimation()
    {
        
    }
}
