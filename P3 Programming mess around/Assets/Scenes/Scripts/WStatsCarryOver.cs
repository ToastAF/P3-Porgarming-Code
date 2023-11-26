using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WStatsCarryOver : MonoBehaviour
{
    //Stats taken from the WProperties.cs script
    public float physDmg, magDmg;

    SpriteRenderer spr;
    float newAlpha = 1;

    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        spr.color = new Color(1, 1, 1, newAlpha);
        newAlpha -= 0.5f * Time.deltaTime;
    }
}
