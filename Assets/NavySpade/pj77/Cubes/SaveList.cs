using System;
using System.Collections.Generic;
using UnityEngine;


namespace NavySpade.pj77.Cubes{
    [Serializable] internal struct SaveList{
        [SerializeField]
        public List<int> list;

        public SaveList(List<int> list){
            this.list = list;
        }
    }
}