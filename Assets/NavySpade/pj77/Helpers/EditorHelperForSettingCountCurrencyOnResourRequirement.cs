using System;
using NavySpade.pj77.Buildings.Zones;
using TMPro;
using UnityEngine;


namespace NavySpade.pj77.Editor.Helpers{
    [ExecuteInEditMode] [RequireComponent(typeof(RequiredResources))]
    public class EditorHelperForSettingCountCurrencyOnResourRequirement : MonoBehaviour{
        private RequiredResources _requiredResources;
        private TextMeshPro _text;

        private void Awake(){
            _requiredResources = GetComponent<RequiredResources>();
        }

        private void Update(){
#if UNITY_EDITOR
            // _requiredResources.Init();
#endif
        }
    }
}