using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VertexData : MonoBehaviour
{
    //the ID of this vertex, set as -1 -1 -1 as a default, this is changed when it is instaciated
    [HideInInspector] public Vector3Int VertexID = new Vector3Int(-1, -1, -1);

    public int buildingValue = 0; //value of the building, no building is 0, village is 1, city is 2
    public int ownerPlayer = 0; //when a building is built this value changes to reflect who owns that building

    [SerializeField] private Button _buildButton;


    //player infomation
    public List<Vector3Int> FindAdjacentVertices()
    {
        List<Vector3Int> AdjacentVertices = new List<Vector3Int>();

        int VertexX = VertexID.x;
        int VertexY = VertexID.y;
        int VertexK = VertexID.z;

        if (VertexK == 0) //if it is a north vertex
        {
            AdjacentVertices.Add(new Vector3Int(VertexX - 1, VertexY, 1));
            AdjacentVertices.Add(new Vector3Int(VertexX, VertexY - 1, 1));
            AdjacentVertices.Add(new Vector3Int(VertexX, VertexY, 1));
        }
        else if (VertexK == 1) //if it is a north east vertex
        {
            AdjacentVertices.Add(new Vector3Int(VertexX, VertexY, 0));
            AdjacentVertices.Add(new Vector3Int(VertexX, VertexY + 1, 0));
            AdjacentVertices.Add(new Vector3Int(VertexX + 1, VertexY, 0));
        }
        else
        {
            Debug.Log(this.gameObject.name + " has error, K is out of scope");
        }
        return (AdjacentVertices);
    }

    //update function to enable or disable the button

    public void ClickBuildButton()
    {

    }

    //checks wether the build button should be clickable
    private void EnableButtonCheck()
    {
        TurnControllerScript tControl = GameObject.Find("TurnController").GetComponent<TurnControllerScript>();
        //if we're in the build step
        if (tControl.GetTurnStep() == 3)
        {
            if ((buildingValue == 1) && (ownerPlayer == tControl.GetActivePlayer()))
            {

            }
            else if (HasNoRoadOrVillageBlocks() == true)
            {

            }
            //and current player has the resources
        }
        //and current player has the resources

        //or start of game

        //and this is a village owned by that player
        //or there is no village on the immediate edge
    }

    private bool HasNoRoadOrVillageBlocks()
    {
        return (true);
    }
}
