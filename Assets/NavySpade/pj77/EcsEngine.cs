using System.Collections.Generic;
using System.IO;
using System.Text;
using Leopotam.Ecs;
using NavySpade.Modules.Saving.Runtime;
using NavySpade.Modules.Utils.Serialization.Serializers;
using NavySpade.pj77.Buildings.House;
using NavySpade.pj77.Buildings.House.UselessUnits;
#if UNITY_EDITOR
using Leopotam.Ecs.UnityIntegration;
#endif
using NavySpade.pj77.Buildings.Store;
using NavySpade.pj77.Buildings.Warehouse;
using NavySpade.pj77.Buildings.Zones;
using NavySpade.pj77.Cameras;
using NavySpade.pj77.Cubes;
using NavySpade.pj77.Cubes.Merge;
using NavySpade.pj77.Cubes.PickAndPut;
using NavySpade.pj77.Input;
using NavySpade.pj77.Notifications;
using NavySpade.pj77.Player;
using NavySpade.pj77.Progress;
using NavySpade.pj77.Transporter;
using UnityEngine;
using JsonSerializer = NavySpade.Modules.Utils.Serialization.Serializers.JsonSerializer;


namespace NavySpade.pj77{
    internal class EcsEngine : MonoBehaviour{
        private EcsWorld _world;
        private EcsSystems _execute;
        private readonly List<object> _listForInject = new List<object>();
        private bool IsOn = false;

        public static EcsWorld World{ get; set; }

        public void Init(){
            _world = new EcsWorld();
            World = _world;

            _execute = new EcsSystems(_world);
#if UNITY_EDITOR
            EcsWorldObserver.Create(_world);
            EcsSystemsObserver.Create(_execute);
#endif
            _execute
                .Add(new InputControlSystem())
                .Add(new CreatePlayerSystem())
                .Add(new MovingPlayerSystem())
                .Add(new TextOnDisplaySystem())
                .Add(new ConstructionBuildingsSystem())
                // .Add(new CreateTransportersSystem())
                .Add(new SpawnCubesSystem())
                .Add(new ClearCubeWasTakenSystem())
                .Add(new MovingCubesOnTransporterSystem())
                .Add(new PickOrPutCubeOnPlayer())
                .Add(new MergeCubesSystem())
                .Add(new CameraSwitcherSystem())
                .Add(new StoresSystem())
                .Add(new WarehousesSystem())
                .Add(new HousesSystem())
                .Add(new UselessPeopleSystem())
                .Add(new ProgressBuildFactoriesSystem())
                .Add(new SaveCubesSystem())
                ;

            foreach (var obj in _listForInject){
                _execute.Inject(obj);
            }

            IsOn = true;
            _execute.Init();

            SaveManager.Save($"Test", 0);
        }

        public void Inject(object obj){
            if (obj == null) return;
            _listForInject.Add(obj);
            Debug.Log($"Injected object in EcsWorld:{obj}");
        }

        private void Update(){
            if (!IsOn) return;
            _execute?.Run();
        }

        private void OnDisable(){
            if (_execute != null){
                _execute.Destroy();
                _execute = null;
            }

            if (_world != null){
                _world.Destroy();
                _world = null;
            }
        }

        private void OnDestroy(){
            // if (_execute != null){
            //     _execute.Destroy();
            //     _execute = null;
            // }
            //
            // if (_world != null){
            //     _world.Destroy();
            //     _world = null;
            // }
        }
    }
}