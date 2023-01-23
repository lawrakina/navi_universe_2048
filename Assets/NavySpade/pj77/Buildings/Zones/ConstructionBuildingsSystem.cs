using System;
using System.Linq;
using Leopotam.Ecs;
using NavySpade.Core.Runtime.Levels;
using NavySpade.Meta.Runtime.Economic.Currencies;
using NavySpade.Modules.Saving.Runtime;
using NavySpade.pj77.Cubes;
using NavySpade.pj77.Effects;
using NavySpade.pj77.Extension;
using NavySpade.pj77.Player;
using Object = UnityEngine.Object;


namespace NavySpade.pj77.Buildings.Zones{
    internal class ConstructionBuildingsSystem : IEcsInitSystem, IEcsRunSystem{
        private EcsWorld _world;

        private EffectsSettings _effectsSettings;
        private LevelsManager _levelsManager;

        private EcsFilter<PlayerComponent> _playerFilter;
        private EcsEntity _player;

        private EcsFilter<ZoneBuildingComponent, NeedCuclCountsResToTake> _cuclCountResources;
        private EcsFilter<ZoneBuildingComponent, NeedPickupResource> _takeResources;
        private EcsFilter<ZoneBuildingComponent, NeedPickupCube> _takeCubes;

        public void Init(){
            foreach (var i in _playerFilter){
                _player = _playerFilter.GetEntity(i);
            }

            foreach (var building in Object.FindObjectsOfType<Building>()){
                if (!CheckAvailabilityResources(building.ZoneBuilding)){
                    //init Zone construct building
                    building.Factory.Activate(false);
                    building.ZoneBuilding.Entity = _world.NewEntity();
                    building.ZoneBuilding.Entity.Get<ZoneBuildingComponent>().Building = building;
                    building.ZoneBuilding.Entity.Get<ZoneBuildingComponent>().Zone = building.ZoneBuilding;
                } else{
                    //init building
                    building.Factory.World = _world;
                    building.Factory.Activate(true);
                }
            }

            foreach (var resource in Object.FindObjectsOfType<RequiredResources>()){
                resource.Entity = _world.NewEntity();
                switch (resource.ResourceTypeEnum){
                    case ResourceTypeEnum.Money:
                        resource.Entity.Get<ResourceMoneyComponent>().Currency = (resource as RequiredMoney)?.Currency;
                        break;
                    case ResourceTypeEnum.Cube:
                        resource.Entity.Get<ResourceCubeComponent>().Value =
                            (resource as RequiredCube).RequireCubeValue;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                var save = SaveManager.Load<int>(
                    $"Level_{_levelsManager.LevelIndex};resource_{resource.GetInstanceID()}");
                resource.Init(save);
            }
        }

        private bool CheckAvailabilityResources(ZoneBuildingView zoneBuilding){
            if (zoneBuilding == null) return true;

            int index = 0;
            foreach (var requiredResources in zoneBuilding.RequiredResources){
                var save = SaveManager.Load<int>(
                    $"Level_{_levelsManager.LevelIndex};resource_{requiredResources.GetInstanceID()}");

                requiredResources.Init(save);

                if (requiredResources.RequireCount == 0){
                    index = index + 1;
                    
                    requiredResources.gameObject.SetActive(false);
                }
            }

            for (int i = 0; i < zoneBuilding.LevelBuilds.Length; i++){
                zoneBuilding.LevelBuilds[i].gameObject.SetActive(i == index);
            }

            var result = index == zoneBuilding.RequiredResources.Length;
            zoneBuilding.gameObject.SetActive(!result);

            return result;
        }

        public void Run(){
            foreach (var i in _cuclCountResources){
                ref var entity = ref _cuclCountResources.GetEntity(i);
                ref var resources = ref _cuclCountResources.Get2(i);

                var currentRequire = resources.Value.FirstOrDefault(x => x.RequireCount > 0);
                if (currentRequire == null) continue;

                if (currentRequire.Entity.Has<ResourceMoneyComponent>()){
                    var curAvailable =
                        CurrencyConfig.Instance.UsedInGame.FirstOrDefault(x =>
                            currentRequire != null &&
                            x == currentRequire.Entity.Get<ResourceMoneyComponent>().Currency);

                    if (currentRequire == null || currentRequire.RequireCount <= 0) continue;
                    if (curAvailable == null || curAvailable.Count <= 0) continue;

                    var delta = 0;
                    if (currentRequire.RequireCount % curAvailable.Count == 0){
                        delta = (curAvailable.Count + 2 - 1) / 2;
                    } else{
                        delta = currentRequire.RequireCount % curAvailable.Count;
                        delta = delta.GetNormalHalf();
                    }

                    entity.Get<NeedPickupResource>().BatchResource = new BatchResource(currentRequire, delta);
                    curAvailable.Count -= delta;

                    entity.Del<NeedCuclCountsResToTake>();
                    continue;
                }

                if (currentRequire.Entity.Has<ResourceCubeComponent>()){
                    var cubeValueAva = _player.Get<PlayerComponent>().Value.GetCubeValue;
                    if (cubeValueAva == currentRequire.Entity.Get<ResourceCubeComponent>().Value){
                        entity.Get<NeedPickupCube>().BatchResourceCube = new BatchResource(currentRequire, 1);
                        _player.Get<PlayerComponent>().Value.ThrowAllOutOfHand();
                        _player.Get<PlayerComponent>().Value.CubeToPool();

                        var actionToClear = _world.NewEntity();
                        actionToClear.Get<ActionToClearCubeWasTaken>();
                    }

                    entity.Del<NeedCuclCountsResToTake>();
                    continue;
                }
            }

            foreach (var i in _takeResources){
                ref var entity = ref _takeResources.GetEntity(i);
                ref var zone = ref _takeResources.Get1(i);
                var batch = _takeResources.Get2(i);

                var resources =
                    zone.Zone.RequiredResources.FirstOrDefault(x => x == batch.BatchResource.RequiredResource);
                if (resources != null){
                    resources.RequireCount -= batch.BatchResource.value;

                    var save = SaveManager.Load<int>(
                        $"Level_{_levelsManager.LevelIndex};resource_{resources.GetInstanceID()}");
                    SaveManager.Save($"Level_{_levelsManager.LevelIndex};resource_{resources.GetInstanceID()}",
                        save + batch.BatchResource.value);
                    // SaveManager.Save(resources.GetInstanceID().ToString(), save + batch.BatchResource.value);
                }

                ResourceLoader.InstantiateObject(_effectsSettings.ReceivingMoney, zone.Building.transform.position);

                if (CheckAvailabilityResources(zone.Zone)){
                    zone.Building.Factory.World = _world;
                    zone.Building.Factory.Activate(true);
                    ResourceLoader.InstantiateObject(_effectsSettings.BuildingBuild, zone.Building.transform.position);
                }

                entity.Del<NeedPickupResource>();
            }

            foreach (var i in _takeCubes){
                ref var entity = ref _takeCubes.GetEntity(i);
                ref var zone = ref _takeCubes.Get1(i);
                var cube = _takeCubes.Get2(i);

                var resources =
                    zone.Zone.RequiredResources.FirstOrDefault(x => x == cube.BatchResourceCube.RequiredResource);
                if (resources != null){
                    resources.RequireCount -= cube.BatchResourceCube.value;

                    var save = SaveManager.Load<int>(
                        $"Level_{_levelsManager.LevelIndex};resource_{resources.GetInstanceID()}");
                    SaveManager.Save($"Level_{_levelsManager.LevelIndex};resource_{resources.GetInstanceID()}",
                        save + cube.BatchResourceCube.value);
                    // SaveManager.Save($"Level_{_levelsManager.LevelIndex};resource_{resources.GetInstanceID()}", save + resources.RequireCount);
                    // SaveManager.Save(resources.GetInstanceID().ToString(), resources.RequireCount);
                }

                if (CheckAvailabilityResources(zone.Zone)){
                    _player.Get<PlayerComponent>().Value.ThrowAllOutOfHand();
                    _player.Get<PlayerComponent>().Value.CubeToPool();
                    zone.Building.Factory.World = _world;
                    zone.Building.Factory.Activate(true);
                    ResourceLoader.InstantiateObject(_effectsSettings.BuildingBuild, zone.Building.transform.position);
                }

                entity.Del<NeedPickupCube>();
            }
        }
    }
}