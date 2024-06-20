using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    private Animator animator;
    public float speed;
    private bool _isMoving;
    private Vector2 _charPos;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
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
        
        movementVector.Normalize();
        animator.SetBool("IsMoving", movementVector.magnitude > 0);

        GetComponent<Rigidbody2D>().velocity = movementVector * speed;
    }

    
    
}
