using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    PlayerMove playerScr;
    public GameObject shop;
    bool shopClosed;

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
    Item sword = new Item(10, 0, 300);
    Item magicBook = new Item(0, 10, 300);
    

    void Start()
    {
        playerScr = gameObject.GetComponent<PlayerMove>();
        shop.SetActive(false);
        shopClosed = true;
    }

    void Update()
    {
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
        if(playerScr.gold >= sword.buyPrice)
        {
            playerScr.attackDamage += sword.atkDmg;
            playerScr.gold -= sword.buyPrice;
        }
        else
        {
            print("Not enough gold!");
        }
    }

    public void BuyMagicBook()
    {
        if (playerScr.gold >= magicBook.buyPrice)
        {
            playerScr.abilityPower += magicBook.magDmg;
            playerScr.gold -= magicBook.buyPrice;
        }
        else
        {
            print("Not enough gold!");
        }
    }
}
