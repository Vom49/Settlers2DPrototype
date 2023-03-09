using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnControllerScript : MonoBehaviour
{
    public int activePlayer = 1;

    public int turnStep;

    public int _maxPlayers;

    private bool setupAllPlayersGoneOnce = false;

    private void Start()
    {
        //playerNames = new string[maxPlayers];
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
            PassPlayerTurn();
        }
        else
        {
            turnStep++;
        }
        //-1, 0 for player 1, then 2 then 3 then 4, repeat
        //proper turn

        //incremend through steps
    }
}
