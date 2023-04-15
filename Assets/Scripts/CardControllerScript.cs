using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardControllerScript : MonoBehaviour
{
    private int[,] playerHandData = new int[5,6]; //playerID, cardPosition
    [SerializeField] private CardDataScript[] handDisplay = new CardDataScript[6];
    private int[] deckArray = new int[25];


    [SerializeField] private GameObject ShowHandButton;
    [SerializeField] private GameObject CardPanel;
    private bool handShowing = false;

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
        EnableDisableHandButton();
    }

    private void EnableDisableHandButton()
    {
        TurnControllerScript tControl = GameObject.Find("TurnController").GetComponent<TurnControllerScript>();
        if (handShowing == false)
        {
            int turnStep = tControl.GetTurnStep();
            if (turnStep == 2 || turnStep == 3)
            {
                ShowHandButton.SetActive(true);
            }
            else
            {
                ShowHandButton.SetActive(false);
            }
        }
    }

    public void ShowHand()
    {
        handShowing = true;
        CardPanel.SetActive(true);
        ShowHandButton.SetActive(false);
    }

    public void HideHand()
    {
        handShowing = false;
        CardPanel.SetActive(false);
        ShowHandButton.SetActive(true);
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
