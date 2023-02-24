using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridControlScript : MonoBehaviour
{
    //prefab for the hex that it will draw
    [SerializeField] private GameObject _HexPrefab;
    [SerializeField] private GameObject _VertexPrefab;
    [HideInInspector] public GameObject[,] HexGridArray;

    float xOffset = 0.457f;
    float yOffset = 0.795f;

    //width and height in hexes, nothing to do with unity coords
    int gridWidth = 7;
    int gridHeight = 7;


    // Start is called before the first frame update
    void Start()
    {
        drawGridAxial();
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
                hexInstance.transform.SetParent(this.gameObject.transform);
                hexInstance.name = "Hex_" + i + "_" + j;
                HexGridArray[i, j] = hexInstance;
                drawVertices(hexInstance, i, j);
            }
        }

        
        //removes hexes that are not on the catan board
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

    //draws the vertices of the hex that is inputted, used inside the draw grid loop
    private void drawVertices(GameObject targetHex, int hexX, int hexY)
    {
        GameObject vertexInstance1 = (GameObject)Instantiate(_VertexPrefab, new Vector2(targetHex.transform.position.x, targetHex.transform.position.y + 0.5f), Quaternion.identity);
        vertexInstance1.name = "Vertex_" + hexX + "_" + hexY + "_1"; //north vertex
        GameObject vertexInstance2 = (GameObject)Instantiate(_VertexPrefab, new Vector2(targetHex.transform.position.x + 0.455f, targetHex.transform.position.y + 0.28f), Quaternion.identity);
        vertexInstance1.name = "Vertex_" + hexX + "_" + hexY + "_2"; //north east vertex
    }
}
