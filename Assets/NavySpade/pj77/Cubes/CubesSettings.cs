using System;
using NavySpade.pj77.Input;
using UnityEngine;
using Random = UnityEngine.Random;


namespace NavySpade.pj77.Cubes{
    [CreateAssetMenu(fileName = nameof(CubesSettings), menuName = "Settings/" + nameof(CubesSettings))]
    public class CubesSettings : ScriptableObject{
        [field: SerializeField]
        public CubeView CubeView{ get; set; }
        [field: SerializeField]
        public float SpeedMovingCubes{ get; set; }
        [field: SerializeField]
        public CubeInfo[] CubeVariants{ get; set; }
        [field: SerializeField]
        public int MaxRandomValue{ get; set; }

        private void Awake(){
            TransportersCount = 0;
        }

        public CubeInfo RandomValue(){
            return CubeVariants[
                Random.Range(0, MaxRandomValue > CubeVariants.Length ? CubeVariants.Length : MaxRandomValue)];
        }

        public void NextLevelForCube(ref CubeView original){
            for (var index = 0; index < CubeVariants.Length; index++){
                var cubeVariant = CubeVariants[index];
                if (original.CubeValue != cubeVariant.Value) continue;
                original.SetValue(CubeVariants[index + 1]);
                break;
            }
        }

        public CubeInfo GetCube(int value){
            return CubeVariants[value];
        }

        public CubeInfo GetCube(int min, int max){
            return CubeVariants[Random.Range(min, max)];
        }

        public CubeInfo GetCubeByValue(int value){
            foreach (var cubeInfo in CubeVariants){
                if (value == cubeInfo.Value) return cubeInfo;
            }

            return CubeVariants[0];
        }

        public static int TransportersCount{ get; set; }
    }
}