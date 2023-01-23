using System;
using NavySpade.pj77.Cubes;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;


namespace NavySpade.pj77.Buildings.Store{
    public class RequireCubesOnUi2 : MonoBehaviour{
        public static CubeInfo StaticData{ get; set; }
        [field: SerializeField]
        public Image Image{ get; set; }
        [SerializeField]
        private Image _background;
        public static RequireCubesOnUi2 Instance{ get; set; }

        // private void Start(){
        //     Instance = this;
        //     if(_staticData != null){
        //         Image.gameObject.SetActive(true);
        //         Image.sprite = _staticData.Sprite;
        //     }else{
        //         Image.gameObject.SetActive(false);
        //     }
        // }

        private void OnEnable(){
            Instance = this;
            if(StaticData != null){
                Image.gameObject.SetActive(true);
                _background.enabled = true;
                Image.sprite = StaticData.Sprite;
            }else{
                Image.gameObject.SetActive(false);
                _background.enabled = false;
            }
        }

        public void UpdateInfo(CubeInfo cubeInfo){
            StaticData = cubeInfo;
            if(Image== null)
            { var array = FindObjectsOfType<Image>();
                for (int i = 0; i < array.Length; i++){
                    if (i + 1 == array.Length){
                        Image = array[i];
                    }
                }}
            Image.enabled = true;
            Image.sprite = cubeInfo.Sprite;
        }
        public static void Init(){
            Instance= Object.FindObjectOfType<RequireCubesOnUi2>();
        }
    }
}