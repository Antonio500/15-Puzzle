using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSize : MonoBehaviour {
    void OnMouseDown()
    {
        transform.localScale = new Vector3(1.05f, 1.05f, 1.05f);
    }
    void OnMouseUp()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

}
