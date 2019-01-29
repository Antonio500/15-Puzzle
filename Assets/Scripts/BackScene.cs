using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackScene : MonoBehaviour {
    public string loadSceneName;
    public GameObject[] arrayButton;
    public string[] arrayAnim;

    void Update()
    {
        // При нажатии Escape убираем кнопки и загружаем предыдущую сцену
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            int i = 0;
            foreach (GameObject button in arrayButton)
            {
                button.GetComponent<Animation>().Play(arrayAnim[i++]);
            }
            // Отложенный вызов (ждем окончание анимации)
            Invoke("Load", 0.5f);
        }
    }
    void Load()
    {
        SceneManager.LoadScene(loadSceneName, LoadSceneMode.Single);
    }
}