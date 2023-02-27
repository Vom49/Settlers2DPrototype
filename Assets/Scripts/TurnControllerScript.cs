using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnControllerScript : MonoBehaviour
{
    public static int activePlayer;

    [SerializeField] int maxPlayers;

    private void Start()
    {
        //playerNames = new string[maxPlayers];
    }
    void PassPlayerTurn()
    {
        activePlayer++;
        if (activePlayer > maxPlayers)
        {
            activePlayer = 1;
        }
    }
}
