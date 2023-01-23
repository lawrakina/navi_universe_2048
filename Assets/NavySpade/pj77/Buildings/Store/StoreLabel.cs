using NavySpade.pj77.Cubes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace NavySpade.pj77.Buildings.Store{
    internal class StoreLabel : MonoBehaviour{
        [SerializeField]
        private TMP_Text _number;
        [SerializeField]
        private Image _sprite;

        public void UpdateInfo(CubeInfo cubeInfo){
            _number.text = cubeInfo.Value.ToString();
            // _sprite.color = cubeInfo.Color;
            _sprite.sprite = cubeInfo.Sprite;
        }
    }
}