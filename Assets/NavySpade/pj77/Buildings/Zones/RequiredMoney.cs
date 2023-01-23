using Leopotam.Ecs;
using NavySpade.Meta.Runtime.Economic.Currencies;
using TMPro;
using UnityEngine;


namespace NavySpade.pj77.Buildings.Zones{
    public class RequiredMoney : RequiredResources{
        [Header("links to components")]
        [SerializeField]
        private TMP_Text _textOnScene;
        [Header("Settings in game")]
        public Currency Currency;

        [SerializeField]
        private int _count;

        private bool _isInit = false;
        
        public override ResourceTypeEnum ResourceTypeEnum{ get; set; } = ResourceTypeEnum.Money;
        public override EcsEntity Entity{ get; set; }
        public override int RequireCount{
            get => _count;
            set{
                _count = value;
                _textOnScene.text = value.ToString();
            }
        }

        public override void Init(int save){
            if(_isInit) return;
            _isInit = true;
            
            _count = _count - save;
            // _count = _count - save;
            RequireCount = _count;
            _textOnScene.text = _count.ToString();
        }
    }
}