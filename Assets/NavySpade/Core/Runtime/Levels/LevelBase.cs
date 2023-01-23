using Main.Levels.Data;
using UnityEngine;

namespace NS.Core.Levels
{
    public abstract class LevelBase : MonoBehaviour
    {
        public abstract void Init(LevelDataBase.AdditionData[] data);
    }
}