using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHover : MonoBehaviour
{
    //This scripts has two function that either set the rangeCircle gameobject to be active or activen't. These functions are called on event triggers on UI elements
    public GameObject rangeCircle;
    private void Start()
    {
        rangeCircle.SetActive(false);
    }
    public void SetTrue()
    {
        rangeCircle.SetActive(true);
    }
    public void SetFalse()
    {
        rangeCircle.SetActive(false);
    }
}
