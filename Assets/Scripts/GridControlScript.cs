using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridControlScript : MonoBehaviour
{
    //prefab for the hex that it will draw
    [SerializeField] private GameObject _HexPrefab;
    [HideInInspector] public GameObject[,] HexGridArray;


    

    // Start is called before the first frame update
    void Start()
    {
        drawGridAxial();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void drawGridAxial()
    {
        //width and height in hexes, nothing to do with unity coords
        int gridWidth = 5;
        int gridHeight = 5;

        float xOffset = 0.457f;
        float yOffset = 0.795f;
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
            }
        }
        //removes hexes that are not on the catan board

        Destroy(GameObject.Find("Hex_0_0"));
        HexGridArray[0, 0] = null;
        Destroy(GameObject.Find("Hex_0_1"));
        HexGridArray[0, 1] = null;
        Destroy(GameObject.Find("Hex_1_0"));
        HexGridArray[1, 0] = null;
        Destroy(GameObject.Find("Hex_3_4"));
        HexGridArray[3, 4] = null;
        Destroy(GameObject.Find("Hex_4_3"));
        HexGridArray[4, 3] = null;
        Destroy(GameObject.Find("Hex_4_4"));
        HexGridArray[4, 4] = null;
    }
}
