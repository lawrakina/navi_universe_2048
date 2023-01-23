using System;
using System.Collections;
using System.Threading.Tasks;
using Leopotam.Ecs;
using NaughtyAttributes;
using NavySpade.Meta.Runtime.Economic.Currencies;
using NavySpade.pj77.Cubes;
using NavySpade.pj77.Player;
using NavySpade.pj77.Progress;
using UniRx;
using UnityEngine;


namespace NavySpade.pj77.Buildings.Store{
    public class StoreView : FactoryView{
        [field: SerializeField]
        public Currency CurrencyMoney{ get; set; }

        [SerializeField]
        private StoreLabel[] _storeLabel;

        [SerializeField]
        private IntMinMax _cubesSettings;

        private int _cubeRequireValue;
        [SerializeField]
        private EnumNumberStore _storeNumber;

        [field: SerializeField]
        public bool IsTutorRequire{ get; set; }
        [field: SerializeField, ShowIf("IsTutorRequire")]
        public int TutorRequireValue{ get; set; }

        protected override void Init(){
            _storeLabel ??= GetComponentsInChildren<StoreLabel>();

            Entity = World.NewEntity();
            Entity.Get<StoreComponent>().View = this;
            Entity.Get<StoreComponent>().CubeSettings = _cubesSettings;
            Entity.Get<AddToProgress>().Level = 1;
        }

        public void OnTriggerZonePutCubeEnter(Collider other){
            if (!other.TryGetComponent(out PlayerView player)) return;
            if (player.GetCubeValue != _cubeRequireValue) return;
            if (Entity.Has<CubeSaleComponent>()) return;
            Entity.Get<CubeSaleComponent>().Value = player.GetCubeValue;
            Entity.Get<CubeSaleComponent>().Receiver = player;
        }

        public async void UpdateRequire(CubeInfo cubeInfo){
            _cubeRequireValue = cubeInfo.Value;
            foreach (var storeLabel in _storeLabel){
                storeLabel.UpdateInfo(cubeInfo);
            }

            RequireCubesOnUi.Instance.UpdateInfo(cubeInfo, _storeNumber);

            // switch (_storeNumber){
            //     case EnumNumberStore.First:
            //         // RequireCubesOnUi1.Instance.UpdateInfo(cubeInfo);
            //         if (RequireCubesOnUi1.Instance == null)
            //             RequireCubesOnUi1.Init();
            //         await StartCoroutine(FindObjectRqCubes2());
            //         RequireCubesOnUi1.StaticData = cubeInfo;
            //         RequireCubesOnUi1.Instance.UpdateInfo(cubeInfo);
            //         break;
            //     case EnumNumberStore.Second:
            //         if (RequireCubesOnUi2.Instance == null)
            //             RequireCubesOnUi2.Init();
            //         await StartCoroutine(FindObjectRqCubes2());
            //         RequireCubesOnUi2.StaticData = cubeInfo;
            //         RequireCubesOnUi2.Instance.UpdateInfo(cubeInfo);
            //         break;
            //     default:
            //         throw new ArgumentOutOfRangeException();
            // }
        }

        private IEnumerator FindObjectRqCubes2(){
            yield return new WaitForSeconds(1);
            if (RequireCubesOnUi2.Instance == null)
                RequireCubesOnUi2.Init();
        }
    }

    public enum EnumNumberStore{
        First, Second
    }
}