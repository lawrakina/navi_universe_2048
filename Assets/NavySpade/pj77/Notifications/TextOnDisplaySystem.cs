using Guirao.UltimateTextDamage;
using Leopotam.Ecs;
using UnityEngine;


namespace NavySpade.pj77.Notifications{
    internal class TextOnDisplaySystem : IEcsInitSystem, IEcsRunSystem{
        private UltimateTextDamageManager _textManager;

        private EcsFilter<ShowingSendMoney> _showing;

        public void Init(){
        }

        public void Run(){
            foreach (var i in _showing){
                ref var entity = ref _showing.GetEntity(i);
                ref var text = ref _showing.Get1(i);

                if(text.Value > 0)
                    _textManager.Add($"+${text.Value}", text.Position + Vector3.up * 2, $"money");

                entity.Del<ShowingSendMoney>();
            }
        }
    }
}