using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class MovementController : MonoBehaviour
{
    private Animator animator;
    public float speed;
    private bool _isMoving;
    private Vector2 _charPos;
    public VectorValue _startingPos;

    private bool dialogueIsPlaying = false;
    private bool isMoveable = true;
    
    public event Action OnEncounteredBattle;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        transform.position = _startingPos.inputVector;
        _startingPos.inputVector = Vector2.zero;
        
    }

    private void Update()
    {
        if (DialogueManager.GetInstance() != null)
        {
            isMoveable = !DialogueManager.GetInstance().dialogueIsPlaying;
        }
        if (GameController.GetInstance() != null)
        {
            isMoveable = GameController.GetInstance().isMoveable;
        }
        if (!isMoveable)
        {
            return;
        }
        
        Vector2 movementVector = Vector2.zero;
        if (Input.GetKey(KeyCode.A))
        {
            movementVector.x = -1;
            animator.SetFloat("X", -1f);
            animator.SetFloat("Y", 0f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movementVector.x = 1;
            animator.SetFloat("X", 1f);
            animator.SetFloat("Y", 0f);
        }

        if (Input.GetKey(KeyCode.W))
        {
            movementVector.y = 1;
            animator.SetFloat("Y", 1f);
            animator.SetFloat("X", 0f);
            
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movementVector.y = -1;
            animator.SetFloat("Y", -1f);
            animator.SetFloat("X", 0f);
        }
        
        EncounterBattle();
        
        movementVector.Normalize();
        animator.SetBool("IsMoving", movementVector.magnitude > 0);

        GetComponent<Rigidbody2D>().velocity = movementVector * speed;
    }

    private void EncounterBattle()
    {
        if (Random.Range(0, 10000) < 5)
        {
            OnEncounteredBattle();
        }
    }
    
    
}
