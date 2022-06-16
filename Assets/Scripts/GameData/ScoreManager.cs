using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xami.Data {
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance { get; private set; }

        private Score score;

        private IScoreProvider scoreProvider;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            scoreProvider = new JsonScoreProvider("scores.json");
            score = scoreProvider.Load();
        }

        public void RecordScore(float time, int index)
        {
            if (score.scores.Count > index)
            {
                score.scores[index] = time;
            } else
            {
                while (score.scores.Count <= index)
                {
                    score.scores.Add(0f);
                }
                score.scores[index] = time;
            }
                scoreProvider.Save(score);

                for (int i = 0; i < score.scores.Count; i++)
                {
                    float scoreTime = score.scores[i];
                    string mins = ((int)scoreTime / 60).ToString("00");
                    string segs = (scoreTime % 60).ToString("00");
                    string milisegs = ((scoreTime * 100) % 100).ToString("00");

                    string timerString = string.Format("{00}:{01}:{02}", mins, segs, milisegs);

                    Debug.Log(timerString);
                }
            }
        }
    }
