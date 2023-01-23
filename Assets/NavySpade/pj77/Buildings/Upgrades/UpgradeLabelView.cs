using TMPro;
using UnityEngine;


namespace NavySpade.pj77.Buildings.Upgrades{
    public class UpgradeLabelView : MonoBehaviour{
        [SerializeField]
        private TMP_Text _levelText;
        [SerializeField]
        private GameObject[] _stickers;

        public int Level{
            set{
                for (var i = 0; i < _stickers.Length; i++){
                    _stickers[i].gameObject.SetActive(value <= i);
                }
            }
        }
    }
}