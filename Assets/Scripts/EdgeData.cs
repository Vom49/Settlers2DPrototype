using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeData : MonoBehaviour
{
    public int ownerPlayer = 0;
    public bool roadBuilt = false;
    public Vector3 vertex1;
    public Vector3 vertex2;


    private void Update()
    {
        EnableDisableButton();
    }
    public void ClickBuildButton()
    {

    }

    private void EnableDisableButton()
    {

    }
}
