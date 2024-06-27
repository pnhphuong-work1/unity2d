using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OrcKilledManager : MonoBehaviour
{
    private bool orcKilled = false;
    void Start()
    {
        var OrcQuest = PlayerPrefs.GetString("KillOrc");
        if (OrcQuest == "Success")
        {
            orcKilled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
