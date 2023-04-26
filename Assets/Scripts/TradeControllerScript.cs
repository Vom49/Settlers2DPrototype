using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TradeControllerScript : MonoBehaviour
{
    PlayerControllerScript pControl;
    TurnControllerScript tControl;

    public int currentTradeTarget = 0;

    [SerializeField] private GameObject TradeWidget;
    [SerializeField] private GameObject TradeOfferButton;
    [SerializeField] private TMP_Text ActivePlayerText;

    [SerializeField] private TradeDialData BrickDial;
    [SerializeField] private TradeDialData LumberDial;
    [SerializeField] private TradeDialData OreDial;
    [SerializeField] private TradeDialData GrainDial;
    [SerializeField] private TradeDialData SheepDial;

    private int[] indexToIDlookup;

    [SerializeField] private TMP_Dropdown TradeTargetsDropdown;
    private List<string> tradeableTargetsList = new List<string>();

    void Start()
    {
        pControl = GameObject.Find("PlayerController").GetComponent<PlayerControllerScript>();
        tControl = GameObject.Find("TurnController").GetComponent<TurnControllerScript>();
        indexToIDlookup = new int[5];
        UpdateTradeTargets();
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
        if (tControl.GetTurnStep() == 2)
        {
            TradeWidget.SetActive(true);
        }
        else
        {
            TradeWidget.SetActive(false);
        }
    }
    private void EnableDisableOfferButton()
    {
        if (currentTradeTarget == 0)
        {
            int playerGainCount = 0;
            int bankGainCount = 0;
            if (BrickDial.GetDialValue() > 0)
            {
                playerGainCount += BrickDial.GetDialValue();
            }
            else if (BrickDial.GetDialValue() < 0)
            {
                bankGainCount += -(BrickDial.GetDialValue());
            }

            if (LumberDial.GetDialValue() > 0)
            {
                playerGainCount += LumberDial.GetDialValue();
            }
            else if (LumberDial.GetDialValue() < 0)
            {
                bankGainCount += -(LumberDial.GetDialValue());
            }

            if (OreDial.GetDialValue() > 0)
            {
                playerGainCount += OreDial.GetDialValue();
            }
            else if (OreDial.GetDialValue() < 0)
            {
                bankGainCount += -(OreDial.GetDialValue());
            }

            if (GrainDial.GetDialValue() > 0)
            {
                playerGainCount += GrainDial.GetDialValue();
            }
            else if (GrainDial.GetDialValue() < 0)
            {
                bankGainCount += -(GrainDial.GetDialValue());
            }

            if (SheepDial.GetDialValue() > 0)
            {
                playerGainCount += SheepDial.GetDialValue();
            }
            else if (SheepDial.GetDialValue() < 0)
            {
                bankGainCount += -(SheepDial.GetDialValue());
            }

            if ((ALLDialsPossible() == true) && playerGainCount <= bankGainCount/4)
            {
                TradeOfferButton.SetActive(true);
            }
            else
            {
                TradeOfferButton.SetActive(false);
            }
        }
        else
        {
            TradeOfferButton.SetActive(ALLDialsPossible());
        }
    }
    private void UpdateActivePlayerText()
    {
        ActivePlayerText.text = pControl.GetPlayerName(tControl.GetActivePlayer());
        ActivePlayerText.color = pControl.GetPlayerColor(tControl.GetActivePlayer());
    }

    public void DropdownChange()
    {
        int dropdownIndex = TradeTargetsDropdown.value;
        currentTradeTarget = indexToIDlookup[dropdownIndex];
    }

    public void UpdateTradeTargets()
    {
        tradeableTargetsList.Clear();
        tradeableTargetsList.Add("Bank");
        indexToIDlookup[0] = 0;
        int indexTracker = 1;
        for (int i = 1; i <= pControl.maxPlayers; i++)
        {
            if (i != tControl.GetActivePlayer())
            {
                tradeableTargetsList.Add(pControl.GetPlayerName(i));
                indexToIDlookup[indexTracker] = i;
                //Debug.Log(indexToIDlookup[indexTracker]);
                indexTracker++;
            }
        }
        TradeTargetsDropdown.ClearOptions();
        TradeTargetsDropdown.AddOptions(tradeableTargetsList);
    }

    private bool ALLDialsPossible()
    {
        if (CheckSingleDialValues(BrickDial) == false)
        {
            return (false);
        }
        if (CheckSingleDialValues(LumberDial) == false)
        {
            return (false);
        }
        if (CheckSingleDialValues(OreDial) == false)
        {
            return (false);
        }
        if (CheckSingleDialValues(GrainDial) == false)
        {
            return (false);
        }
        if (CheckSingleDialValues(SheepDial) == false)
        {
            return (false);
        }
        return (true);
    }

    private bool CheckSingleDialValues(TradeDialData dial)
    {
        //taken from target
        if (dial.GetDialValue() > 0)
        {
            if (currentTradeTarget == 0)
            {
                return (true);
            }
            else if (pControl.GetPlayerResource(currentTradeTarget, dial.GetDialResource()) >= dial.GetDialValue()) //check resources
            {
                return (true);
            }
            else
            {
                return (false);
            }
        }
        else if (dial.GetDialValue() < 0) //taken from active player
        {
            if (pControl.GetPlayerResource(tControl.GetActivePlayer(), dial.GetDialResource()) >= dial.GetDialValue()) //check resources
            {
                return (true);
            }
            else
            {
                return (false);
            }
        }
        else
        {
            return (true);
        }
    }

    public void Trade()
    {
        //brick
        pControl.EditPlayerResource(tControl.activePlayer, Resources.Brick, BrickDial.GetDialValue());
        pControl.EditPlayerResource(currentTradeTarget, Resources.Brick, -(BrickDial.GetDialValue()));
        BrickDial.ResetValue();

        //lumber
        pControl.EditPlayerResource(tControl.activePlayer, Resources.Lumber, LumberDial.GetDialValue());
        pControl.EditPlayerResource(currentTradeTarget, Resources.Lumber, -(LumberDial.GetDialValue()));
        LumberDial.ResetValue();

        //ore
        pControl.EditPlayerResource(tControl.activePlayer, Resources.Ore, OreDial.GetDialValue());
        pControl.EditPlayerResource(currentTradeTarget, Resources.Ore, -(OreDial.GetDialValue()));
        OreDial.ResetValue();

        //grain
        pControl.EditPlayerResource(tControl.activePlayer, Resources.Grain, GrainDial.GetDialValue());
        pControl.EditPlayerResource(currentTradeTarget, Resources.Grain, -(GrainDial.GetDialValue()));
        GrainDial.ResetValue();

        //sheep
        pControl.EditPlayerResource(tControl.activePlayer, Resources.Sheep, SheepDial.GetDialValue());
        pControl.EditPlayerResource(currentTradeTarget, Resources.Sheep, -(SheepDial.GetDialValue()));
        SheepDial.ResetValue();
    }
}
