using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerMovementController : MonoBehaviour, IDataPersistence
{
    public float speed;

    private Animator animator;
    public event Action OnEncounteredBattle;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }


    public void HandleUpdate()
    {
        Vector2 dir = Vector2.zero;
        if (Input.GetKey(KeyCode.A))
        {
            dir.x = -1;
            animator.SetInteger("Direction", 3);
            EncounteredBattle();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            dir.x = 1;
            animator.SetInteger("Direction", 2);
        }

        if (Input.GetKey(KeyCode.W))
        {
            dir.y = 1;
            animator.SetInteger("Direction", 1);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            dir.y = -1;
            animator.SetInteger("Direction", 0);
        }

        dir.Normalize();
        animator.SetBool("IsMoving", dir.magnitude > 0);

        GetComponent<Rigidbody2D>().velocity = speed * dir;
    }

    public void LoadData(GameData data)
    {
        this.transform.position = data.playerPos;
    }

    public void SaveData(ref GameData data)
    {
        data.playerPos = this.transform.position;
    }
    
    //Call this method when the player encounters a battle
    private void EncounteredBattle()
    {
        if (Random.Range(0, 100) < 10)
        {
            animator.SetBool("IsMoving", false);
            OnEncounteredBattle();
        }
    }
}
