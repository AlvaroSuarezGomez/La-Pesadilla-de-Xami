using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Xami.Data;

public class Timer : MonoBehaviour
{
    private float time;

    [SerializeField] 
    private TextMeshProUGUI text;

    private int levelIndex;

    void Start()
    {
        time = 0;
        levelIndex = SceneManager.GetActiveScene().buildIndex;
    }
    void Update()
    {
        time += Time.deltaTime;
        string mins = ((int)time / 60).ToString("00");
        string segs = (time % 60).ToString("00");
        string milisegs = ((time * 100) % 100).ToString("00");

        string TimerString = string.Format("{00}:{01}:{02}", mins, segs, milisegs);

        text.text = TimerString.ToString();
    }

    public void ResetTimer()
    {
        time = 0;
    }

    public void SaveTime()
    {
        ScoreManager.Instance.RecordScore(time, levelIndex);
    }
}
