using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TradeDialData : MonoBehaviour
{
    [SerializeField] private Resources DialResource;
    private int DialValue = 0;
    [SerializeField] private TMP_Text resourceCounterText;

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
        UpdateDialValue();
    }
    public void DowntickValue()
    {
        DialValue--;
        UpdateDialValue();
    }

    public void ResetValue()
    {
        DialValue = 0;
        UpdateDialValue();
    }

    private void UpdateDialValue()
    {
        resourceCounterText.text = DialValue.ToString();
    }
}
