using UnityEngine;

namespace Main.Levels.Data
{
    [CreateAssetMenu(fileName = "level", menuName = "Game/Level/Scene", order = 51)]
    public class SceneLevelData : LevelDataBase
    {
        [NaughtyAttributes.Scene]
        public int BuildIndex;
    }
}