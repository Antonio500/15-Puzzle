using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSound : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
    }

}
