using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridControlScript : MonoBehaviour
{
    //prefab for the hex that it will draw
    [SerializeField] private GameObject _HexPrefab;
    //prefab for the vertex it will draw
    [SerializeField] private GameObject _VertexPrefab;

    //main storage and access to hexes
    [HideInInspector] public GameObject[,] HexGridArray;

    //uses vector3int as key to hold X Y and K coords of the Vertexes
    [HideInInspector] public Dictionary<Vector3Int, GameObject> VertexDict;

    const float xOffset = 0.457f;
    const float yOffset = 0.795f;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //draws the grid using an Axial method of storing the grid
    //Axial hex grid means that a column cascades down to the right
    private void drawGridAxial()
    {
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

                GameObject vertexInstance2 = (GameObject)Instantiate(_VertexPrefab, new Vector2(targetHex.transform.position.x + neVertexXOffset, targetHex.transform.position.y + neVertexYOffset), Quaternion.identity);
                vertexInstance2.name = "Vertex_" + hexX + "_" + hexY + "_1"; //north east vertex
                vertexInstance2.transform.SetParent(this.gameObject.transform); //for readability in editor
                VertexDict.Add(new Vector3Int(hexX, hexY, 1), vertexInstance1);
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
    
}
