using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiceRoller : MonoBehaviour
{
    [SerializeField] TMP_Text _DiceOutput;


    //make dice roll button appear on turnstep 1
    public void OnClick()
    {
        //generate multiple 1-6 nums
        int dice1 = Random.Range(1, 7);
        int dice2 = Random.Range(1, 7);
        int DiceTotal = dice1 + dice2;
        GridControlScript gControl = GameObject.Find("GridController").GetComponent<GridControlScript>();
        _DiceOutput.text = DiceTotal.ToString();
        if (DiceTotal == 7) //robber
        {
            TurnControllerScript tControl = GameObject.Find("TurnController").GetComponent<TurnControllerScript>();
            tControl.GoToRobbing();
        }
        else
        {
            gControl.ResourceProduction(DiceTotal);
        }
    }
}
