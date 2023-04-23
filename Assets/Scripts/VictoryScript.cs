using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class VictoryScript : MonoBehaviour
{
    [SerializeField] private TMP_Text playerNameText;

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void setPlayerNameToActivePlayer()
    {
        PlayerControllerScript pControl = GameObject.Find("PlayerController").GetComponent<PlayerControllerScript>();
        TurnControllerScript tControl = GameObject.Find("TurnController").GetComponent<TurnControllerScript>();
        playerNameText.text = pControl.GetPlayerName(tControl.GetActivePlayer());
    }
}
