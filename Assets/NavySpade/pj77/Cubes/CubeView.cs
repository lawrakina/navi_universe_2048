using System;
using System.Collections;
using TMPro;
using UnityEngine;


namespace NavySpade.pj77.Cubes{
    public class CubeView : MonoBehaviour{
        [SerializeField]
        private TextMeshPro[] _labels;

        private Renderer _renderer;
        private CubeInfo _cubeInfo;
        
        public int CubeValue => _cubeInfo.Value;

        private void Awake(){
            _renderer = GetComponentInChildren<Renderer>();
        }

        public void SetValue(CubeInfo cubeInfo){
            if (cubeInfo.Value > 0)
                gameObject.SetActive(true);
            
            _cubeInfo = cubeInfo;
            foreach (var label in _labels){
                label.text = cubeInfo.Value.ToString();
                _renderer.material.color = cubeInfo.Color;
                // _renderer.material = cubeInfo.Material;
            }

            name = $"{name}.{cubeInfo.Value}";
        }

        public void MoveTo(Vector3 direction){
            transform.position += direction;
        }
    }
}