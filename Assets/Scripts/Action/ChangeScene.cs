using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneToLoad;
    public VectorValue initialSpawn;
    public VectorValue expectedSpawn;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            expectedSpawn.inputVector = initialSpawn.inputVector;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
