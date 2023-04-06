using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    [SerializeField] GameObject _PlayerPrefab;

    private GameObject[] playerObjs;
    private string[] playerNames;
    private Color[] playerColors;
    private int maxPlayers;

    private void Start()
    {
        maxPlayers = GameObject.Find("TurnController").GetComponent<TurnControllerScript>()._maxPlayers;
        playerObjs = new GameObject[maxPlayers + 1];
        playerNames = new string[maxPlayers + 1];
        playerColors = new Color[maxPlayers + 1];
        float yOffset = -1.6f;
        
        playerNames[1] = "Tim";
        playerNames[2] = "Jim";
        playerNames[3] = "Bim";
        playerNames[4] = "Lim";
        
        playerColors[1] = Color.red;
        playerColors[2] = Color.green;
        playerColors[3] = Color.blue;
        playerColors[4] = Color.yellow;

        //create the player objects
        for (int i = 1; i <= maxPlayers; i++)
        {
            GameObject playerInstance = (GameObject)Instantiate(_PlayerPrefab, transform.position ,Quaternion.identity);
            playerInstance.transform.SetParent(this.gameObject.transform);
            playerInstance.transform.position = new Vector2(transform.position.x, transform.position.y + (yOffset * (i-1)));
            playerInstance.name = ("Player_" + i);
            playerObjs[i] = playerInstance;

            //hand player it's name
            playerInstance.GetComponent<PlayerDataScript>().VisualSetUp(playerNames[i], playerColors[i]);
        }
    }
    public GameObject GetPlayerObj(int pNum)
    {
        GameObject playerObj = playerObjs[pNum];
        return (playerObj);
    }

    public Color GetPlayerColor(int pNum)
    {
        return (playerColors[pNum]);
    }

    public string GetPlayerName(int pNum)
    {
        return (playerNames[pNum]);
    }

    public int GetPlayerResource(int playerNum, Resources tResource)
    {
        GameObject p = GetPlayerObj(playerNum);
        return (p.GetComponent<PlayerDataScript>().FindResourceAmount(tResource));
    }

    public void EditPlayerResource(int playerNum, Resources tResource, int resourceAmount)
    {
        GameObject p = GetPlayerObj(playerNum);
        p.GetComponent<PlayerDataScript>().EditResourceAmount(tResource, resourceAmount);
    }
}
