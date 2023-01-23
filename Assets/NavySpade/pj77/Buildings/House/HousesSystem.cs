using System.Linq;
using Guirao.UltimateTextDamage;
using Leopotam.Ecs;
using NavySpade.Meta.Runtime.Economic.Currencies;
using NavySpade.pj77.Effects;
using NavySpade.pj77.Extension;
using NavySpade.pj77.Notifications;
using NavySpade.pj77.Player;
using UnityEngine;


namespace NavySpade.pj77.Buildings.House{
    internal class HousesSystem : IEcsInitSystem, IEcsRunSystem{
        private EcsWorld _world;
        private EcsEntity _player;

        private UltimateTextDamageManager _textManager;

        private EffectsSettings _effectsSettings;
        private PlayerSettings _playerSettings;

        private EcsFilter<HouseComponent, AwardLeftTime> _receiveAwardTime;
        private EcsFilter<PlayerComponent> _playerFilter;
        private EcsFilter<HouseComponent, NeedSendStorageMoneyToOther> _sendStorageMoney;

        public void Init(){
            foreach (var i in _playerFilter){
                _player = _playerFilter.GetEntity(i);
            }
        }

        public void Run(){
            foreach (var i in _receiveAwardTime){
                ref var house = ref _receiveAwardTime.Get1(i);
                ref var timer = ref _receiveAwardTime.Get2(i);

                timer.Value -= Time.deltaTime;

                if (timer.Value < 0){
                    house.View.storageView.StorageMoney++;
                    timer.Value = house.View.DelayBeforeReceivingAward;
                }
            }


            foreach (var i in _sendStorageMoney){
                ref var entity = ref _sendStorageMoney.GetEntity(i);
                ref var store = ref _sendStorageMoney.Get1(i);
                ref var sender = ref _sendStorageMoney.Get2(i);

                CurrencyConfig.Instance.UsedInGame.FirstOrDefault(
                    x => x == _playerSettings.CurrencyMoney).Count += sender.SumMoney;
                if (sender.SumMoney > 1)
                    ResourceLoader.InstantiateObject(_effectsSettings.SendMoney, store.View.transform.position);
                entity.Get<ShowingSendMoney>().Position = sender.Other.transform.position;
                entity.Get<ShowingSendMoney>().Value = sender.SumMoney;

                entity.Del<NeedSendStorageMoneyToOther>();
            }
        }
    }
}