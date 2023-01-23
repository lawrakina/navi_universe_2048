using System.Linq;
using EventSystem.Runtime.Core.Managers;
using Leopotam.Ecs;
using NavySpade.Meta.Runtime.Economic.Currencies;
using NavySpade.Modules.Saving.Runtime;
using NavySpade.pj77.Buildings.Zones;
using NavySpade.pj77.Player;


namespace NavySpade.pj77.Progress{
    internal class ProgressBuildFactoriesSystem : IEcsInitSystem, IEcsRunSystem{
        private EcsFilter<ZoneBuildingComponent> _zonesBuildings;
        private EcsFilter<AddToProgress>.Exclude<AddedToProgress> _addToProgress;

        private PlayerSettings _playerSettings;

        private LimitedCurrency _progressLimit;
        private int _maxCountBuildings;
        private int _currentCountBuildings = 0;
        private Currency _currencyInDb;

        public void Init(){
            _currencyInDb = CurrencyConfig.Instance.UsedInGame.FirstOrDefault(x => x == _progressLimit);
            _currentCountBuildings = 0;
        }

        public void Run(){
            int zoneBuildings = 0;
            foreach (var i in _zonesBuildings){
                zoneBuildings++;
            }

            _maxCountBuildings = zoneBuildings;
            foreach (var i in _addToProgress){
                ref var entity = ref _addToProgress.GetEntity(i);
                ref var progress = ref _addToProgress.Get1(i);
                _currentCountBuildings++;
                entity.Get<AddedToProgress>();

                var temp = (((float) _currentCountBuildings / (float) _maxCountBuildings) * 100f);
                _currencyInDb.Count = (int) temp;

                // if (_currencyInDb.Count >= 10){
                if (_currencyInDb.Count >= _playerSettings.VictoryCondition){
                    EventManager.Invoke(GameStatesEM.OnWin);
                }
            }
        }
    }
}