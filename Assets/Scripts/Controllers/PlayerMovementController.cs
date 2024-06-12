using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerMovementController : MonoBehaviour
{
    public float speed;

    private Animator animator;
    public event Action OnEncounteredBattle;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }


    public void Update()
    {
        Vector2 dir = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            dir.y = 1;
            animator.SetInteger("Direction", 0);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            dir.x = -1;
            animator.SetInteger("Direction", 1);
        }

        if (Input.GetKey(KeyCode.S))
        {
            dir.y = -1;
            animator.SetInteger("Direction", 2);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            dir.x = 1;
            animator.SetInteger("Direction", 3);
        }

        dir.Normalize();
        animator.SetBool("IsMoving", dir.magnitude > 0);

        GetComponent<Rigidbody2D>().velocity = speed * dir;
    }
    
    //Call this method when the player encounters a battle
    private void EncounteredBattle()
    {
        if (Random.Range(0, 10000) < 5)
        {
            OnEncounteredBattle();
        }
    }
}
