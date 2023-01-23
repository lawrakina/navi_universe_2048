using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace NavySpade.pj77.Tutorial{
    public class TutorDownUpProxy : MonoBehaviour{
        [SerializeField]
        private Transform[] _objectsForDownAndUp;

        private Dictionary<Transform, float> _dictionary = new Dictionary<Transform, float>();

        private void Awake(){
            foreach (var transform1 in _objectsForDownAndUp){
                _dictionary.Add(transform1, transform1.position.y);
            }
        }

        public void Down(){
            foreach (var tr in _objectsForDownAndUp){
                tr.position = new Vector3(
                    tr.position.x,
                    tr.position.y - 10,
                    tr.position.z
                    );
            }
        }

        public void Up(){
            foreach (var kvp in _dictionary){
                kvp.Key.position = new Vector3(
                    kvp.Key.position.x,
                    kvp.Value,
                    kvp.Key.position.z
                );
            }
        }
    }
}