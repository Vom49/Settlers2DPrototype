using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerDataScript : MonoBehaviour
{
    public string playerName;
    //for the resource counters
    [SerializeField] TMP_Text _BrickCounter;
    [SerializeField] TMP_Text _LumberCounter;
    [SerializeField] TMP_Text _OreCounter;
    [SerializeField] TMP_Text _GrainCounter;
    [SerializeField] TMP_Text _SheepCounter;

    //lookup for the resource counts
    private Dictionary<Resources, int> ResourceDict = new Dictionary<Resources, int>();

    private void Start()
    {
        ResourceDict.Add(Resources.Brick, 0);
        ResourceDict.Add(Resources.Lumber, 0);
        ResourceDict.Add(Resources.Ore, 0);
        ResourceDict.Add(Resources.Grain, 0);
        ResourceDict.Add(Resources.Sheep, 0);
    }
    public int FindResourceAmount(Resources tResource)
    {
        return (ResourceDict[tResource]);
    }

    //changes the amount and matches the UI accordingly
    public void EditResourceAmount(Resources tResource, int changeAmount)
    {
        ResourceDict[tResource] = ResourceDict[tResource] + changeAmount;
    }
}
