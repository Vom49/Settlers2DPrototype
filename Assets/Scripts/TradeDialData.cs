using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeDialData : MonoBehaviour
{
    [SerializeField] private Resources DialResource;
    private int DialValue = 0;

    public Resources GetDialResource()
    {
        return (DialResource);
    }

    public int GetDialValue()
    {
        return (DialValue);
    }

    public void UptickValue()
    {
        DialValue++;
    }
    public void DowntickValue()
    {
        DialValue--;
    }

    public void ResetValue()
    {
        DialValue = 0;
    }
}
