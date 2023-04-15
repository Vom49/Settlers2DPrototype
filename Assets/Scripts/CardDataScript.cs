using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDataScript : MonoBehaviour
{
    //card ID
    //Assign Sprite based on card ID
    //on click, have card perform action based on card ID

    public int cardID = 0;
    [SerializeField] private Image buttonImage;

    public Sprite blankCard;
    public Sprite victoryCard;
    public Sprite knightCard;
    public Sprite roadroadCard;
    public Sprite resourceCard;
    public Sprite monopolyCard;


    private void Update()
    {
        
        switch (cardID)
        {
            case 1:
                buttonImage.sprite = victoryCard;
                break;
            case 2:
                buttonImage.sprite = knightCard;
                break;
            case 3:
                buttonImage.sprite = roadroadCard;
                break;
            case 4:
                buttonImage.sprite = resourceCard;
                break;
            case 5:
                buttonImage.sprite = monopolyCard;
                break;
            default:
                buttonImage.sprite = blankCard;
                break;
        }
    }

    /*
     * 0 - nothing
     * 1 - victory point
     * 2 - knight
     * 3 - build 2 free roads
     * 4 - 2 resources
     * 5 - get one type of resource form other players
     */
    public void onClick()
    {
        switch (cardID)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            default:
                break;
        }
    }


    public void setID(int ID)
    {
        cardID = ID;
    }
    
}
