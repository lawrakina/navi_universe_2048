using System;
using UnityEngine;


namespace NavySpade.pj77.Cubes{
    [Serializable] public class CubeInfo{
        [field: SerializeField]
        public Color Color{ get; set; }

        [field: SerializeField]
        public int Value{ get; set; }
        
        [field: SerializeField]
        public Sprite Sprite{ get; set; }
    }
}