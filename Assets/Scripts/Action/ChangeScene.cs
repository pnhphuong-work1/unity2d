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
    [SerializeField] private GameObject VisualCue;
    private bool PlayerInRange;

    private void Awake() 
    {
        PlayerInRange = false;
        VisualCue.SetActive(false);
    }
    private void Update()
    {
        if (PlayerInRange) // && !DialogueManager.GetInstance().dialogueIsPlaying
        {
            VisualCue.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E)) 
            {
                startingSpawn.inputVector = locationSpawn.inputVector;
                SceneManager.LoadScene(sceneToLoad);
            }
        }
        else 
        {
            VisualCue.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if (other.CompareTag("Player") && !other.isTrigger)
        // {
        //     startingSpawn.inputVector = locationSpawn.inputVector;
        //     SceneManager.LoadScene(sceneToLoad);
        // }
        if (other.CompareTag("Player"))
        {
            PlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInRange = false;
        }
    }
}
