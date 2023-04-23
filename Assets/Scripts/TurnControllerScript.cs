using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnControllerScript : MonoBehaviour
{
    public int activePlayer = 1;

    public int turnStep;

    private bool setupAllPlayersGoneOnce = false;

    private int whenWasRobStarted = 1;
    private int whenWasCardEffectStarted = 1;

    private int largestArmy = 0;

    [SerializeField] private GameObject nextStepButton;
    [SerializeField] private TMP_Text nextStepButtonText;

    [SerializeField] private GameObject VictoryScreen;

    [SerializeField] private TMP_Text playerNameText;

    private void Start()
    {
    }

    private void Update()
    {
        EnableDisableNextStepButton();
        UpdateActivePlayerName();
    }
    void PassPlayerTurn()
    {
        PlayerControllerScript pControl = GameObject.Find("PlayerController").GetComponent<PlayerControllerScript>();
        //check for win
        activePlayer++;
        if (activePlayer > pControl.GetMaxPlayers())
        {
            activePlayer = 1;
        }
    }

    public int GetTurnStep()
    {
        return (turnStep);
    }

    public int GetActivePlayer()
    {
        return (activePlayer);
    }


    //turnsteps
    //-1 and 0 for setup
    //1 for dice roll
    //66 for robbing (only accseed in spiecal cases
    //2 for trade
    //3 for build
    //develoment cards can be played at any time after the dice roll
    public void MoveTurnAlong()
    {
        PlayerControllerScript pControl = GameObject.Find("PlayerController").GetComponent<PlayerControllerScript>();
        //setup
        if (turnStep == 0)
        {
            if (setupAllPlayersGoneOnce == true && activePlayer == 1)
            {
                turnStep++;
            }
            else if (setupAllPlayersGoneOnce == false)
            {
                turnStep = -1;
                if (activePlayer >= pControl.GetMaxPlayers())
                {
                    setupAllPlayersGoneOnce = true;
                }
                else
                {
                    activePlayer++;
                }
            }
            else
            {
                turnStep = -1;
                activePlayer--;
            }
        }
        else if (turnStep == 3)//last turn step
        {
            //check for win
            turnStep = 1;
            LargestArmyCheck();
            LongestRoadCheck();
            CheckForWin();
            PassPlayerTurn();
        }
        else if (turnStep == 66) //robber step
        {
            turnStep = whenWasRobStarted;
            if (turnStep == 1)
            {
                turnStep++;
            }
        }
        else if (turnStep == 88) //2nd free road step
        {
            turnStep = whenWasCardEffectStarted;
        }
        else if (turnStep == 100) //2nd free resource step
        {
            turnStep = whenWasCardEffectStarted;
        }
        else if (turnStep == 50) //monopoly step
        {
            turnStep = whenWasCardEffectStarted;
        }
        else
        {
            turnStep++;
        }
        //-1, 0 for player 1, then 2 then 3 then 4, repeat
        //proper turn
        TradeControllerScript tradeControl = GameObject.Find("TradeController").GetComponent<TradeControllerScript>();
        CardControllerScript cControl = GameObject.Find("CardController").GetComponent<CardControllerScript>();
        tradeControl.UpdateTradeTargets();
        cControl.HideHand();
    }

    private void EnableDisableNextStepButton()
    {
        TurnControllerScript tControl = GameObject.Find("TurnController").GetComponent<TurnControllerScript>();
        if (tControl.GetTurnStep() == 2)
        {
            nextStepButtonText.text = "Next Step";
            nextStepButton.SetActive(true);
        }
        else if (tControl.GetTurnStep() == 3)
        {
            nextStepButtonText.text = "Next Turn";
            nextStepButton.SetActive(true);
        }
        else
        {
            nextStepButton.SetActive(false);
        }
    }

    public void GoToRobbing()
    {
        whenWasRobStarted = turnStep;
        turnStep = 66;
    }

    public void GoToFreeRoads()
    {
        whenWasCardEffectStarted = turnStep;
        turnStep = 87;
    }

    public void GoToFreeResources()
    {
        whenWasCardEffectStarted = turnStep;
        turnStep = 99;
    }

    public void GoToMonopoly()
    {
        whenWasCardEffectStarted = turnStep;
        turnStep = 50;
    }
    private void CheckForWin()
    {
        PlayerControllerScript pControl = GameObject.Find("PlayerController").GetComponent<PlayerControllerScript>();
        if (pControl.GetPlayerResource(activePlayer, Resources.VictoryPoints) >= 10)
        {
            Win(activePlayer);
        }
    }
    private void Win(int playerNum)
    {
        VictoryScreen.SetActive(true);
        VictoryScreen.GetComponent<VictoryScript>().setPlayerNameToActivePlayer();
    }

    private void UpdateActivePlayerName()
    {
        PlayerControllerScript pControl = GameObject.Find("PlayerController").GetComponent<PlayerControllerScript>();
        playerNameText.color = pControl.GetPlayerColor(activePlayer);
        playerNameText.text = pControl.GetPlayerName(activePlayer);
    }

    private void LargestArmyCheck()
    {
        PlayerControllerScript pControl = GameObject.Find("PlayerController").GetComponent<PlayerControllerScript>();
        int largetArmyPlayer = 0;
        int largetArmyCount = 0;
        for (int i = 1; i < pControl.GetMaxPlayers() + 1; i++)
        {
            if (pControl.GetPlayerResource(i, Resources.Knights) > largetArmyCount)
            {
                largetArmyCount = pControl.GetPlayerResource(i, Resources.Knights);
                largetArmyPlayer = i;
            }
        }
        if (largetArmyCount >= 3 && largetArmyPlayer != largestArmy) //does not repeat if the result has not changed
        {
            pControl.ChangeLargestArmy(largetArmyPlayer, largestArmy);
            largestArmy = largetArmyPlayer;
        }
    }
    private void LongestRoadCheck()
    {

    }
}
