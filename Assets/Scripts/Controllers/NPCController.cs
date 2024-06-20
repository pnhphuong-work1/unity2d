using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class NPCController : MonoBehaviour
{
    public GameObject _dialogPanel;
    public Text _dialogText;
    public Text _NPCText;
    public string _npcName;
    public GameObject _continueButton;
    public GameObject[] _choicesButton;
    public string[] _choices;
    public BaseDialogue[] ChoicesDialogues;
    public BaseDialogue openDialogue;
    public float textSpeed;
    

    private int _index;
    public bool _playerIsClose;

    // Update is called once per frame
    private void Start()
    {
        _NPCText.text = _npcName;
        _dialogPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _playerIsClose)
        {
            if (_dialogPanel.activeInHierarchy)
            {
                ResetDialog();
            }
            else
            {
                _dialogPanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }
        
        if (_dialogText.text == openDialogue.dialogueList[_index])
        {
            _continueButton.SetActive(true);
        }
    }

    public void ResetDialog()
    {
        _dialogText.text = "";
        _index = 0;
        _dialogPanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach (var letter in openDialogue.dialogueList[_index].ToCharArray())
        {
            _dialogText.text += letter;
            yield return new WaitForSeconds(1/textSpeed);
        }
    }

    public void NextLine()
    {
        _continueButton.SetActive(false);
        
        if (_index < openDialogue.dialogueList.Length - 1)
        {
            _index++;
            _dialogText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            ResetDialog();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerIsClose = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerIsClose = false;
            ResetDialog();
        }
    }
    
}
