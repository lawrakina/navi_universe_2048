using System;
using Cinemachine;
using Leopotam.Ecs;
using NavySpade.pj77.Player;
using UnityEngine;


namespace NavySpade.pj77.Cameras{
    public class CameraSwitcherView: MonoBehaviour{
        [field: SerializeField]
        public CinemachineVirtualCamera TargetCamera{ get; set; }
        [field: SerializeField]
        public CameraSwitcherView OtherSwitcher{ get; set; }
        [SerializeField]
        private bool _showingRequireCubes;

        public EcsEntity Entity;

        private void OnTriggerEnter(Collider other){
            if (other.TryGetComponent(out PlayerView _)){
                Entity.Get<PlayerEnterToZoneSwitchingCamera>().ShowingLabel = _showingRequireCubes;
                Entity.Get<PlayerEnterToZoneSwitchingCamera>().Mi = this;
                Entity.Get<PlayerEnterToZoneSwitchingCamera>().Other = OtherSwitcher;
            }
        }
    }
}