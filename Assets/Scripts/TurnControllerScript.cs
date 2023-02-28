using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnControllerScript : MonoBehaviour
{
    public static int activePlayer = 1;

    private int turnStep = 0;

    [SerializeField] int _maxPlayers;

    private void Start()
    {
        //playerNames = new string[maxPlayers];
    }
    void PassPlayerTurn()
    {
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
}
