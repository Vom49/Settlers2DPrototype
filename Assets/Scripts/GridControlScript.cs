using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridControlScript : MonoBehaviour
{
    //prefab for the hex that it will draw
    [SerializeField] private GameObject _HexPrefab;
    //prefab for the vertex it will draw
    [SerializeField] private GameObject _VertexPrefab;
    //prefab for the edge of the hex
    [SerializeField] private GameObject _EdgePrefab;

    //generate the premade catan board
    [SerializeField] private bool premadeBoard;

    //main storage and access to hexes
    [HideInInspector] public static GameObject[,] HexGridArray;

    //uses vector3int as key to hold X Y and K coords of the Vertexes
    [HideInInspector] private static Dictionary<Vector3Int, GameObject> VertexDict;

    //holds a pair of vertex coords to identify the edge
    [HideInInspector] public static Dictionary<(Vector3Int, Vector3Int), GameObject> EdgeDict;

    //width and height in hexes, nothing to do with unity coords this needs to be large enough so all play hexes are surronded by other hexes
    const int gridWidth = 7;
    const int gridHeight = 7;


    // Start is called before the first frame update
    void Start()
    {
        drawGridAxial();
        drawVertices();
        destroyExcessHexes();
        destroyExcessVertices();
        drawEdges();
        assignResourcesAndNums();

        //adjusts the scale of the grid
        transform.localScale += new Vector3(1f, 1f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //draws the grid using an Axial method of storing the grid
    //Axial hex grid means that a column cascades down to the right
    private void drawGridAxial()
    {
        const float xOffset = 0.457f;
        const float yOffset = 0.795f;
        HexGridArray = new GameObject[gridWidth, gridHeight];
        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                float xpos = (j + i) * xOffset + (i * xOffset);
                float ypos = -j * yOffset;

                //creates the grid reletive to the grid controller object using this.gameObject.transform.position vector
                GameObject hexInstance = (GameObject)Instantiate(_HexPrefab, new Vector2(this.gameObject.transform.position.x + xpos, this.gameObject.transform.position.y + ypos), Quaternion.identity); //creates and saves the hex that was just created
                hexInstance.transform.SetParent(this.gameObject.transform); //for readability in editor

                hexInstance.name = "Hex_" + i + "_" + j;
                HexGridArray[i, j] = hexInstance;
                HexGridArray[i, j].GetComponent<HexData>().myX = i;
                HexGridArray[i, j].GetComponent<HexData>().myY = j;
            }
        }

        
        
    }
    //draws the vertices of the grid
    private void drawVertices()
    {
        //offsets for the north vertex and the northeast vertex
        const float nVertexYOffset = 0.5f;
        const float neVertexXOffset = 0.455f;
        const float neVertexYOffset = 0.28f;

        VertexDict = new Dictionary<Vector3Int, GameObject>();

        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                int hexX = i;
                int hexY = j;
                GameObject targetHex = HexGridArray[hexX, hexY];

                GameObject vertexInstance1 = (GameObject)Instantiate(_VertexPrefab, new Vector2(targetHex.transform.position.x, targetHex.transform.position.y + nVertexYOffset), Quaternion.identity);
                vertexInstance1.name = "Vertex_" + hexX + "_" + hexY + "_0"; //north vertex
                vertexInstance1.transform.SetParent(this.gameObject.transform); //for readability in editor
                VertexDict.Add(new Vector3Int(hexX, hexY, 0), vertexInstance1);
                //Infrom the Vertex script of it's own ID used to increase runtime performance
                vertexInstance1.GetComponent<VertexData>().VertexID = new Vector3Int(hexX, hexY, 0);


                GameObject vertexInstance2 = (GameObject)Instantiate(_VertexPrefab, new Vector2(targetHex.transform.position.x + neVertexXOffset, targetHex.transform.position.y + neVertexYOffset), Quaternion.identity);
                vertexInstance2.name = "Vertex_" + hexX + "_" + hexY + "_1"; //north east vertex
                vertexInstance2.transform.SetParent(this.gameObject.transform); //for readability in editor
                VertexDict.Add(new Vector3Int(hexX, hexY, 1), vertexInstance1);
                //Infrom the Vertex script of it's own ID used to increase runtime performance
                vertexInstance2.GetComponent<VertexData>().VertexID = new Vector3Int(hexX, hexY, 1);
            }
        }
    }

    //because of how the board renders to generate all required vertexes, it must make hexes that are not used in the game
    private void destroyExcessHexes()
    {
        //destroys the far left and far right columns
        
        for (int i = 0; i < gridHeight; i++)
        {
            Destroy(GameObject.Find("Hex_0_" + i));
            HexGridArray[0, i] = null;
            Destroy(GameObject.Find("Hex_6_" + i));
            HexGridArray[6, i] = null;
        }

        for (int j = 1; j < gridWidth - 1; j++)
        {
            Destroy(GameObject.Find("Hex_" + j + "_0"));
            HexGridArray[j, 0] = null;
            Destroy(GameObject.Find("Hex_" + j + "_6"));
            HexGridArray[j, 6] = null;
        }

        //removes hexes that are not on the catan board but aren't removed by the loops
        Destroy(GameObject.Find("Hex_1_1"));
        HexGridArray[1, 1] = null;
        Destroy(GameObject.Find("Hex_1_2"));
        HexGridArray[1, 2] = null;
        Destroy(GameObject.Find("Hex_2_1"));
        HexGridArray[2, 1] = null;
        Destroy(GameObject.Find("Hex_5_4"));
        HexGridArray[5, 4] = null;
        Destroy(GameObject.Find("Hex_5_5"));
        HexGridArray[5, 5] = null;
        Destroy(GameObject.Find("Hex_4_5"));
        HexGridArray[4, 5] = null;
    }

    private void destroyExcessVertices()
    {
        //loop through and remove far right column
        for (int i = 0; i < gridWidth; i++)
        {
            Destroy(GameObject.Find("Vertex_6_" + i + "_0"));
            VertexDict.Remove(new Vector3Int(6, i, 0));
            Destroy(GameObject.Find("Vertex_6_" + i + "_1"));
            VertexDict.Remove(new Vector3Int(6, i, 1));
        }
        //loop through and remove top row
        for (int j = 0; j < gridHeight; j++)
        {
            Destroy(GameObject.Find("Vertex_" + j +"_0_0"));
            VertexDict.Remove(new Vector3Int(j, 0, 0));
            Destroy(GameObject.Find("Vertex_" + j + "_0_1"));
            VertexDict.Remove(new Vector3Int(j, 0, 1));
        }

        //remove all excess vertices that are not removed by loops (I'm sorry, I hate this too)
        Destroy(GameObject.Find("Vertex_1_0_0"));
        VertexDict.Remove(new Vector3Int(1, 0, 0));
        Destroy(GameObject.Find("Vertex_1_0_1"));
        VertexDict.Remove(new Vector3Int(1, 0, 1));

        Destroy(GameObject.Find("Vertex_0_1_0"));
        VertexDict.Remove(new Vector3Int(0, 1, 0));
        Destroy(GameObject.Find("Vertex_0_1_1"));
        VertexDict.Remove(new Vector3Int(0, 1, 1));

        Destroy(GameObject.Find("Vertex_0_2_0"));
        VertexDict.Remove(new Vector3Int(0, 2, 0));
        Destroy(GameObject.Find("Vertex_0_2_1"));
        VertexDict.Remove(new Vector3Int(0, 2, 1));

        Destroy(GameObject.Find("Vertex_2_0_0"));
        VertexDict.Remove(new Vector3Int(2, 0, 0));
        Destroy(GameObject.Find("Vertex_2_0_1"));
        VertexDict.Remove(new Vector3Int(2, 0, 1));

        Destroy(GameObject.Find("Vertex_1_1_0"));
        VertexDict.Remove(new Vector3Int(1, 1, 0));
        Destroy(GameObject.Find("Vertex_1_1_1"));
        VertexDict.Remove(new Vector3Int(1, 1, 1));

        Destroy(GameObject.Find("Vertex_4_6_0"));
        VertexDict.Remove(new Vector3Int(4, 6, 0));
        Destroy(GameObject.Find("Vertex_4_6_1"));
        VertexDict.Remove(new Vector3Int(4, 6, 1));

        Destroy(GameObject.Find("Vertex_5_6_0"));
        VertexDict.Remove(new Vector3Int(5, 6, 0));
        Destroy(GameObject.Find("Vertex_5_6_1"));
        VertexDict.Remove(new Vector3Int(5, 6, 1));

        Destroy(GameObject.Find("Vertex_5_5_0"));
        VertexDict.Remove(new Vector3Int(5, 5, 0));
        Destroy(GameObject.Find("Vertex_5_5_1"));
        VertexDict.Remove(new Vector3Int(5, 5, 1));

        Destroy(GameObject.Find("Vertex_2_1_0"));
        VertexDict.Remove(new Vector3Int(2, 1, 0));

        Destroy(GameObject.Find("Vertex_1_2_0"));
        VertexDict.Remove(new Vector3Int(1, 2, 0));

        Destroy(GameObject.Find("Vertex_2_1_0"));
        VertexDict.Remove(new Vector3Int(2, 1, 0));

        Destroy(GameObject.Find("Vertex_0_3_0"));
        VertexDict.Remove(new Vector3Int(0, 3, 0));

        Destroy(GameObject.Find("Vertex_5_4_1"));
        VertexDict.Remove(new Vector3Int(5, 4, 1));

        Destroy(GameObject.Find("Vertex_4_5_1"));
        VertexDict.Remove(new Vector3Int(4, 5, 1));

        Destroy(GameObject.Find("Vertex_3_6_1"));
        VertexDict.Remove(new Vector3Int(3, 6, 1));
    }

    private void drawEdges()
    {
        //offsets used to align the position of the edges with the actual edges of the hex
        const float xOffset = 0.226f;
        const float yOffset = -0.108f;

        //define dict for edges
        EdgeDict = new Dictionary<(Vector3Int, Vector3Int), GameObject>();
        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                //this should not require different code for vertices with different k values
                for(int k = 0; k < 1; k++)
                {
                    GameObject rootVertex;
                    if (VertexDict.TryGetValue(new Vector3Int(i, j, k), out rootVertex))
                    {
                        //this gets the vertex data script and runs the method Find Adjacent Vertices, returning the IDs of the vertices adjacent
                        List<Vector3Int> adjVertexIDList = rootVertex.GetComponent<VertexData>().FindAdjacentVertices();
                        //in a hex grid it will always have up to 3 adjencent vertices
                        for(int n = 0; n < 3; n++)
                        {
                            //if the edge does not already exist
                            if (getEdge(new Vector3Int(i,j,k), adjVertexIDList[n]) == null)
                            {
                                GameObject otherVertex;
                                if (VertexDict.TryGetValue(adjVertexIDList[n], out otherVertex))
                                {
                                    //finds the midpoint between the vertices
                                    float edgeX = (rootVertex.transform.position.x + otherVertex.transform.position.x) / 2;
                                    float edgeY = (rootVertex.transform.position.y + otherVertex.transform.position.y) / 2;
                                    Vector2 edgeCreatePosition = new Vector2(edgeX + xOffset, edgeY + yOffset);

                                    GameObject edgeInstance = (GameObject)Instantiate(_EdgePrefab, edgeCreatePosition, Quaternion.identity);
                                    edgeInstance.transform.SetParent(this.gameObject.transform);
                                    EdgeDict.Add((new Vector3Int(i, j, k), adjVertexIDList[n]), edgeInstance);
                                    edgeInstance.name = (new Vector3Int(i, j, k) + "_" + adjVertexIDList[n]);

                                    //set edge rotation
                                    GameObject edgeSprite = edgeInstance.GetComponentInChildren<SpriteRenderer>().gameObject;
                                    switch (n)
                                    {
                                        case 0:
                                            edgeSprite.transform.rotation = Quaternion.Euler(0,0,-60);
                                            break;
                                        case 2:
                                            edgeSprite.transform.rotation = Quaternion.Euler(0, 0, 60);
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                Debug.Log("edge creation skipped");
                            }
                        }
                    }
                } 
            }
        }
    }

    //gets edge from the dictonary, this ensures that no duplicates are made
    public GameObject getEdge(Vector3Int edge1ID, Vector3Int edge2ID)
    {
        GameObject edge;
        if(EdgeDict.TryGetValue((edge1ID, edge2ID), out edge))
        {
            return (edge);
        }
        else if(EdgeDict.TryGetValue((edge2ID, edge1ID), out edge))
        {
            return (edge);
        }
        else
        {
            return (null);
        }
    }

    public GameObject GetVertex(Vector3Int vertexID)
    {
        GameObject outputVertex;
        if (VertexDict.TryGetValue(vertexID, out outputVertex))
        {
            outputVertex = GameObject.Find("Vertex_" + vertexID.x + "_" + vertexID.y + "_" + vertexID.z);
            return (outputVertex);
        }
        else
        {
            return (null);
        }
    }
    private void assignResourcesAndNums()
    {
        Resources[,] hexResources = new Resources[gridWidth, gridHeight];
        int[,] hexNum = new int[gridWidth, gridHeight];

        //make a function to generate contents of arrays

        //this sets up the board as it appears in the rulebook
        if (premadeBoard == true)
        {
            hexResources[3, 1] = Resources.Ore;
            hexNum[3, 1] = 10;

            hexResources[4, 1] = Resources.Sheep;
            hexNum[4, 1] = 2;

            hexResources[5, 1] = Resources.Lumber;
            hexNum[5, 1] = 9;

            hexResources[2, 2] = Resources.Grain;
            hexNum[2, 2] = 12;

            hexResources[3, 2] = Resources.Brick;
            hexNum[3, 2] = 6;

            hexResources[4, 2] = Resources.Sheep;
            hexNum[4, 2] = 4;

            hexResources[5, 2] = Resources.Brick;
            hexNum[5, 2] = 10;

            hexResources[1, 3] = Resources.Grain;
            hexNum[1, 3] = 9;

            hexResources[2, 3] = Resources.Lumber;
            hexNum[2, 3] = 11;

            //desert hex
            hexResources[3, 3] = Resources.Nothing;
            hexNum[3, 3] = 0;

            hexResources[4, 3] = Resources.Lumber;
            hexNum[4, 3] = 3;

            hexResources[5, 3] = Resources.Ore;
            hexNum[5, 3] = 8;

            hexResources[1, 4] = Resources.Lumber;
            hexNum[1, 4] = 8;

            hexResources[2, 4] = Resources.Ore;
            hexNum[2, 4] = 3;

            hexResources[3, 4] = Resources.Grain;
            hexNum[3, 4] = 4;

            hexResources[4, 4] = Resources.Sheep;
            hexNum[4, 4] = 5;

            hexResources[1, 5] = Resources.Brick;
            hexNum[1, 5] = 5;

            hexResources[2, 5] = Resources.Grain;
            hexNum[2, 5] = 6;

            hexResources[3, 5] = Resources.Sheep;
            hexNum[3, 5] = 11;

        }
        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                if (HexGridArray[i,j] != null)
                {
                    HexGridArray[i, j].GetComponent<HexData>().AssignHexData(hexResources[i, j], hexNum[i, j]);
                }
            }
        }
    }
}
