using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VictoryScript : MonoBehaviour
{
    [SerializeField] private TMP_Text playerNameText;

    public void GoToMenu()
    {

    }

    public void setPlayerNameToActivePlayer()
    {
        PlayerControllerScript pControl = GameObject.Find("PlayerController").GetComponent<PlayerControllerScript>();
        TurnControllerScript tControl = GameObject.Find("TurnController").GetComponent<TurnControllerScript>();
        playerNameText.text = pControl.GetPlayerName(tControl.GetActivePlayer());
    }
}
