using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    PlayerMove playerScr;
    InventoryManager inventory;
    public GameObject shop;
    bool shopClosed;
    
    //The class 'Item' is used to easily create new items for the player to buy. These items have stats and a price
    class Item
    {
        public float atkDmg, magDmg;
        public int buyPrice;

        public Item(float atk, float mag, int price)
        {
            atkDmg = atk;
            magDmg = mag;
            buyPrice = price;
        }
    }
    //Two items are instantiated
    Item sword = new Item(10, 0, 300);
    Item magicBook = new Item(0, 10, 300);
    

    void Start()
    {
        //Yoink the other scripts which are also on the player
        playerScr = GetComponent<PlayerMove>();
        inventory = GetComponent<InventoryManager>();
        shop.SetActive(false); //Close shop on start
        shopClosed = true;
    }

    void Update()
    {
        //Clicking P opens or closes the shop menu
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (shopClosed == true)
            {
                shop.SetActive(true);
                shopClosed = false;
            }
            else
            {
                shop.SetActive(false);
                shopClosed = true;
            }
        }
    }

    public void BuySword()
    {
        //If there is less than 6 items in the inventory and the player has enough gold, an item is added to the inventory and gold is taken for the purchase.
        if(inventory.countedItems < 6)
        {
            if(playerScr.gold >= sword.buyPrice)
            {
                playerScr.attackDamage += sword.atkDmg; //Also the items stats are transfered to the player
                playerScr.gold -= sword.buyPrice;
                inventory.AddItem(0);
            }
            else
            {
                print("Not enough gold!");
            }
        }
        else
        {
            print("Not enough space!");
        }
    }

    //This does the same as the method above. Both are being called from UI buttons in the canvas while playing the game, hence the public prefix
    public void BuyMagicBook()
    {
        if(inventory.countedItems < 6)
        {
            if (playerScr.gold >= magicBook.buyPrice)
            {
                playerScr.abilityPower += magicBook.magDmg;
                playerScr.gold -= magicBook.buyPrice;
                inventory.AddItem(1);
            }
            else
            {
                print("Not enough gold!");
            }
        }
        else
        {
            print("Not enough space!");
        }
    }
}
