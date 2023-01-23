using System;
using Main.Levels.Configuration;
using Main.Levels.Data;
using NavySpade.Core.Runtime.Game;
using NavySpade.Modules.Configuration.Runtime.SO;
using NavySpade.Modules.Saving.Runtime;
using Pj_61_Weapon_adv.Common.Loading;
using UnityEngine;


namespace NavySpade.Core.Runtime.Levels
{
    public class LevelsManager : MonoBehaviour
    {
        private const string SAVE_KEY = "levelIndex";
        
        private LevelsConfig _levelsConfig;
        private bool _isFirstLoad;
        
        public void Init()
        {
            _levelsConfig = ObjectConfig.GetConfig<LevelsConfig>();
            _isFirstLoad = true;
        }
        
        private int? _levelIndex;

        public int LevelIndex
        {
            get
            {
                if (_levelIndex == null)
                {
                    _levelIndex = SaveManager.Load<int>(SAVE_KEY);
                }

                return (int)_levelIndex;
            }
            set
            {
                _levelIndex = value;
                LevelIndexChanged?.Invoke(value);
                SaveManager.Save(SAVE_KEY, value);
            }
        }
        
        public string CurrentSceneName { get; set; }
        
        public event Action<int> LevelIndexChanged;

        public void LoadLevel()
        {
            LevelDataBase levelDataBase  = _levelsConfig.Main[LevelIndex];
            
            ProjectContext.Instance.SplashScreen.Execute(new LoadingLevelByScene(
                this, 
                levelDataBase as SceneLevelData,
                _isFirstLoad));
            
            _isFirstLoad = false;
        }
        
        public void UnlockNextLevel()
        {
            var levelIndex = LevelIndex + 1;
            levelIndex = (int) Mathf.Repeat(levelIndex, _levelsConfig.Main.Length);
            LevelIndex = levelIndex;
        }
    }
}