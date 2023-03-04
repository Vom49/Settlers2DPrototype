using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnControllerScript : MonoBehaviour
{
    public int activePlayer = 1;

    public int turnStep;

    public int _maxPlayers;

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

    private void MoveTurnAlong()
    {
        //setup
        //-1, 0 for player 1, then 2 then 3 then 4, repeat
        //proper turn

        //incremend through steps
    }
}
