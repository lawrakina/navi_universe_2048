using UnityEngine;


namespace NavySpade.pj77.Effects{
    [CreateAssetMenu(fileName = nameof(EffectsSettings), menuName = "Settings/" + nameof(EffectsSettings))]
    public class EffectsSettings : ScriptableObject{
        [field: SerializeField]
        public GameObject SendMoney{ get; set; }
        [field: SerializeField]
        public GameObject BuildingBuild{ get; set; }
        [field: SerializeField]
        public GameObject ReceivingMoney{ get; set; }
        [field: SerializeField]
        public GameObject MergeEffect{ get; set; }
        [field: SerializeField]
        public GameObject SpawnUnit{ get; set; }
        [field: SerializeField]
        public GameObject TakeCube{ get; set; }
    }
}