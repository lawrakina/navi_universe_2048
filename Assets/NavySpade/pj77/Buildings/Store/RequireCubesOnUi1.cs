using NavySpade.pj77.Cubes;
using UnityEngine;
using UnityEngine.UI;


namespace NavySpade.pj77.Buildings.Store{
    public class RequireCubesOnUi1 : MonoBehaviour{
        public static CubeInfo StaticData{ get; set; }
        [field: SerializeField]
        public Image Image{ get; set; }
        public static RequireCubesOnUi1 Instance{ get; set; }

        private void OnEnable(){
            Instance = this;
            if(StaticData != null){
                Image.sprite = StaticData.Sprite;
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
            Instance= Object.FindObjectOfType<RequireCubesOnUi1>();
        }
    }
}