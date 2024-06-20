using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerMovementController : MonoBehaviour
{
    public float speed;
    private bool _isMoving;
    private Vector2 _movement;
    private Animator _animator;
    
    public event Action OnEncounteredBattle;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }


    public void Update()
    {
        if (!_isMoving)
        {
            _movement.x = Input.GetAxisRaw("Horizontal");
            _movement.y = Input.GetAxisRaw("Vertical");
            if (_movement != Vector2.zero)
            {
                var targetPos = transform.position;
                targetPos.x += _movement.x;
                targetPos.y += _movement.y;
                _animator.SetFloat("X", _movement.x);
                _animator.SetFloat("Y", _movement.y);
                
                //StartCoroutine(Move(targetPos));
            }
        }

        //EncounterBattle();
        _animator.SetBool("IsMoving", _isMoving);
    }
    
    // IEnumerator Move(Vector3 targetPos)
    // {
    //     _isMoving = true;
    //     while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
    //     {
    //         //transform.position = Vector3.MoveTowards(transform.position, targetPos, speed*Time.fixedDeltaTime);
    //         GetComponent<Rigidbody2D>().velocity = targetPos * speed;
    //         yield return null;
    //     }
    //     //transform.position = targetPos;
    //     GetComponent<Rigidbody2D>().velocity = targetPos * speed;
    //     _isMoving = false;
    // }
    //Call this method when the player encounters a battle
    // private void EncounterBattle()
    // {
    //     if (Random.Range(0, 10000) < 5)
    //     {
    //         OnEncounteredBattle();
    //     }
    // }
}
