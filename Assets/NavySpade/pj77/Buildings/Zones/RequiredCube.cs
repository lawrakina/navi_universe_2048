using Leopotam.Ecs;
using NavySpade.pj77.Buildings.Store;
using NavySpade.pj77.Cubes;
using NavySpade.pj77.Extension;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace NavySpade.pj77.Buildings.Zones{
    public class RequiredCube : RequiredResources{
        [Header("links to components")]
        [SerializeField]
        private Image _cubeImage;
        [SerializeField]
        private TMP_Text _cubeValue;
        [SerializeField]
        private TMP_Text _nowCountValue;
        [SerializeField]
        private TMP_Text _maxCountValue;
        [Header("Settings in game")]
        [SerializeField]
        private int _nowCount;

        [SerializeField]
        private GameObject[] _buildingLevels;
        
        private CubesSettings _cubesSettings;
        public int RequireCubeValue;
        private bool _isInit = false;

        public CubeInfo RequireCube{
            set{
                // _cubeImage.color = value.Color;
                _cubeImage.sprite = value.Sprite;
                _cubeValue.text = value.Value.ToString();
            }
        }

        public override void Init(int save){
            if(_isInit) return;
            _isInit = true;
            
            RequireCount = _buildingLevels.Length - save;
            
            _nowCountValue.text = _nowCount.ToString();
            _maxCountValue.text = $"/{_buildingLevels.Length.ToString()}";
            _cubesSettings = ResourceLoader.LoadConfig<CubesSettings>(); //ToDo Naughty Attributes can be load SO`s to fields on Editor?
             RequireCube = _cubesSettings.GetCubeByValue(RequireCubeValue);
        }

        public override ResourceTypeEnum ResourceTypeEnum{ get; set; } = ResourceTypeEnum.Cube;
        public override EcsEntity Entity{ get; set; }
        public override int RequireCount{
            get => _buildingLevels.Length - _nowCount;
            set{
                _nowCount = _buildingLevels.Length - value;
                _nowCountValue.text = _nowCount.ToString();
                _maxCountValue.text = $"/{_buildingLevels.Length.ToString()}";
                for (int i = 0; i < _buildingLevels.Length; i++){
                    _buildingLevels[i].SetActive( i == _nowCount);
                }
            }
        }
    }
}