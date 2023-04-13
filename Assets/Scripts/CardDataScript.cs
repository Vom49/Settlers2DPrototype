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
    public Image buttonImage;
    public Sprite blankCard;
    private void Update()
    {
        
        switch (cardID)
        {
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
            default:
                break;
        }
    }
    
}
