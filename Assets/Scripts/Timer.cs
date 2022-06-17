using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Xami.Data;

public class Timer : MonoBehaviour
{
    public static Timer Instance { get; private set; }

    private float time;

    public float LapTime => time;


    public TextMeshProUGUI text;

    private int levelIndex;

    private bool activated;

    public bool Activated { get { return activated; } set { activated = value; } }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            activated = true;
        } else
        {
            Instance = this;
        }

        DontDestroyOnLoad(Instance);
        //Debug.Log(Instance.time + " " + Instance.activated);
    }

    void Start()
    {
        if (!activated)
        {
            time = 0;
            activated = true;
        }
        levelIndex = SceneManager.GetActiveScene().buildIndex;
    }
    void Update()
    {
        if (activated)
        {
            time += Time.deltaTime;
        }
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
