using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    [SerializeField] GameObject _PlayerPrefab;

    private GameObject[] playerObjs;
    private string[] playerNames;
    private Color[] playerColors;
    private bool[] playerIsAI;
    public int maxPlayers;

    private void Start()
    {
        float yOffset = -1.6f;

        maxPlayers = PlayerPrefs.GetInt("MaxPlayers");


        playerObjs = new GameObject[maxPlayers + 1];
        playerNames = new string[maxPlayers + 1];
        playerColors = new Color[maxPlayers + 1];
        playerIsAI = new bool[maxPlayers + 1];

        playerNames[1] = PlayerPrefs.GetString("PlayerName1");
        playerColors[1] = new Color(PlayerPrefs.GetInt("PlayerColor1R"), PlayerPrefs.GetInt("PlayerColor1G"), PlayerPrefs.GetInt("PlayerColor1B"));
        if (PlayerPrefs.GetInt("PlayerAI1") == 1) { playerIsAI[1] = true; } else { playerIsAI[1] = false; }

        playerNames[2] = PlayerPrefs.GetString("PlayerName2");
        playerColors[2] = new Color(PlayerPrefs.GetInt("PlayerColor2R"), PlayerPrefs.GetInt("PlayerColor2G"), PlayerPrefs.GetInt("PlayerColor2B"));
        if (PlayerPrefs.GetInt("PlayerAI2") == 1) { playerIsAI[2] = true; } else { playerIsAI[2] = false; }

        playerNames[3] = PlayerPrefs.GetString("PlayerName3");
        playerColors[3] = new Color(PlayerPrefs.GetInt("PlayerColor3R"), PlayerPrefs.GetInt("PlayerColor3G"), PlayerPrefs.GetInt("PlayerColor3B"));
        if (PlayerPrefs.GetInt("PlayerAI3") == 1) { playerIsAI[3] = true; } else { playerIsAI[3] = false; }

        if (maxPlayers == 4)
        {
            playerNames[4] = PlayerPrefs.GetString("PlayerName4");
            playerColors[4] = new Color(PlayerPrefs.GetInt("PlayerColor4R"), PlayerPrefs.GetInt("PlayerColor4G"), PlayerPrefs.GetInt("PlayerColor4B"));
            if (PlayerPrefs.GetInt("PlayerAI4") == 1) { playerIsAI[4] = true; } else { playerIsAI[4] = false; }
        }
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

    public int GetMaxPlayers()
    {
        return (maxPlayers);
    }
}
