using NavySpade.Meta.Runtime.Economic.Currencies;
using UnityEngine;


namespace NavySpade.pj77.Player{
    [CreateAssetMenu(fileName = nameof(PlayerSettings), menuName = "Settings/" + nameof(PlayerSettings))]
    internal class PlayerSettings : ScriptableObject{
        [field: SerializeField]
        public Currency CurrencyMoney{ get; set; }
        [field: SerializeField]
        public float FrizeTimeAbterPutCube{ get; set; } = 0.3f;
        
        [field: SerializeField]
        public PlayerView PlayerPrefab{ get; set; }

        [field: SerializeField]
        public float MoveSpeed{ get; set; }
        [field: SerializeField]
        public int VictoryCondition{ get; set; } = 100;
        [field: SerializeField]
        public int MoneyAfterFirstStart{ get; set; } = 300;
    }
}