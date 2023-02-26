using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum Resources
{ 
    Nothing,
    Brick,
    Lumber,
    Ore,
    Grain,
    Sheep
}

public class HexData : MonoBehaviour
{
    public int myX;
    public int myY;
    public Resources ProducedResource = Resources.Nothing;
    public int outputNumber = 0;

    [SerializeField] private TMP_Text outputNumText;
    [SerializeField] private SpriteRenderer hexSpriteRenderer;

    private void Start()
    {
    }

    public void AssignHexData(Resources desiredResource, int desiredOutputNum)
    {
        ProducedResource = desiredResource;
        outputNumber = desiredOutputNum;
        
        //hide the number on the desert
        if (desiredOutputNum == 0)
        {
            outputNumText.text = "";
        }
        else
        {
            string outputString = outputNumber.ToString();
            outputNumText.text = outputString;
        }
        
        
        switch (desiredResource)
        {
            case Resources.Brick:
                hexSpriteRenderer.color = new Color(200 / 255f, 90 / 255f, 60 / 255f);
                break;
            case Resources.Lumber:
                hexSpriteRenderer.color = new Color(30 / 255f, 110 / 255f, 15 / 255f);
                break;
            case Resources.Ore:
                hexSpriteRenderer.color = new Color(162 / 255f, 190 / 255f, 190 / 255f);
                break;
            case Resources.Grain:
                hexSpriteRenderer.color = new Color(225 / 255f, 215 / 255f, 25 / 255f);
                break;
            case Resources.Sheep:
                hexSpriteRenderer.color = new Color(110 / 255f, 200 / 255f, 45 / 255f);
                break;
            default:
                hexSpriteRenderer.color = new Color(235 / 255f, 230 / 255f, 160 / 255f);
                break;
        }
        
    }
}
