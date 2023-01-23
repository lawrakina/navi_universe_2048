using System;
using NavySpade.pj77.Player;
using UnityEngine;


namespace NavySpade.pj77.Tutorial{
    [RequireComponent(typeof(Collider))] public class TutorialTriggerHandlerView : MonoBehaviour{
        [SerializeField]
        private TutorAction _tutorAction;

        private void OnTriggerEnter(Collider other){
            if (other.TryGetComponent(out PlayerView _)){
                TutorialController.InvokeAction(_tutorAction);
            }
        }
    }
}