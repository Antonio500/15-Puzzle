using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string loadSceneName;
    public GameObject[] arrayButton;
    public string[] arrayAnim;

    public void onButtonClick()
    {
        // При нажатии button убираем кнопки и загружаем сцену
        int i = 0;
        foreach (GameObject button in arrayButton)
        {
            button.GetComponent<Animation>().Play(arrayAnim[i++]);
        }
        // Отложенный вызов (ждем окончание анимации)
        Invoke("Load", 0.5f);
    }
    void Load()
    {
        SceneManager.LoadScene(loadSceneName, LoadSceneMode.Single);
    }
}

