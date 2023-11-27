using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    //An array of gameobjects to hold the information of the items bought in the shop
    public GameObject[] items = new GameObject[6];
    public int countedItems = 0;

    //AddItem is called in ShopScript.cs. It adds items to the 'items' array full of gameobjects. Technically it only changes the sprite on the 6 inventory slots ingame :)
    public void AddItem(int itemType)
    {
            if(itemType == 0) //Add the sword to inventory
            {
                items[countedItems].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sword");
            }
            if (itemType == 1) //Add the book to inventory
            {
                items[countedItems].GetComponent<Image>().sprite = Resources.Load<Sprite>("Book");
            }
            //Add a count to countedItems to keep track of which sprite to change
            countedItems++;
    }
}
