using System.Linq;
using Leopotam.Ecs;
using NavySpade.Core.Runtime.Levels;
using NavySpade.Modules.Saving.Runtime;
using NavySpade.pj77.Effects;
using NavySpade.pj77.Extension;
using NavySpade.pj77.Player;
using NavySpade.pj77.Tests;
using NavySpade.pj77.Transporter;
using NavySpade.pj77.Transporter.Points;
using UnityEngine;


namespace NavySpade.pj77.Cubes.Merge{
    internal class MergeCubesSystem : IEcsRunSystem{
        private CubesSettings _cubesSettings;
        private EffectsSettings _effectsSettings;

        private LevelsManager _levelsManager;

        private EcsFilter<PlayerComponent, TriggerPickupCube, CubeComponent, EnterZoneTransporter> _merge;
        private EcsFilter<PointTransport, CubeWasTaken, CubeComponent> _transporterWithWasCube;

        public void Run(){
            foreach (var i in _merge){
                ref var entity = ref _merge.GetEntity(i);
                ref var player = ref _merge.Get1(i);
                ref var trigger = ref _merge.Get2(i);
                ref var cube = ref _merge.Get3(i);

                if (trigger.Entity.Has<CubeComponent>()){
                    var ownCube = trigger.Entity.Get<CubeComponent>();
                    if (cube.Value.CubeValue == ownCube.Value.CubeValue){
                        _cubesSettings.NextLevelForCube(ref ownCube.Value);
                        ResourceLoader.InstantiateObject(_effectsSettings.MergeEffect, ownCube.Value.transform.position + Vector3.up*1.5f);

                        foreach (var j in _transporterWithWasCube){
                            ref var transporter = ref _transporterWithWasCube.GetEntity(j);
                            transporter.Del<CubeComponent>();
                            transporter.Del<CubeWasTaken>();
                        }

                        entity.Del<CubeComponent>();

                        //If cube don`t taking from trap => Uncommenting this
                        // entity.Del<TriggerPickupCube>();
                        // entity.Del<EnterZoneTransporter>();

                        player.Value.ThrowAllOutOfHand();
                        player.Value.CubeToPool();
                    }
                }
            }
        }
    }
}