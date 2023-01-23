using UnityEngine;


namespace NavySpade.pj77.Buildings.House.UselessUnits{
    [CreateAssetMenu(fileName = nameof(UselessSettings), menuName = "Settings/" + nameof(UselessSettings))]
    public class UselessSettings : ScriptableObject{
        [SerializeField]
        private UnitView[] _listUnits;
        
        public UnitView RandomUnit => _listUnits[Random.Range(0, _listUnits.Length)];

        [field: SerializeField]
        public float DelayTimeBetweenSpawn{ get; set; } = 2f;
        [field: SerializeField]
        public float MoveSpeedUnits{ get; set; } = 1f;
    }
}