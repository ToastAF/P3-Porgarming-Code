using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHover : MonoBehaviour
{
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
