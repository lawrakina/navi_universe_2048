using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Leopotam.Ecs;
using NavySpade.Core.Runtime.Levels;
using NavySpade.Modules.Pooling.Runtime;
using NavySpade.Modules.Saving.Runtime;
using NavySpade.pj77.Transporter;
using NavySpade.pj77.Transporter.Points;
using UnityEngine;


namespace NavySpade.pj77.Cubes{
    internal class SpawnCubesSystem : IEcsInitSystem, IEcsRunSystem{
        private CubesSettings _cubesSettings;
        private LevelsManager _levelsManager;
        
        // private ObjectPool<CubeView> _pool;
        

        private EcsFilter<PointTransport> _pointTransports;


        #region PrivateData

        private List<int> _savedCubes;

        #endregion


        public async void Init(){
            _savedCubes = SaveManager.Load<SaveList>($"Level_{_levelsManager.LevelIndex};cubes").list;
        }

        public void Run(){
            foreach (var i in _pointTransports){
                ref var entity = ref _pointTransports.GetEntity(i);
                ref var point = ref _pointTransports.Get1(i);
                if (point.IsFirst){
                    if (point.Body.transform.childCount == 0){
                        // var cube = SpawnRandomCube(_cubesSettings);
                        var cube = (CheckSavedCube(out CubeInfo cubeinfo).Value != 0)
                            ? SpawnCube(cubeinfo)
                            : SpawnRandomCube(_cubesSettings);
                        //settings for first cube (tutor)
                        if (entity.Has<IsFirstCube>()){
                            cube.SetValue(_cubesSettings.GetCubeByValue(entity.Get<IsFirstCube>().Value));
                            entity.Del<IsFirstCube>();
                        }

                        cube.transform.SetParent(point.Body.transform);
                        cube.transform.localPosition = Vector3.zero;
                        // cube.transform.localRotation = Quaternion.identity;
                        entity.Get<CubeComponent>().Value = cube;
                    }
                }
            }
        }

        private CubeInfo CheckSavedCube(out CubeInfo cube){
            int saveCube = 0; //= _savedCubes.FirstOrDefault();
            if (_savedCubes != null){
                saveCube = _savedCubes.FirstOrDefault();
                _savedCubes.Remove(saveCube);
            }

            cube = saveCube == 0 ? new CubeInfo() : _cubesSettings.GetCubeByValue(saveCube);

            return cube;
        }

        private CubeView SpawnCube(CubeInfo cubeinfo){
            // var cube = PoolCubes.Instance.Get();
            var cube = Object.Instantiate(_cubesSettings.CubeView);
            cube.SetValue(cubeinfo);
            return cube;
        }

        private CubeView SpawnRandomCube(CubesSettings settings){
            // var cube = PoolCubes.Instance.Get();
            var cube = Object.Instantiate(settings.CubeView);
            cube.SetValue(settings.RandomValue());
            return cube;
        }
    }
}