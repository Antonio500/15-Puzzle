using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshScript : MonoBehaviour
{
    public GameObject objectGameControl;

    public void OnClick()
    {
        objectGameControl.GetComponent<GameControl>().StartNewGame();
        objectGameControl.GetComponent<TextScript>().Start();
    }
}