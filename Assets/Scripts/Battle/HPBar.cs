using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    [SerializeField] GameObject health;

    public void SetHp(float hpNormalized)
    {
        health.transform.localScale = new Vector3(hpNormalized, 1f);
    }
    
    public IEnumerator SetHpSmooth(float newHpNormalized)
    {
        float originalHp = health.transform.localScale.x;
        float changeAmount = originalHp - newHpNormalized;

        while (originalHp - newHpNormalized > Mathf.Epsilon)
        {
            originalHp -= changeAmount * Time.deltaTime;
            health.transform.localScale = new Vector3(originalHp, 1f);
            yield return null;
        }
        health.transform.localScale = new Vector3(newHpNormalized, 1f);
    }
}
