using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] CharBase _base;
    [SerializeField] int level;
    [SerializeField] private bool isPlayerUnit;

    public Character Char { get; set; } 
    
    private Image _image;
    private Vector3 _originalPos;
    private Color _originalColor;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _originalPos = _image.transform.localPosition;
        _originalColor = _image.color;
        // _originalPos = _image.rectTransform.localPosition;
    }

    public void SetUp()
    {
        Char = new Character(_base, level);
        if (isPlayerUnit)
            _image.sprite = Char.Base.FrontSprite;
        else
            _image.sprite = Char.Base.FrontSprite;
        
        PlayBattleEnterAnimation();
    }
    
    public void PlayBattleEnterAnimation()
    {
        if (isPlayerUnit)
            _image.transform.localPosition = new Vector3(-900f, _originalPos.y);
        else
            _image.transform.localPosition = new Vector3(900f, _originalPos.y);
        
        _image.transform.DOLocalMoveX(_originalPos.x, 1f);
    }
    
    public void PlayAttackAnimation()
    {
        var sequence = DOTween.Sequence();
        if (isPlayerUnit)
            sequence.Append(_image.transform.DOLocalMoveX(_originalPos.x + 50f, 0.25f));
        else
            sequence.Append(_image.transform.DOLocalMoveX(_originalPos.x - 50f, 0.25f));
        
        
        sequence.Append(_image.transform.DOLocalMoveX(_originalPos.x, 0.25f));
    }
    
    public void PlayHitAnimation()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(_image.DOColor(Color.gray, 0.1f));
        sequence.Append(_image.DOColor(_originalColor, 0.1f));
        //_image.transform.DOShakePosition(0.5f, 10, 50, 90, false, true);
    }
    
    public void PlayFaintAnimation()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(_image.transform.DOLocalMoveY(_originalPos.y - 150f, 0.5f));
        sequence.Join(_image.DOFade(0f, 0.5f));
    }
}
