using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardControllerScript : MonoBehaviour
{
    private int[,] playerHandData = new int[4,6];
    [SerializeField] private CardDataScript[] handDisplay = new CardDataScript[6];
    private int[] deckArray = new int[25];

    private void Start()
    {
        SetupDeck();
    }
    private void Update()
    {
        TurnControllerScript tControl = GameObject.Find("TurnController").GetComponent<TurnControllerScript>();
        for (int i = 0; i < 6; i++)
        {
            handDisplay[i].setID(playerHandData[tControl.GetActivePlayer(), i]);
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
        while (findingCard == true)
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
        for (int i = 0; i < 6; i++)
        {
            //find first empty slot
            if (playerHandData[tControl.GetActivePlayer(), i] == 0)
            {
                playerHandData[tControl.GetActivePlayer(), i] = foundCard;
            }
        }
    }
}
