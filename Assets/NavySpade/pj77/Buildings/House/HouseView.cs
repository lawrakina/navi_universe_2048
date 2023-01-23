using Leopotam.Ecs;
using NavySpade.pj77.Player;
using NavySpade.pj77.Progress;
using UnityEngine;


namespace NavySpade.pj77.Buildings.House{
    public class HouseView : FactoryView{
        [field: SerializeField]
        public Transform SpawnPoint{ get; set; }
        [field: SerializeField, Range(0, 10)]
        public int UnitsOnLevel{ get; set; } = 3;

        [Header("Visual")]
        [SerializeField]
        public GameObject[] VisualOnUpgradeLevel;

        [field: SerializeField]
        public float DelayBeforeReceivingAward{ get; set; } = 0.3f;

        [field: SerializeField]
        public StorageView storageView{ get; set; }

        protected override void Init(){
            base.Init();
            Entity = World.NewEntity();
            Entity.Get<HouseComponent>().View = this;
            Entity.Get<HouseComponent>().Units = 0;
            Entity.Get<HouseComponent>().UpdateLevel = 1;
            Entity.Get<LeftTime>().Value = 1f;
            Entity.Get<AwardLeftTime>().Value = DelayBeforeReceivingAward;
            Entity.Get<AddToProgress>().Level = 1;
        }

        private void OnTriggerEnter(Collider other){
            if (other.TryGetComponent(out PlayerView player)){
                Entity.Get<NeedSendStorageMoneyToOther>().Other = player;
                Entity.Get<NeedSendStorageMoneyToOther>().SumMoney = storageView.StorageMoney;
                storageView.StorageMoney = 0;
            }
        }
    }
}