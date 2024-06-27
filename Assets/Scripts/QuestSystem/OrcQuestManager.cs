using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcQuestManager : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isFainted = false;
    [SerializeField] private GameObject NPC;
    private GameController gameManagerInstance = GameController.GetInstance();

    // Update is called once per frame
    void Update()
    {
        if (gameManagerInstance == null)
        {
            gameManagerInstance = GameController.GetInstance();
        }
        isFainted = gameManagerInstance.enemyFainted;
        if (isFainted)
        {
            NPC.SetActive(false);
            PlayerPrefs.SetString("KillOrc", "Success");
        }
    }
}
