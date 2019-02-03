using UnityEngine;
using UnityEngine.UI;
using System;

public class TextScript : MonoBehaviour
{
    public GameObject objectGameControl;
    public Text timeText;
    public Text countClickText;
    public Text winText;
    public GameObject winTextObj;

    private DateTime startTime;
    private TimeSpan timerTime;
    private bool startFlag;

    public GameObject winAudio;
    private bool soundFlag = true;

    public void Start()
    {
        countClickText.text = "0";
        timeText.text = "0:00";
        winText.text = "";
        winTextObj.SetActive(false);
        startFlag = true;
        soundFlag = true;
    }

    void Update()
    {
        if (objectGameControl.GetComponent<GameControl>().WinFlag)
        {
            if (soundFlag)
            {
                winText.text = "Победа!!!";
                winTextObj.SetActive(true);
                winAudio.GetComponent<AudioSource>().Play();
                soundFlag = false;
                // Подгружаем последние значения
                countClickText.text = objectGameControl.GetComponent<GameControl>().countClick.ToString();
                if (timerTime.Seconds < 10)
                    timeText.text = timerTime.Minutes.ToString() + ":0" + timerTime.Seconds.ToString();
                else
                    timeText.text = timerTime.Minutes.ToString() + ":" + timerTime.Seconds.ToString();
            }
            return;
        }

        if (objectGameControl.GetComponent<GameControl>().countClick == 0) return;
        if (objectGameControl.GetComponent<GameControl>().countClick == 1 && startFlag)
        {
            startTime = DateTime.Now;
            startFlag = false;
        }
        timerTime = DateTime.Now - startTime;
        // Подгружаем текущие значения
        countClickText.text = objectGameControl.GetComponent<GameControl>().countClick.ToString();
        if (timerTime.Seconds < 10)
            timeText.text = timerTime.Minutes.ToString() + ":0" + timerTime.Seconds.ToString();
        else
            timeText.text = timerTime.Minutes.ToString() + ":" + timerTime.Seconds.ToString();
    }
}
