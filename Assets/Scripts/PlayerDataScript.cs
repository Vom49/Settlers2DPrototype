using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataScript : MonoBehaviour
{
    public string playerName;
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

    public void EditResourceAmount(Resources tResource, int changeAmount)
    {
        ResourceDict[tResource] = ResourceDict[tResource] + changeAmount;
    }
}
