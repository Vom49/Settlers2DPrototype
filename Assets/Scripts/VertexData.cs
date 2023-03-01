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
        Debug.Log("click " + VertexID);
        buildingValue++;
        ownerPlayer = GameObject.Find("TurnController").GetComponent<TurnControllerScript>().GetActivePlayer();
        PreventAdjacentVillages();
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
        //if we're in the build step
        if (tControl.GetTurnStep() == 3)
        {
            if ((buildingValue == 1) && (ownerPlayer == tControl.GetActivePlayer())) //will only activate for building a city
            {
                Debug.Log("city build");
                //and current player has the resources
                return (true);
            }
            else if ((HasAdjacentOwnedRoad() == true) && (buildingBlock == false) && buildingValue == 0) //building village
            {
                //and current player has the resources
                return (true);
            }
            //and current player has the resources
        }
        else if(tControl.GetTurnStep() == -1) //-1 is setup for placing starting villages
        {
            if (buildingBlock == false)
            {
                return (true);
            }
        }
        

        //or start of game

        //and this is a village owned by that player
        //or there is no village on the immediate edge
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

    private bool HasAdjacentOwnedRoad()
    {
        //check adjecents and check edges
        return (true);
    }
}
