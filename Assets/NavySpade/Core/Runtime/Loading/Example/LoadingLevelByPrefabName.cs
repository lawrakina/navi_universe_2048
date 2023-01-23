using System.Collections;
using Core.Loading.Operations;
using Main.Levels;
using Main.Levels.Data;
using NS.Core.Levels;

namespace Core.Loading.Example
{
    //TODO: Using old levelManager
    
    public class LoadingLevelByPrefabName : IAsyncOperation
    {
        private const string ASSET_DIR = "Levels/"; 
        
        private ResourceNameLevelData _levelData;
        private LevelManager _levelManager;
        
        public LoadingLevelByPrefabName(LevelManager levelManager, ResourceNameLevelData levelData)
        {
            _levelManager = levelManager;
            _levelData = levelData;
        }
        
        public IEnumerator Load()
        {
            _levelManager.DestroyLevels();
            
            var request = new LoadResourceAsync<LevelBase>(ASSET_DIR + _levelData.PrefabName);
            yield return request.Load();
            
            LevelBase level = request.Asset;
            level.Init(_levelData.AdditionsData);
            _levelManager.SetLevel(level);
        }
    }
}