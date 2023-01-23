using Leopotam.Ecs;
using NavySpade.pj77.Buildings.Store;
using NavySpade.pj77.Cubes;
using NavySpade.pj77.Effects;
using NavySpade.pj77.Extension;
using NavySpade.pj77.Player;


namespace NavySpade.pj77.Buildings.Warehouse{
    internal class WarehousesSystem : IEcsInitSystem, IEcsRunSystem{
        private EcsWorld _world;

        private EffectsSettings _effectsSettings;

        private EcsFilter<PlayerComponent> _playerFilter;
        private EcsFilter<WarehouseComponent, PlayerEnterToWarehouse, SlotEmpty> _emptyZone;
        private EcsFilter<WarehouseComponent, PlayerEnterToWarehouse, SlotFill, CubeComponent> _fillZone;

        private EcsEntity _player;

        public void Init(){
            foreach (var i in _playerFilter){
                _player = _playerFilter.GetEntity(i);
            }
        }

        public void Run(){
            foreach (var i in _emptyZone){
                ref var entity = ref _emptyZone.GetEntity(i);
                ref var warehouse = ref _emptyZone.Get1(i);
                ref var enter = ref _emptyZone.Get2(i);

                if (_player.Has<CubeComponent>()){
                    warehouse.View.AddToSlot(_player.Get<CubeComponent>().Value);
                    entity.Get<CubeComponent>() = _player.Get<CubeComponent>();
                    enter.Player.ThrowAllOutOfHand();
                    enter.Player.CubeToPool();

                    ResourceLoader.InstantiateObject(_effectsSettings.TakeCube, warehouse.View.transform.position);

                    var actionToClear = _world.NewEntity();
                    actionToClear.Get<ActionToClearCubeWasTaken>();

                    entity.Get<SlotFill>();
                    entity.Del<PlayerEnterToWarehouse>();
                    entity.Del<SlotEmpty>();
                }
            }

            foreach (var i in _fillZone){
                ref var entity = ref _fillZone.GetEntity(i);
                ref var warehouse = ref _fillZone.Get1(i);
                ref var enter = ref _fillZone.Get2(i);
                ref var cube = ref _fillZone.Get4(i);

                if (!_player.Has<CubeComponent>()){
                    _player.Get<CubeComponent>() = cube;
                    _player.Get<PlayerComponent>().Value.Pickup(cube.Value);
                    warehouse.View.RemoveCubeInSlot();
                    entity.Get<SlotEmpty>();
                    entity.Del<SlotFill>();
                    entity.Del<CubeComponent>();
                    entity.Del<PlayerEnterToWarehouse>();
                }
            }
        }
    }
}