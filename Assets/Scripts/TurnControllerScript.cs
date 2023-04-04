using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnControllerScript : MonoBehaviour
{
    public int activePlayer = 1;

    public int turnStep;

    public int _maxPlayers;

    private bool setupAllPlayersGoneOnce = false;

    private int whenWasRobStarted = 1;

    [SerializeField] private GameObject nextStepButton;

    [SerializeField] private GameObject VictoryScreen;

    private void Start()
    {
        //playerNames = new string[maxPlayers];
    }

    private void Update()
    {
        EnableDisableNextStepButton();
    }
    void PassPlayerTurn()
    {
        //check for win
        activePlayer++;
        if (activePlayer > _maxPlayers)
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
                if (activePlayer >= _maxPlayers)
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
        else if (turnStep == 10)//last turn step
        {
            //check for win
            turnStep = 1;
            CheckForWin();
            PassPlayerTurn();
        }
        else if (turnStep == 66) //robber step
        {
            turnStep = whenWasRobStarted;
        }
        else
        {
            turnStep++;
        }
        //-1, 0 for player 1, then 2 then 3 then 4, repeat
        //proper turn

        //incremend through steps
    }

    private void EnableDisableNextStepButton()
    {

    }

    public void GoToRobbing()
    {
        whenWasRobStarted = turnStep;
        turnStep = 66;
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
}
