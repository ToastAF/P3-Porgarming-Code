using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject[] items = new GameObject[6];
    public int countedItems = 0;

    void Start()
    {
        
    }

    void Update()
    {
        if(countedItems < 6)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                items[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("EmptyItem");
                countedItems++;
                print("Did it, boss! Item count: " + countedItems);
            }
        }
    }
}
