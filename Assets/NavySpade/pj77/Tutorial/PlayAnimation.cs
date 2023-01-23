using System;
using UnityEngine;


namespace NavySpade.pj77.Tutorial{
    [RequireComponent(typeof(Animation))]
    public class PlayAnimation : MonoBehaviour
    {
        private void Awake(){
            GetComponent<Animation>().Play();
        }
    }
}
