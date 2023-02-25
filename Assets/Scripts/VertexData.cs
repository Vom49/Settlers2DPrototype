using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexData : MonoBehaviour
{
    //the ID of this vertex, set as -1 -1 -1 as a default, this is changed when it is instaciated
    [HideInInspector] public Vector3Int VertexID = new Vector3Int(-1, -1, -1); 

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
}
