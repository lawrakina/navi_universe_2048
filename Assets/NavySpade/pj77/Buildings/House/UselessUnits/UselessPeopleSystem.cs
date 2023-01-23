using Leopotam.Ecs;
using NavySpade.pj77.Effects;
using NavySpade.pj77.Extension;
using NavySpade.pj77.Plane;
using NavySpade.pj77.Player;
using UnityEngine;


namespace NavySpade.pj77.Buildings.House.UselessUnits{
    internal class UselessPeopleSystem : IEcsRunSystem{
        private EcsWorld _world;
        
        private EcsFilter<HouseComponent, LeftTime> _spawnUnits;
        private EcsFilter<UselessPeople>.Exclude<MovingToPoint> _stayUnits;
        private EcsFilter<UselessPeople, MovingToPoint> _movingUnits;

        private EffectsSettings _effectsSettings;
        private PlayerSettings _playerSettings;
        private UselessSettings _uselessSettings;
        private LevelPlane _levelPlane;

        public void Run(){
            foreach (var i in _spawnUnits){
                ref var entity = ref _spawnUnits.GetEntity(i);
                ref var house = ref _spawnUnits.Get1(i);
                ref var time = ref _spawnUnits.Get2(i);

                if (time.Value > 0){
                    time.Value -= Time.deltaTime;
                } else{
                    if (house.Units < house.UpdateLevel * house.View.UnitsOnLevel){
                        var unit = _world.NewEntity();
                        unit.Get<UselessPeople>().View = ResourceLoader.InstantiateObject(
                            _uselessSettings.RandomUnit, house.View.SpawnPoint.position);
                        house.Units++;
                        ResourceLoader.InstantiateObject(_effectsSettings.SpawnUnit, house.View.SpawnPoint.position);
                        time.Value = _uselessSettings.DelayTimeBetweenSpawn;
                    }
                }
            }

            foreach (var i in _stayUnits){
                ref var entity = ref _stayUnits.GetEntity(i);
                ref var unit = ref _stayUnits.Get1(i);

                unit.View.MoveSpeed = _uselessSettings.MoveSpeedUnits;
                unit.View.StartMove(CheckEmptyPoint(_levelPlane.StartPos.position, _levelPlane.EndPos.position));
                entity.Get<MovingToPoint>();
            }

            foreach (var i in _movingUnits){
                ref var entity = ref _movingUnits.GetEntity(i);
                ref var unit = ref _movingUnits.Get1(i);

                if (unit.View.IsTargetComplete){
                    entity.Del<MovingToPoint>();
                }
            }
        }
        private Vector3 CheckEmptyPoint(Vector3 startPos, Vector3 endPos){
            var result = startPos;
            for (int i = 0; i < 10; i++){
                var vector = new Vector3(
                    Random.Range(startPos.x, endPos.x),
                    1,
                    Random.Range(startPos.z, endPos.z));
                if (!Physics.CheckSphere(vector, 0.4f)){
                    return vector;
                }
            }

            return result;
        }
    }
}