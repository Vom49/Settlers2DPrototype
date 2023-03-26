using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VertexData : MonoBehaviour
{
    //the ID of this vertex, set as -1 -1 -1 as a default, this is changed when it is instaciated
     public Vector3Int VertexID = new Vector3Int(-1, -1, -1);

    public int buildingValue = 0; //value of the building, no building is 0, village is 1, city is 2
    public int ownerPlayer = 0; //when a building is built this value changes to reflect who owns that building
    public bool buildingBlock = false;

    [SerializeField] private GameObject villageSprite;
    [SerializeField] private GameObject citySprite;

    [SerializeField] private Button _buildButton;


    //player infomation

    private void Start()
    {
        
    }

    //update function to enable or disable the button
    private void Update()
    {
        EnableDisableButton();
    }
    public void ClickBuildButton()
    {
        TurnControllerScript tControl = GameObject.Find("TurnController").GetComponent<TurnControllerScript>();
        Debug.Log("click " + VertexID);
        buildingValue++;
        ownerPlayer = tControl.GetActivePlayer();
        PreventAdjacentVillages();
        PlayerControllerScript pControl = GameObject.Find("PlayerController").GetComponent<PlayerControllerScript>();
        villageSprite.GetComponent<SpriteRenderer>().color = pControl.GetPlayerColor(ownerPlayer);
        citySprite.GetComponent<SpriteRenderer>().color = pControl.GetPlayerColor(ownerPlayer);
        if (buildingValue == 1)
        {
            
            villageSprite.SetActive(true);
            citySprite.SetActive(false);
        } 
        else
        {
            villageSprite.SetActive(false);
            citySprite.SetActive(true);
        }
        //add 1 victory point, as village is worth 1 and a city is worth 1 addiontal point
        pControl.EditPlayerResource(ownerPlayer, Resources.VictoryPoints, 1);
        tControl.MoveTurnAlong();
    }

    public List<Vector3Int> FindAdjacentVertices()
    {
        List<Vector3Int> AdjacentVertices = new List<Vector3Int>();

        int VertexX = VertexID.x;
        int VertexY = VertexID.y;
        int VertexK = VertexID.z;

        if (VertexK == 0) //if it is a north vertex
        {
            AdjacentVertices.Clear();
            AdjacentVertices.Add(new Vector3Int(VertexX - 1, VertexY, 1));
            AdjacentVertices.Add(new Vector3Int(VertexX, VertexY - 1, 1));
            AdjacentVertices.Add(new Vector3Int(VertexX, VertexY, 1));
        }
        else if (VertexK == 1) //if it is a north east vertex
        {
            AdjacentVertices.Clear();
            AdjacentVertices.Add(new Vector3Int(VertexX, VertexY, 0));
            AdjacentVertices.Add(new Vector3Int(VertexX, VertexY + 1, 0));
            AdjacentVertices.Add(new Vector3Int(VertexX + 1, VertexY, 0));
        }
        else
        {
            Debug.Log(this.gameObject.name + " has error, K is out of scope");
        }
        //Debug.Log(VertexID + " | "+ AdjacentVertices[0] + " + " + AdjacentVertices[1] + " + " + AdjacentVertices[2]);
        return (AdjacentVertices);
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
    //checks wether the build button should be clickable
    private bool EnableButtonCheck()
    {
        TurnControllerScript tControl = GameObject.Find("TurnController").GetComponent<TurnControllerScript>();
        PlayerControllerScript pControl = GameObject.Find("PlayerController").GetComponent<PlayerControllerScript>();
        int activePlayer = tControl.GetActivePlayer();
        //if we're in the build step
        if (tControl.GetTurnStep() == 3)
        {
            if ((buildingValue == 1) && (ownerPlayer == activePlayer)) //will only activate for building a city
            {
                Debug.Log("city build");
                //current player has the resources
                if ((pControl.GetPlayerResource(activePlayer, Resources.Ore) >= 3) && (pControl.GetPlayerResource(activePlayer, Resources.Grain) >= 2))
                {
                    return (true);
                }
            }
            else if ((CheckAdjacentOwnedRoad(null, null) == true) && (buildingBlock == false) && buildingValue == 0) //building village
            {
                //current player has the resources
                if ((pControl.GetPlayerResource(activePlayer, Resources.Brick) >= 1) && (pControl.GetPlayerResource(activePlayer, Resources.Grain) >= 1) && (pControl.GetPlayerResource(activePlayer, Resources.Sheep) >= 1) && (pControl.GetPlayerResource(activePlayer, Resources.Lumber) >= 1))
                {
                    return (true);
                }
            }
            //and current player has the resources
        }
        else if((tControl.GetTurnStep() == -1) && (buildingValue < 1)) //-1 is setup for placing starting villages
        {
            if (buildingBlock == false)
            {
                return (true);
            }
        }
        return (false);
    }

    //this finds adhacent Vertices and prevents them from being buildable
    private void PreventAdjacentVillages()
    {
        GridControlScript gControl = GameObject.Find("GridController").GetComponent<GridControlScript>();
        List<Vector3Int> AdjacentVertices = FindAdjacentVertices();
        //Debug.Log(VertexID + " | " + AdjacentVertices[0] + " + " + AdjacentVertices[1] + " + " + AdjacentVertices[2]);
        for (int i = 0; i < 3; i++)
        {
            //make sure it's not null
            if (gControl.GetVertex(AdjacentVertices[i]) != null)
            {
                //Debug.Log(gControl.GetVertex(AdjacentVertices[i]).name + " " + AdjacentVertices[i]);
                gControl.GetVertex(AdjacentVertices[i]).GetComponent<VertexData>().buildingBlock = true;
            }
        }
    }

    private bool CheckResourcesAvailable()
    {
        return (true);
    }

    //checks if it has an Adjacent road owned by the active player, has optional parameters for a road to exclude from the check
    public bool CheckAdjacentOwnedRoad(Vector3Int? exclusionRoadVector1, Vector3Int? exclusionRoadVector2)
    {
        List<Vector3Int> AdjacentVertices = FindAdjacentVertices();
        GridControlScript gControl = GameObject.Find("GridController").GetComponent<GridControlScript>();
        TurnControllerScript tControl = GameObject.Find("TurnController").GetComponent<TurnControllerScript>();

        for (int i = 0; i < 3; i++)
        {
            GameObject targetEdge = gControl.getEdge(AdjacentVertices[i], VertexID);
            if (targetEdge != null)
            {
                //if this edge is the exclusion edge then do nothing if not follow else
                if ((exclusionRoadVector1 != null && exclusionRoadVector2 != null) && (gControl.getEdge(exclusionRoadVector1.GetValueOrDefault(), exclusionRoadVector2.GetValueOrDefault()) == targetEdge))
                {
                    //this is used to skip the owner check if this edge is the one that is excluded from the check
                }
                else
                {
                    //if this edge exists but is not the exlusion edge
                    if(targetEdge.GetComponent<EdgeData>().CheckOwningPlayer() == tControl.GetActivePlayer())
                    {
                        Debug.Log("edge owned");
                        return (true);
                    }
                }
            }
        }
        //if it goes through the whole for loop without finding an adjencent owned edge that is not the exlusion edge
        return (false);
    }
}
