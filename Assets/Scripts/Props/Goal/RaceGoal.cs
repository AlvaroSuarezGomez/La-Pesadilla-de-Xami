using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Xami.Data;

public class RaceGoal : MonoBehaviour
{
    public bool activated;

    [SerializeField] private GameObject recordMessage;

    [SerializeField] private float messageWait;

    private void Awake()
    {
        Timer.Instance.ResetTimer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Vehicle") && activated)
        {
            if ((ScoreManager.Instance.Score.scores.Count <= SceneManager.GetActiveScene().buildIndex) || (Timer.Instance.LapTime < ScoreManager.Instance.Score.scores[SceneManager.GetActiveScene().buildIndex]))
            {
                Timer.Instance.SaveTime();
                StartCoroutine(ShowMessage());
            }
            Timer.Instance.ResetTimer();
            activated = false;
        }
    }

    private IEnumerator ShowMessage()
    {
        for (int i = 0; i < 5; i++)
        {
            recordMessage.SetActive(true);
            yield return new WaitForSeconds(messageWait);
            recordMessage.SetActive(false);
            yield return new WaitForSeconds(messageWait);
        }
    }
}
