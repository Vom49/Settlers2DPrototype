using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridControlScript : MonoBehaviour
{
    //prefab for the hex that it will draw
    [SerializeField] private GameObject _HexPrefab;
    [HideInInspector] public GameObject[,] HexGridArray;


    //width and height in hexes, nothing to do with unity coords
    int gridWidth = 5;
    int gridHeight = 5;

    float xOffset = 0.9f;
    float yOffset = 0.78f;

    // Start is called before the first frame update
    void Start()
    {
        //use loop to draw hexes
        //note this starts of the loop in the bottom left making the bottom left 0 0
        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                float xpos = i * xOffset;
                if ( j % 2 == 1)
                {
                    xpos += xOffset/ 2;
                }
                float ypos = j * yOffset;

                
                GameObject hexInstance = (GameObject)Instantiate(_HexPrefab, new Vector2(xpos,ypos), Quaternion.identity); //creates and saves the hex that was just created
                hexInstance.name = "Hex_" + i + "_" + j;
                HexGridArray[i, j] = hexInstance;
            }
        }

        //removes hexes that are not on the catan board
        Destroy(GameObject.Find("Hex_0_0"));
        Destroy(GameObject.Find("Hex_0_4"));
        Destroy(GameObject.Find("Hex_4_0"));
        Destroy(GameObject.Find("Hex_4_1"));
        Destroy(GameObject.Find("Hex_4_3"));
        Destroy(GameObject.Find("Hex_4_4"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
