using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    [SerializeField] private int camSpeed;
    [SerializeField] private float y0;
    [SerializeField] private float y1;
    private Vector3 position;
    private bool isGoingUp = true;
    private void Start()
    {
        position = transform.position;
        var vector3 = position;
        vector3.y = y0;
        transform.position = vector3;
    }

    void Update()
    {
        if (transform.position.y > y1)
        {
            isGoingUp = false;
        } else if(transform.position.y < y0)
        {
            isGoingUp = true;
        }
        Vector2 movementVector = Vector2.zero;
        if (isGoingUp)
        {
            movementVector.y = 1;
        }
        else
        {
            movementVector.y = -1;
        }
        
        movementVector.Normalize();
        GetComponent<Rigidbody2D>().velocity = movementVector * camSpeed;
    }
}
