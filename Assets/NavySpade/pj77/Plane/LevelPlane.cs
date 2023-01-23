using UnityEngine;


namespace NavySpade.pj77.Plane{
    internal class LevelPlane: MonoBehaviour{
        [field: SerializeField]
        public Transform StartPos{ get; set; }
        [field: SerializeField]
        public Transform EndPos{ get; set; }
    }
}