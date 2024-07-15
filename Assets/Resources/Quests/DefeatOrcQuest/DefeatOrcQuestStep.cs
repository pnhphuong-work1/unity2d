using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatOrcQuestStep : QuestStep
{
    private bool orcDefeated = false;
    private string enemyName = "RavineOrc";

    private void OnEnable()
    {
        GameEventsManager.instance.miscEvents.onEnemyDefeated += OrcDefeated;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.miscEvents.onEnemyDefeated -= OrcDefeated;
    }

    private void OrcDefeated(string name)
    {
        if (!orcDefeated && name == enemyName) orcDefeated = true;
        if (orcDefeated) FinishQuestStep();
    }

    protected override void SetQuestStepState(string state)
    {
        throw new System.NotImplementedException();
    }
}
