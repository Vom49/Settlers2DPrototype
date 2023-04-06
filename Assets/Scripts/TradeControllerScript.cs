using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TradeControllerScript : MonoBehaviour
{
    PlayerControllerScript pControl;
    TurnControllerScript tControl;

    [SerializeField] private GameObject TradeWidget;
    [SerializeField] private GameObject TradeOfferButton;
    [SerializeField] private TMP_Text ActivePlayerText;

    void Start()
    {
        pControl = GameObject.Find("PlayerController").GetComponent<PlayerControllerScript>();
        tControl = GameObject.Find("TurnController").GetComponent<TurnControllerScript>();
    }


    // Update is called once per frame
    void Update()
    {
        EnableDisableTradeWidget();
        EnableDisableOfferButton();
        UpdateActivePlayerText();
    }

    private void EnableDisableTradeWidget()
    {

    }
    private void EnableDisableOfferButton()
    {

    }
    private void UpdateActivePlayerText()
    {
        ActivePlayerText.text = pControl.GetPlayerName(tControl.GetActivePlayer());
        ActivePlayerText.color = pControl.GetPlayerColor(tControl.GetActivePlayer());
    }
}
