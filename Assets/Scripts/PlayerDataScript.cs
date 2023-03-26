using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerDataScript : MonoBehaviour
{
    //for the resource counters
    [SerializeField] TMP_Text _BrickCounter;
    [SerializeField] TMP_Text _LumberCounter;
    [SerializeField] TMP_Text _OreCounter;
    [SerializeField] TMP_Text _GrainCounter;
    [SerializeField] TMP_Text _SheepCounter;
    [SerializeField] TMP_Text _VPCounter;

    //lookup for the resource counts
    private Dictionary<Resources, int> ResourceDict = new Dictionary<Resources, int>();

    [SerializeField] TMP_Text _playerName;

    private void Start()
    {
        ResourceDict.Add(Resources.Brick, 0);
        ResourceDict.Add(Resources.Lumber, 0);
        ResourceDict.Add(Resources.Ore, 0);
        ResourceDict.Add(Resources.Grain, 0);
        ResourceDict.Add(Resources.Sheep, 0);
        ResourceDict.Add(Resources.VictoryPoints, 0);
    }
    public int FindResourceAmount(Resources tResource)
    {
        return (ResourceDict[tResource]);
    }

    //changes the amount and matches the UI accordingly
    public void EditResourceAmount(Resources tResource, int changeAmount)
    {
        ResourceDict[tResource] = ResourceDict[tResource] + changeAmount;
        switch (tResource)
        {
            case Resources.Brick:
                _BrickCounter.text = ResourceDict[tResource].ToString();
                break;
            case Resources.Lumber:
                _LumberCounter.text = ResourceDict[tResource].ToString();
                break;
            case Resources.Ore:
                _OreCounter.text = ResourceDict[tResource].ToString();
                break;
            case Resources.Grain:
                _GrainCounter.text = ResourceDict[tResource].ToString();
                break;
            case Resources.Sheep:
                _SheepCounter.text = ResourceDict[tResource].ToString();
                break;
            case Resources.VictoryPoints:
                _VPCounter.text = ResourceDict[tResource].ToString();
                break;
        }
    }

    public void VisualSetUp(string pName, Color pColor)
    {
        _playerName.text = pName;
        _playerName.color = pColor;
    }
}
