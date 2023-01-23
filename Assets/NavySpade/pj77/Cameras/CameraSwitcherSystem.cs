using Leopotam.Ecs;
using NavySpade.pj77.Buildings.Store;
using NavySpade.pj77.Player;
using UnityEngine;


namespace NavySpade.pj77.Cameras{
    internal class CameraSwitcherSystem : IEcsInitSystem, IEcsRunSystem{
        private EcsWorld _world;
        private PlayerView _playerView;
        
        private EcsFilter<CameraSwitcherComponent, PlayerEnterToZoneSwitchingCamera> _switch;

        public void Init(){
            foreach (var cameraSwitcherView in Object.FindObjectsOfType<CameraSwitcherView>()){
                cameraSwitcherView.Entity = _world.NewEntity();
                cameraSwitcherView.Entity.Get<CameraSwitcherComponent>();
                CameraManager.Instance.AddCamera(cameraSwitcherView.TargetCamera);
            }
        }

        public void Run(){
            foreach (var i in _switch){
                ref var entity = ref _switch.GetEntity(i);
                ref var switchValue = ref _switch.Get2(i);

                CameraManager.Instance.SwitchCamera(switchValue.Mi.TargetCamera);
                switchValue.Mi.gameObject.SetActive(false);
                switchValue.Other.gameObject.SetActive(true);
                RequireCubesOnUi.Instance.SetActive(switchValue.ShowingLabel);
                entity.Del<PlayerEnterToZoneSwitchingCamera>();
            }
        }
    }
}