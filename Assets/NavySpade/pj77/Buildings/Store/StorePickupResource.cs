using System;
using Leopotam.Ecs;
using NavySpade.pj77.Buildings.Zones;
using NavySpade.pj77.Cubes;
using NavySpade.pj77.Player;
using UnityEngine;


namespace NavySpade.pj77.Buildings.Store{
    [RequireComponent(typeof(BoxCollider))]
    public class StorePickupResource : MonoBehaviour{
        [field: SerializeField]
        private float _deltaBetweenPickups{ get; set; } = 0.5f;
        [field: SerializeField]
        private StoreView StoreView{ get; set; }

        private float _leftTime;

        // private void OnTriggerStay(Collider other){
        //     if (_leftTime > 0){
        //         _leftTime -= Time.deltaTime;
        //     } else{
        //         _leftTime = _deltaBetweenPickups;
        //         StoreView.OnTriggerZonePutCubeEnter(other);
        //     }
        // }

        private void OnTriggerEnter(Collider other){
            StoreView.OnTriggerZonePutCubeEnter(other);
        }
    }
}