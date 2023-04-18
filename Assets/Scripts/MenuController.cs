using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{
    private int MaxPlayers = 4;
    [SerializeField] private Toggle PlayerNumToggle;

    [SerializeField] private GameObject MainMenuPanel;
    [SerializeField] private GameObject SetupSubMenuPanel;

    //Player1
    [SerializeField] private TMP_InputField playerNameField1;
    [SerializeField] private SpriteRenderer colorBox1;
    [SerializeField] private Slider redSlider1;
    [SerializeField] private Slider greenSlider1;
    [SerializeField] private Slider blueSlider1;
    [SerializeField] private Toggle isAI1;
    private Color playerColor1 = Color.red;
    private int pAI1 = 0;

    //Player2
    [SerializeField] private TMP_InputField playerNameField2;
    [SerializeField] private SpriteRenderer colorBox2;
    [SerializeField] private Slider redSlider2;
    [SerializeField] private Slider greenSlider2;
    [SerializeField] private Slider blueSlider2;
    [SerializeField] private Toggle isAI2;
    private Color playerColor2 = Color.green;
    private int pAI2 = 0;

    //Player3
    [SerializeField] private TMP_InputField playerNameField3;
    [SerializeField] private SpriteRenderer colorBox3;
    [SerializeField] private Slider redSlider3;
    [SerializeField] private Slider greenSlider3;
    [SerializeField] private Slider blueSlider3;
    [SerializeField] private Toggle isAI3;
    private Color playerColor3 = Color.blue;
    private int pAI3 = 0;

    //Player4
    [SerializeField] private TMP_InputField playerNameField4;
    [SerializeField] private SpriteRenderer colorBox4;
    [SerializeField] private Slider redSlider4;
    [SerializeField] private Slider greenSlider4;
    [SerializeField] private Slider blueSlider4;
    [SerializeField] private Toggle isAI4;
    private Color playerColor4 = Color.yellow;
    private int pAI4 = 0;
    [SerializeField] private GameObject Player4Panel;

    public void ShowSetupMenu()
    {
        MainMenuPanel.SetActive(false);
        SetupSubMenuPanel.SetActive(true);
    }

    //Player1
    public void updateColorBox1()
    {
        playerColor1 = new Color(redSlider1.value / 256, greenSlider1.value / 256, blueSlider1.value / 256);
        colorBox1.color = playerColor1;
    }

    public void isPlayerAI1()
    {
        if(isAI1.isOn)
        {
            pAI1 = 1;
        }
        else
        {
            pAI1 = 0;
        }
    }

    

    //Player2
    public void updateColorBox2()
    {
        playerColor2 = new Color(redSlider2.value / 256, greenSlider2.value / 256, blueSlider2.value / 256);
        colorBox2.color = playerColor2;
    }

    public void isPlayerAI2()
    {
        if (isAI2.isOn)
        {
            pAI2 = 1;
        }
        else
        {
            pAI2 = 0;
        }
    }

    //Player3
    public void updateColorBox3()
    {
        playerColor3 = new Color(redSlider3.value / 256, greenSlider3.value / 256, blueSlider3.value / 256);
        colorBox3.color = playerColor3;
    }

    public void isPlayerAI3()
    {
        if (isAI3.isOn)
        {
            pAI3 = 1;
        }
        else
        {
            pAI3 = 0;
        }
    }

    //Player4
    public void updateColorBox4()
    {
        playerColor4 = new Color(redSlider4.value / 256, greenSlider4.value / 256, blueSlider4.value / 256);
        colorBox4.color = playerColor4;
    }

    public void isPlayerAI4()
    {
        if (isAI3.isOn)
        {
            pAI3 = 1;
        }
        else
        {
            pAI3 = 0;
        }
    }

    public void toggle4Players()
    {
        if (PlayerNumToggle.isOn)
        {
            MaxPlayers = 4;
            Player4Panel.SetActive(true);
        }
        else
        {
            MaxPlayers = 3;
            Player4Panel.SetActive(false);
        }
    }

    public void PlayGame()
    {
        PlayerPrefs.SetInt("MaxPlayers", MaxPlayers);

        PlayerPrefs.SetInt("PlayerColor1R", (int)redSlider1.value);
        PlayerPrefs.SetInt("PlayerColor1G", (int)redSlider1.value);
        PlayerPrefs.SetInt("PlayerColor1B", (int)redSlider1.value);
        PlayerPrefs.SetString("PlayerName1", playerNameField1.text);
        PlayerPrefs.SetInt("PlayerAI1", pAI1);

        PlayerPrefs.SetInt("PlayerColor2R", (int)redSlider2.value);
        PlayerPrefs.SetInt("PlayerColor2G", (int)redSlider2.value);
        PlayerPrefs.SetInt("PlayerColor2B", (int)redSlider2.value);
        PlayerPrefs.SetString("PlayerName2", playerNameField2.text);
        PlayerPrefs.SetInt("PlayerAI2", pAI2);

        PlayerPrefs.SetInt("PlayerColor3R", (int)redSlider3.value);
        PlayerPrefs.SetInt("PlayerColor3G", (int)redSlider3.value);
        PlayerPrefs.SetInt("PlayerColor3B", (int)redSlider3.value);
        PlayerPrefs.SetString("PlayerName3", playerNameField3.text);
        PlayerPrefs.SetInt("PlayerAI3", pAI3);

        if (MaxPlayers == 4)
        {
            PlayerPrefs.SetInt("PlayerColor4R", (int)redSlider4.value);
            PlayerPrefs.SetInt("PlayerColor4G", (int)redSlider4.value);
            PlayerPrefs.SetInt("PlayerColor4B", (int)redSlider4.value);
            PlayerPrefs.SetString("PlayerName4", playerNameField4.text);
            PlayerPrefs.SetInt("PlayerAI4", pAI4);
        }
    }
}
