using Leopotam.Ecs;
using NavySpade.pj77.Player;
using NavySpade.pj77.Transporter;


namespace NavySpade.pj77.Cubes.PickAndPut{
    internal class PickOrPutCubeOnPlayer : IEcsRunSystem{
        private EcsWorld _world;

        private EcsFilter<PlayerComponent, TriggerPickupCube, EnterZoneTransporter>.Exclude<CubeComponent> _pick;
        private EcsFilter<PlayerComponent, TriggerPickupCube, CubeComponent, EnterZoneTransporter> _put;

        public void Run(){
            foreach (var i in _pick){
                ref var entity = ref _pick.GetEntity(i);
                ref var player = ref _pick.Get1(i);
                ref var trigger = ref _pick.Get2(i);

                if (trigger.Entity.Has<CubeComponent>()){
                    entity.Get<CubeComponent>() = trigger.Entity.Get<CubeComponent>();
                    player.Value.Pickup(trigger.Entity.Get<CubeComponent>().Value);
                    trigger.Entity.Get<CubeWasTaken>();

                    entity.Del<EnterZoneTransporter>();
                }
            }

            foreach (var i in _put){
                ref var entity = ref _pick.GetEntity(i);
                ref var player = ref _pick.Get1(i);
                ref var trigger = ref _pick.Get2(i);

                if (trigger.Entity.Has<CubeWasTaken>()){
                    var cubeView = player.Value.PutCube();
                    trigger.Value.ReturnCube(cubeView);
                    entity.Del<CubeComponent>();

                    trigger.Entity.Del<CubeWasTaken>();
                    entity.Del<TriggerPickupCube>();
                    entity.Del<EnterZoneTransporter>();

                    var actionToClear = _world.NewEntity();
                    actionToClear.Get<ActionToClearCubeWasTaken>();

                    player.Value.CubeToPool();
                }
            }
        }
    }
}