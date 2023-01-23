using System;
using UnityEngine;


namespace NavySpade.pj77.Tutorial{
    public class TutorArrow : MonoBehaviour{
        enum TypeRotate{ RotateOn1Axis, RotateOn3Axis }

        [SerializeField]
        private TypeRotate _typeRotate;
        public Transform Target{ get; set; }

        private void Update(){
            if (Target == null)
                return;

            switch (_typeRotate){
                case TypeRotate.RotateOn1Axis:
                    var endRotation = Quaternion.LookRotation(Target.position - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, endRotation, Time.deltaTime * 8);
                    transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
                    break;
                case TypeRotate.RotateOn3Axis:
                    transform.rotation = Quaternion.LookRotation(Target.position - transform.position, Vector3.up);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}