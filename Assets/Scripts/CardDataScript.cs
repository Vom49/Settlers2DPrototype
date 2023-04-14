using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDataScript : MonoBehaviour
{
    //card ID
    //Assign Sprite based on card ID
    //on click, have card perform action based on card ID

    public int cardID = 0;
    public Image buttonImage;
    public Sprite blankCard;

    private int[] deckArray = new int[25];
    private void Update()
    {
        
        switch (cardID)
        {
            default:
                buttonImage.sprite = blankCard;
                break;
        }
    }

    /*
     * 0 - nothing
     * 1 - victory point
     * 2 - knight
     * 3 - build 2 free roads
     * 4 - 2 resources
     * 5 - get one type of resource form other players
     */
    public void onClick()
    {
        switch (cardID)
        {
            default:
                break;
        }
    }

    private void SetupDeck()
    {
        for (int k = 0; k < 14; k++)
        {
            deckArray[k] = 2;
        }
        for (int vp = 14; vp < 19; vp++)
        {
            deckArray[vp] = 1;
        }
        for (int rr = 19; rr < 21; rr++)
        {
            deckArray[rr] = 3;
        }
        for (int yop = 21; yop < 23; yop++)
        {
            deckArray[yop] = 4;
        }
        for (int m = 23; m < 25; m++)
        {
            deckArray[m] = 5;
        }
    }
    public void AddCardToHand()
    {
        TurnControllerScript tControl = GameObject.Find("TurnController").GetComponent<TurnControllerScript>();
        bool findingCard = true;
        int foundCard = 0;
        while(findingCard == true)
        {
            int target = Random.Range(0, 25);
            if (deckArray[target] != 0)
            {
                foundCard = deckArray[target];
                deckArray[target] = 0;
                findingCard = false;
            }
        }
        
        //for loop
        for (int i = 0)
        //find first empty slot
        //set that cards ID to the ID of card from deck
        //remove that card from the deck
    }
    public void setID(int ID)
    {
        cardID = ID;
    }
    
}
