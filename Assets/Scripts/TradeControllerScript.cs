using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeControllerScript : MonoBehaviour
{
    PlayerControllerScript pControl;
    TurnControllerScript tControl;

    [SerializeField] private GameObject TradeWidget;
    [SerializeField] private GameObject TradeOfferButton;
    
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
    }

    private void EnableDisableTradeWidget()
    {

    }
    private void EnableDisableOfferButton()
    {

    }
}
