using UnityEngine;
using UnityEngine.UI;
using System;

public class TextScript : MonoBehaviour
{
    public GameObject ObjectGameControl;
    public Text TimeText;
    public Text CountClickText;
    public Text WinText;

    private DateTime StartTime;
    private TimeSpan TimerTime;
    private bool StartFlag;

    public GameObject WinAudio;
    private bool SoundFlag = true;

    public void Start()
    {
        CountClickText.text = "0";
        TimeText.text = "0:00";
        WinText.text = "";
        StartFlag = true;
        SoundFlag = true;
    }

    void Update()
    {
        if (ObjectGameControl.GetComponent<GameControl>().WinFlag)
        {
            if (SoundFlag)
            {
                WinText.text = "Вы выйграли!!!";
                WinAudio.GetComponent<AudioSource>().Play();
                SoundFlag = false;
                // Подгружаем текущие значения
                CountClickText.text = ObjectGameControl.GetComponent<GameControl>().countClick.ToString();
                if (TimerTime.Seconds < 10)
                    TimeText.text = TimerTime.Minutes.ToString() + ":0" + TimerTime.Seconds.ToString();
                else
                    TimeText.text = TimerTime.Minutes.ToString() + ":" + TimerTime.Seconds.ToString();
            }
            return;
        }

        if (ObjectGameControl.GetComponent<GameControl>().countClick == 0) return;
        if (ObjectGameControl.GetComponent<GameControl>().countClick == 1 && StartFlag)
        {
            StartTime = DateTime.Now;
            StartFlag = false;
        }
        TimerTime = DateTime.Now - StartTime;
        // Подгружаем текущие значения
        CountClickText.text = ObjectGameControl.GetComponent<GameControl>().countClick.ToString();
        if (TimerTime.Seconds < 10)
            TimeText.text = TimerTime.Minutes.ToString() + ":0" + TimerTime.Seconds.ToString();
        else
            TimeText.text = TimerTime.Minutes.ToString() + ":" + TimerTime.Seconds.ToString();
    }
}
