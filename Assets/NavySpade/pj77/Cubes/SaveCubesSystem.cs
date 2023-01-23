using System.Collections.Generic;
using Leopotam.Ecs;
using NavySpade.Core.Runtime.Levels;
using NavySpade.Modules.Saving.Runtime;


namespace NavySpade.pj77.Cubes{
    internal class SaveCubesSystem : IEcsDestroySystem{
        private CubesSettings _cubesSettings;
        private LevelsManager _levelsManager;

        private EcsFilter<CubeComponent> _cubes;

        public void Destroy(){
            var list = new List<int>();
            foreach (var i in _cubes){
                ref var cube = ref _cubes.Get1(i);
                if (cube.Value.CubeValue >= _cubesSettings.GetCube(_cubesSettings.MaxRandomValue).Value){
                    list.Add(cube.Value.CubeValue);
                }
            }

            SaveManager.Save<SaveList>($"Level_{_levelsManager.LevelIndex};cubes", new SaveList(list));
        }
    }
}