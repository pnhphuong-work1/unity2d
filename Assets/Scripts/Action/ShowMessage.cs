using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowMessage : MonoBehaviour
{
    public GameObject _dialogPanel;
    public TextMeshProUGUI _dialogText;
    public string Message;
    
    public float textSpeed;
    
    
    private void Awake()
    {
        _dialogPanel.SetActive(true);
        _dialogText.text = "";
    }

    private void Start()
    {
        StartCoroutine(Typing());
        if (_dialogText.text == Message)
        {
            ResetMessagePanel();
        }
    }

    private void Update()
    {
        if (_dialogText.text == Message)
        {
            ResetMessagePanel();
        }
    }

    public void ResetMessagePanel()
    {
        _dialogText.text = "";
        _dialogPanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach (var letter in Message.ToCharArray())
        {
            _dialogText.text += letter;
            yield return new WaitForSeconds(1/textSpeed);
        }
    }

    
}
