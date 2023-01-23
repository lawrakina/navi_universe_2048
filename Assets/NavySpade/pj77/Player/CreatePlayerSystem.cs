using System;
using Leopotam.Ecs;
using NavySpade.Modules.Pooling.Runtime;
using NavySpade.pj77.Buildings.Warehouse;
using NavySpade.pj77.Cubes;
using NavySpade.pj77.Extension;
using NavySpade.pj77.Input;
using NavySpade.pj77.Plane;
using NavySpade.pj77.Tests;
using UnityEngine;


namespace NavySpade.pj77.Player{
    internal class CreatePlayerSystem : IEcsInitSystem{
        private EcsWorld _world;
        private EcsEntity _entity;
        
        private ObjectPool<CubeView> _pool;
        
        private PlayerView _playerView;

        private readonly PlayerSettings _playerSettings;
        private readonly LevelPlane _levelPlane;

        private readonly EcsEngine _engine;

        public void Init(){
            _entity = _world.NewEntity();
            _entity.Get<TargetControl>();

            ref var player = ref _entity.Get<PlayerComponent>();
            player.Value = _playerView;
            player.CharController = player.Value.gameObject.AddCode<CharacterController>();

            var playerEntity = new PlayerEntity(player.Value, _entity, _playerSettings);
        }
    }
}