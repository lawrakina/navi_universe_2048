using Leopotam.Ecs;
using NavySpade.pj77.Buildings.Warehouse;
using NavySpade.pj77.Player;


namespace NavySpade.pj77.Cubes{
    internal class ClearCubeWasTakenSystem : IEcsRunSystem{
        private EcsFilter<ActionToClearCubeWasTaken> _action;
        private EcsFilter<CubeWasTaken, CubeComponent> _clearPoint;
        private EcsFilter<PlayerComponent, CubeComponent> _player;
        private EcsFilter<CubeComponent, WarehouseComponent> _missingCube;

        public void Run(){
            foreach (var i in _action){
                _action.GetEntity(i).Del<ActionToClearCubeWasTaken>();
                foreach (var j in _clearPoint){
                    ref var entity = ref _clearPoint.GetEntity(j);
                    entity.Del<CubeWasTaken>();
                    entity.Del<CubeComponent>();
                }

                foreach (var k in _player){
                    _player.GetEntity(k).Del<CubeComponent>();
                }

                foreach (var l in _missingCube){
                    ref var entity = ref _missingCube.GetEntity(l);
                    ref var cube = ref _missingCube.Get1(l);

                    if (cube.Value == null){
                        entity.Del<CubeComponent>();
                        entity.Del<SlotFill>();
                        entity.Get<SlotEmpty>();
                    }
                }
            }
        }
    }
}