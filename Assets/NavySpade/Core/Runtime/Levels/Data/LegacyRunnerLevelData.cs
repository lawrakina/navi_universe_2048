using NavySpade.Core.Runtime.Player.Configuration;
using UnityEngine;

namespace Main.Levels.Data
{
    [CreateAssetMenu(fileName = "new Level", menuName = "Game/Level/Legacy Runner", order = 51)]
    public class LegacyRunnerLevelData : LevelDataBase
    {
        public float distance = 50f;
        
        public PlayerParameters player = new PlayerParameters();

        [Header("Игровые элементы")] 
        public LevelElementsData LevelElements = new LevelElementsData();

        [Header("Visual")] 
        public GameViewParameters view = new GameViewParameters();
    }
}