using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xami.Data
{
    public interface IScoreProvider
    {
        Score Load();

        void Save(Score score);
    }
}
