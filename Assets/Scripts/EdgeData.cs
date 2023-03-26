using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeData : MonoBehaviour
{
    public int ownerPlayer = 0;
    public bool roadBuilt = false;
    public Vector3Int vertex1;
    public Vector3Int vertex2;

    [SerializeField] private GameObject _buildButton;
    [SerializeField] private GameObject edgeSprite;


    private void Update()
    {
        EnableDisableButton();
    }
    public void ClickBuildButton()
    {
        TurnControllerScript tControl = GameObject.Find("TurnController").GetComponent<TurnControllerScript>();
        PlayerControllerScript pControl = GameObject.Find("PlayerController").GetComponent<PlayerControllerScript>();
        edgeSprite.SetActive(true);

        Debug.Log("Button " + vertex1 + " " + vertex2);
        roadBuilt = true;
        ownerPlayer = tControl.GetActivePlayer();
        edgeSprite.GetComponent<SpriteRenderer>().color = pControl.GetPlayerColor(ownerPlayer);
        tControl.MoveTurnAlong();
    }

    private void EnableDisableButton()
    {
        if (EnableButtonCheck() == true)
        {
            _buildButton.gameObject.SetActive(true);
        }
        else
        {
            _buildButton.gameObject.SetActive(false);
        }
    }

    private bool EnableButtonCheck()
    {
        if (roadBuilt == false)
        {
            TurnControllerScript tControl = GameObject.Find("TurnController").GetComponent<TurnControllerScript>();
            //if adjecent vertex or road
            if (CheckForAdjacentOwnedVertexOrRoad() == true)
            {
                //if setup road step then enable
                if(tControl.GetTurnStep() == 0)
                {
                    return (true);
                }
                else if (tControl.GetTurnStep() == 3) //if build step then
                {
                    if (CheckResourcesAvailable()) //if active player has resources
                    {
                        return (true);
                    }
                }
            }
        }
        return (false);
    }

    private bool CheckResourcesAvailable()
    {
        PlayerControllerScript pControl = GameObject.Find("PlayerController").GetComponent<PlayerControllerScript>();
        TurnControllerScript tControl = GameObject.Find("TurnController").GetComponent<TurnControllerScript>();
        int activePlayer = tControl.GetActivePlayer();
        if ((pControl.GetPlayerResource(activePlayer, Resources.Brick) >= 1) && (pControl.GetPlayerResource(activePlayer, Resources.Lumber) >= 1))
        {
            return (true);
        }
        else
        {
            return (false);
        }
    }

    //returns the int id of the player who owns the road, 0 if not owned
    public int CheckOwningPlayer()
    {
        return (ownerPlayer);
    }

    //check if a vertex or an edge next to those vertexs are owned by the active player
    private bool CheckForAdjacentOwnedVertexOrRoad()
    {
        TurnControllerScript tControl = GameObject.Find("TurnController").GetComponent<TurnControllerScript>();
        GridControlScript gControl = GameObject.Find("GridController").GetComponent<GridControlScript>();

        VertexData vertex1Data = gControl.GetVertex(vertex1).GetComponent<VertexData>();
        VertexData vertex2Data = gControl.GetVertex(vertex2).GetComponent<VertexData>();
        //check ownershio of both vertexes
        if ((vertex1Data.ownerPlayer == tControl.GetActivePlayer()) || vertex2Data.ownerPlayer == tControl.GetActivePlayer())
        {
            return (true);
        }
        else if ((vertex1Data.CheckAdjacentOwnedRoad(vertex1, vertex2) == true) || (vertex2Data.CheckAdjacentOwnedRoad(vertex1, vertex2) == true)) //check it's adjencent edges, with this edge as an exclusion
        {
            return (true);
        }
        else
        {
            return (false);
        }
    }
}
