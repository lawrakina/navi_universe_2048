using System;
using NavySpade.pj77.Buildings.Zones;
using UnityEngine;


namespace NavySpade.pj77.Buildings{
    public class Building : MonoBehaviour{
        [field: SerializeField]
        public ZoneBuildingView ZoneBuilding{ get; set; }
        [field: SerializeField]
        public FactoryView Factory{ get; set; }
        [SerializeField]
        private Canvas Canvas;

        private void Awake(){
            if(Canvas != null)
                Canvas.worldCamera = Camera.main;
        }
    }
}