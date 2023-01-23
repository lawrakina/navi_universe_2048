using System;
using System.Collections.Generic;
using Leopotam.Ecs;
using NavySpade.pj77.Cubes;
using NavySpade.pj77.Player;
using NavySpade.pj77.Progress;
using UnityEngine;


namespace NavySpade.pj77.Buildings.Warehouse{
    [RequireComponent(typeof(Collider))] public class WarehouseView : FactoryView{
        [SerializeField]
        private Transform _slot;

        protected override void Init(){
            base.Init();
            Entity = World.NewEntity();
            Entity.Get<WarehouseComponent>().View = this;
            Entity.Get<SlotEmpty>();
            Entity.Get<AddToProgress>().Level = 1;
        }

        public void AddToSlot(CubeView cubeView){
            cubeView.transform.SetParent(_slot);
            cubeView.transform.localPosition = Vector3.zero;
            cubeView.transform.localRotation = Quaternion.identity;
        }

        public void RemoveCubeInSlot(){
            var children = new List<GameObject>();
            foreach (Transform child in _slot) children.Add(child.gameObject);
            children.ForEach(child => Destroy(child));
        }

        private void OnTriggerEnter(Collider other){
            if (other.TryGetComponent(out PlayerView player)){
                Entity.Get<PlayerEnterToWarehouse>().Player = player;
            }
        }

        private void OnTriggerExit(Collider other){
            if (other.TryGetComponent(out PlayerView _)){
                Entity.Del<PlayerEnterToWarehouse>();
            }
        }
    }
}