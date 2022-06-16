using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace Xami.Data
{
    public class JsonScoreProvider : IScoreProvider
    {
        private string filePath;

        public JsonScoreProvider(string path)
        {
            filePath = Path.Combine(Application.persistentDataPath, path);
        }

        public Score Load()
        {
            if (!File.Exists(filePath))
            {
                return new Score();
            }

            var json = File.ReadAllText(filePath);
            var score = JsonUtility.FromJson<Score>(json);
            return score;
        }

        public void Save(Score score)
        {
            var stateJson = JsonUtility.ToJson(score);
            File.WriteAllText(filePath, stateJson);
        }
    }
}
