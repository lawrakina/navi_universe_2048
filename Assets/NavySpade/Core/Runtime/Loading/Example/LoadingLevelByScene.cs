using System.Collections;
using Core.Extensions;
using Core.Loading;
using Core.Loading.Operations;
using Main.Levels.Data;
using NavySpade.Core.Runtime.Levels;
using NS.Core.Levels;
using UnityEngine.SceneManagement;

namespace Pj_61_Weapon_adv.Common.Loading
{
    public class LoadingLevelByScene : IAsyncOperation
    {
        private const string LEVEL_NAME = "Level";
        
        private SceneLevelData _levelData;
        private LevelsManager _levelsManager;
        private bool _isFirstLoad;
        
        public LoadingLevelByScene(LevelsManager levelsManager, SceneLevelData levelData, bool isFirstLoad)
        {
            _levelData = levelData;
            _isFirstLoad = isFirstLoad;
            _levelsManager = levelsManager;
        }
        
        public IEnumerator Load()
        {
            Scene levelScene;
            bool needLoad = true;
            if (SceneExtensions.HasSceneBeginningWith(LEVEL_NAME, out levelScene))
            {
                if (_isFirstLoad == false)
                {
                    yield return SceneManager.UnloadSceneAsync(levelScene);
                }
                else
                {
                    needLoad = false;
                }
            }

            if (needLoad)
            {
                var loadingSceneOperation = new LoadingScene(_levelData.BuildIndex);
                yield return loadingSceneOperation.Load();
                levelScene = loadingSceneOperation.LoadedScene;
            }

            SceneManager.SetActiveScene(levelScene);
            
            _levelsManager.CurrentSceneName = levelScene.name;
            LevelBase level = levelScene.GetRoot<LevelBase>();
            level.Init(_levelData.AdditionsData);
        }
    }
}