using System;
using UnityEngine;


namespace NavySpade.pj77.Buildings.Store{
    [Serializable] public class IntMinMax{
        [field: SerializeField]
        public int Min{ get; set; } = 4;

        [field: SerializeField]
        public int Max{ get; set; } = 8;
    }
}