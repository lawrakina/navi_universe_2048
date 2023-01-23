using System.Linq;
using Guirao.UltimateTextDamage;
using Leopotam.Ecs;
using NavySpade.Meta.Runtime.Economic.Currencies;
using NavySpade.pj77.Cubes;
using NavySpade.pj77.Effects;
using NavySpade.pj77.Extension;
using NavySpade.pj77.Notifications;
using NavySpade.pj77.Player;
using UnityEngine;


namespace NavySpade.pj77.Buildings.Store{
    internal class StoresSystem : IEcsInitSystem, IEcsRunSystem{
        private EcsWorld _world;

        private CubesSettings _cubesSettings;
        private EffectsSettings _effectsSettings;

        private UltimateTextDamageManager _textManager;

        private EcsFilter<StoreComponent>.Exclude<RequireCube> _generateCube;
        private EcsFilter<StoreComponent, UpdateRequireCube> _updateRequireCube;

        private EcsFilter<StoreComponent, CubeSaleComponent> _stores;

        private EcsFilter<PlayerComponent> _playerFilter;
        private EcsEntity _player;

        public void Init(){
            foreach (var i in _playerFilter){
                _player = _playerFilter.GetEntity(i);
            }
        }

        public void Run(){
            foreach (var i in _generateCube){
                ref var entity = ref _generateCube.GetEntity(i);
                ref var store = ref _generateCube.Get1(i);

                var requireCube = _cubesSettings.GetCube(store.CubeSettings.Min, CubesSettings.TransportersCount);
                // var requireCube = _cubesSettings.GetCube(store.CubeSettings.Min, store.CubeSettings.Max);

                //for tutor setting first cube
                if (store.View.IsTutorRequire){
                    requireCube = _cubesSettings.GetCubeByValue(store.View.TutorRequireValue);
                    store.View.IsTutorRequire = false;
                }

                entity.Get<RequireCube>().Value = requireCube;
                entity.Get<UpdateRequireCube>().Value = requireCube;
                
            }

            foreach (var i in _stores){
                ref var entity = ref _stores.GetEntity(i);
                ref var store = ref _stores.Get1(i);
                ref var saleInfo = ref _stores.Get2(i);

                var actionToClear = _world.NewEntity();
                actionToClear.Get<ActionToClearCubeWasTaken>();
                saleInfo.Receiver.ThrowAllOutOfHand();
                saleInfo.Receiver.CubeToPool();
                _player.Del<CubeComponent>();
                
                entity.Get<ShowingSendMoney>().Position = saleInfo.Receiver.transform.position;
                entity.Get<ShowingSendMoney>().Value = saleInfo.Value;
                ResourceLoader.InstantiateObject(_effectsSettings.SendMoney, store.View.transform.position);

                var view = store.View;
                CurrencyConfig.Instance.UsedInGame.FirstOrDefault(x => x == view.CurrencyMoney).Count += saleInfo.Value;
                entity.Del<CubeSaleComponent>();
                entity.Del<RequireCube>();
            }

            foreach (var i in _updateRequireCube){
                ref var entity = ref _updateRequireCube.GetEntity(i);
                ref var store = ref _updateRequireCube.Get1(i);
                ref var updateRequireCube = ref _updateRequireCube.Get2(i);
                
                store.View.UpdateRequire(updateRequireCube.Value);
                entity.Del<UpdateRequireCube>();
            }
        }
    }

    internal struct UpdateRequireCube{
        public CubeInfo Value;
    }
}