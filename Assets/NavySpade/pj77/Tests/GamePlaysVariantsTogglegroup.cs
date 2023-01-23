using System;
using NavySpade.pj77.Player;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;


namespace NavySpade.pj77.Tests{
    public class GamePlaysVariantsTogglegroup : MonoBehaviour{
        [SerializeField]
        public Toggle _first;
        [SerializeField]
        public Toggle _second;
        [SerializeField]
        public Toggle _third;
        private ToggleGroup _toggleGroup;
        private TakeCubeZone[] _zones;

        // public static GamePlaysVariantsTogglegroup Instance{ get; set; }

        private void Awake(){
            // Instance = this;
            _zones = Object.FindObjectsOfType<TakeCubeZone>();
            _third.onValueChanged.AddListener(x => {
                foreach (var zone in _zones){
                    zone.gameObject.SetActive(x);
                }
            });
            // _toggleGroup = GetComponent<ToggleGroup>();
            // _toggleGroup.NotifyToggleOn(_third, true);
            //
        }
    }
}