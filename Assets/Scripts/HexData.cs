using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum Resources
{ 
    Nothing,
    Brick,
    Lumber,
    Ore,
    Grain,
    Sheep,
    VictoryPoints
}

public class HexData : MonoBehaviour
{
    public int myX;
    public int myY;
    public Resources ProducedResource = Resources.Nothing;
    public int outputNumber = 0;
    public bool isRobbed = false;

    [SerializeField] private TMP_Text outputNumText;
    [SerializeField] private SpriteRenderer hexSpriteRenderer;
    [SerializeField] private GameObject robberSprite;
    [SerializeField] private GameObject robButton;

    private void Start()
    {
    }

    private void Update()
    {
        EnableDisableButton();
    }
    public void AssignHexData(Resources desiredResource, int desiredOutputNum)
    {
        ProducedResource = desiredResource;
        outputNumber = desiredOutputNum;
        
        //hide the number on the desert
        if (desiredOutputNum == 0)
        {
            outputNumText.text = "";
        }
        else
        {
            string outputString = outputNumber.ToString();
            outputNumText.text = outputString;
        }
        
        
        switch (desiredResource)
        {
            case Resources.Brick:
                hexSpriteRenderer.color = new Color(200 / 255f, 90 / 255f, 60 / 255f);
                break;
            case Resources.Lumber:
                hexSpriteRenderer.color = new Color(30 / 255f, 110 / 255f, 15 / 255f);
                break;
            case Resources.Ore:
                hexSpriteRenderer.color = new Color(162 / 255f, 190 / 255f, 190 / 255f);
                break;
            case Resources.Grain:
                hexSpriteRenderer.color = new Color(225 / 255f, 185 / 255f, 25 / 255f);
                break;
            case Resources.Sheep:
                hexSpriteRenderer.color = new Color(110 / 255f, 200 / 255f, 45 / 255f);
                break;
            default:
                hexSpriteRenderer.color = new Color(235 / 255f, 230 / 255f, 160 / 255f);
                break;
        }
        
    }

    private List<Vector3Int> GetSurrondingVertices()
    {
        List<Vector3Int> SurrondingVertices = new List<Vector3Int>();
        SurrondingVertices.Add(new Vector3Int(myX, myY, 0));
        SurrondingVertices.Add(new Vector3Int(myX, myY, 1));

        SurrondingVertices.Add(new Vector3Int(myX, myY + 1, 0));
        SurrondingVertices.Add(new Vector3Int(myX -1, myY+ 1, 1));

        SurrondingVertices.Add(new Vector3Int(myX -1, myY + 1, 0));
        SurrondingVertices.Add(new Vector3Int(myX -1, myY, 1));
        return (SurrondingVertices);
    }

    public void DistributeResources()
    {
        if (!isRobbed) //avoids the check if the the hex has a robber
        {
            GridControlScript gControl = GameObject.Find("GridController").GetComponent<GridControlScript>();
            PlayerControllerScript pControl = GameObject.Find("PlayerController").GetComponent<PlayerControllerScript>();
            List<Vector3Int> surrondingVertices = GetSurrondingVertices();

            for (int i = 0; i < 6; i++)
            {
                GameObject currentVertex = gControl.GetVertex(surrondingVertices[i]);
                VertexData currentVertexData = currentVertex.GetComponent<VertexData>();
                if (currentVertexData.ownerPlayer != 0) //if it has an owner
                {
                    pControl.EditPlayerResource(currentVertexData.ownerPlayer, ProducedResource, currentVertexData.buildingValue);
                }
            }
        }
    }

    public void ApplyRobber()
    {
        GridControlScript gControl = GameObject.Find("GridController").GetComponent<GridControlScript>();
        TurnControllerScript tControl = GameObject.Find("TurnController").GetComponent<TurnControllerScript>();
        PlayerControllerScript pControl = GameObject.Find("PlayerController").GetComponent<PlayerControllerScript>();
        //do smth to make sure no other hex is being robbed currently
        isRobbed = true;
        robberSprite.SetActive(true);

        //stealling from players on the hex
        List<Vector3Int> surrondingVertices = GetSurrondingVertices();
        for (int i = 0; i < 6; i++)
        {
            GameObject currentVertex = gControl.GetVertex(surrondingVertices[i]);
            VertexData currentVertexData = currentVertex.GetComponent<VertexData>();
            if ((currentVertexData.ownerPlayer != 0) && (currentVertexData.ownerPlayer != tControl.GetActivePlayer()))//if it has an owner
            {
                List<Resources> stealableResources = new List<Resources>();
                //make sure the player has resources to steal
                if (pControl.GetPlayerResource(currentVertexData.ownerPlayer, Resources.Brick) > 0)
                {
                    stealableResources.Add(Resources.Brick);
                }
                if (pControl.GetPlayerResource(currentVertexData.ownerPlayer, Resources.Lumber) > 0)
                {
                    stealableResources.Add(Resources.Lumber);
                }
                if (pControl.GetPlayerResource(currentVertexData.ownerPlayer, Resources.Ore) > 0)
                {
                    stealableResources.Add(Resources.Ore);
                }
                if (pControl.GetPlayerResource(currentVertexData.ownerPlayer, Resources.Grain) > 0)
                {
                    stealableResources.Add(Resources.Grain);
                }
                if (pControl.GetPlayerResource(currentVertexData.ownerPlayer, Resources.Sheep) > 0)
                {
                    stealableResources.Add(Resources.Sheep);
                }

                int randomResourceNum = Random.Range(0, stealableResources.Count);
                pControl.EditPlayerResource(currentVertexData.ownerPlayer, stealableResources[randomResourceNum], -1);
                pControl.EditPlayerResource(tControl.GetActivePlayer(), stealableResources[randomResourceNum], 1);
            }
        }
        //this prevents the turnstep from moving when in the setup
        if (tControl.turnStep == 66)
        {
            tControl.MoveTurnAlong();
        }
    }
    public void RemoveRobber()
    {
        robberSprite.SetActive(false);
    }

    private void EnableDisableButton()
    {
        TurnControllerScript tControl = GameObject.Find("TurnController").GetComponent<TurnControllerScript>();

        //robButton
        if (tControl.turnStep == 66)
        {
            robButton.SetActive(true);
        } 
        else
        {
            robButton.SetActive(false);
        }
    }
}
