using System.Linq;
using Leopotam.Ecs;
using NavySpade.Meta.Runtime.Economic.Currencies;
using NavySpade.pj77.Extension;
using NavySpade.pj77.Player;
using UnityEngine;


namespace NavySpade.pj77.Buildings.Zones{
    public class ZoneBuildingView : MonoBehaviour{
        [field: SerializeField]
        public RequiredResources[] RequiredResources{ get; set; }
        public EcsEntity Entity{ get; set; }

        [SerializeField]
        private float _delayBetweenResourceIntake = 0.3f;

        [field: SerializeField]
        public Transform[] LevelBuilds{ get; set; }

        #region privateData

        private float _leftIntakeTime;

        #endregion


        private void OnTriggerStay(Collider other){
            if (_leftIntakeTime > 0){
                _leftIntakeTime -= Time.deltaTime;
            } else{
                _leftIntakeTime = _delayBetweenResourceIntake;
                if (other.TryGetComponent(out PlayerView player)){
                    Entity.Get<NeedCuclCountsResToTake>().Value = RequiredResources;
                }
            }
        }
    }
}