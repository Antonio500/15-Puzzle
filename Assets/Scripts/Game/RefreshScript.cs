using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshScript : MonoBehaviour
{
    public GameObject ObjectGameControl;

    void OnMouseUpAsButton()
    {
        ObjectGameControl.GetComponent<GameControl>().StartNewGame();
        ObjectGameControl.GetComponent<TextScript>().Start();
    }
}