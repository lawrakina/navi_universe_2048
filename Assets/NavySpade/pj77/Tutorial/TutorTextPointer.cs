using System;
using TMPro;
using UnityEngine;


namespace NavySpade.pj77.Tutorial{
    public class TutorTextPointer : MonoBehaviour{
        [SerializeField]
        private TMP_Text _text;

        public static TutorTextPointer Instance;

        private void Awake(){
            Instance = this;
        }

        public void SetTutorText(string message){
            gameObject.SetActive(message != String.Empty);
            _text.text = message;
        }
    }
}