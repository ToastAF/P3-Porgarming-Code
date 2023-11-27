using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WStatsCarryOver : MonoBehaviour
{
    //Stats taken from the WProperties.cs script because the sprite object which this script is on has the hitbox to hit enemies
    public float physDmg, magDmg;

    SpriteRenderer spr;
    float newAlpha = 1;

    private void Start()
    {
        spr = GetComponent<SpriteRenderer>(); //Get the sprite renderer
    }

    private void Update()
    {
        //This makes the sprite slowly fade to nothing, so it doesn't just dissapear out of nowhere
        spr.color = new Color(1, 1, 1, newAlpha);
        newAlpha -= 0.5f * Time.deltaTime;
    }
}
