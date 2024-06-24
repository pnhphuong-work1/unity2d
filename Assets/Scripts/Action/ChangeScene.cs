using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneToLoad;
    public VectorValue locationSpawn;
    public VectorValue startingSpawn;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            startingSpawn.inputVector = locationSpawn.inputVector;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
