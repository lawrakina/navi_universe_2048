using System.Linq;
using NavySpade.pj77.Cubes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace NavySpade.pj77.Buildings.Store{
    public class RequireCubesOnUi : MonoBehaviour{
        [field: SerializeField]
        public RequiredCubeImage[] Images{ get; set; }
        [SerializeField]
        private Image _background;
        [SerializeField]
        private GameObject _panel;
        [SerializeField]
        private TMP_Text _title;

        public static RequireCubesOnUi Instance{ get; set; }

        private void Awake(){
            Instance = this;

            Hide();
            foreach (var image in Images){
                image.gameObject.SetActive(false);
            }
        }

        private void Show(){
            _background.enabled = true;
            _title.enabled = true;
            _panel.SetActive(true);
        }

        private void Hide(){
            _background.enabled = false;
            _title.enabled = false;
            _panel.SetActive(false);
        }

        public void UpdateInfo(CubeInfo cubeInfo, EnumNumberStore numberStore){
            foreach (var cubeImage in Images){
                if (cubeImage.NumberStore == numberStore){
                    cubeImage.gameObject.SetActive(true);
                    cubeImage.Image.sprite = cubeInfo.Sprite;
                }
            }
        }

        public void SetActive(bool state){
            if(!state)
                Hide();
            else{
                if (Images.Any(cubeImage => cubeImage.gameObject.activeSelf)){
                    Show();
                }
            }
        }
    }
}