using Leopotam.Ecs;
using UnityEngine;


namespace NavySpade.pj77.Buildings{
    public class FactoryView : MonoBehaviour{
        public EcsWorld World{ get; set; }
        public EcsEntity Entity;

        public void Activate(bool stage){
            gameObject.SetActive(stage);

            if (stage){
                Init();
            }
        }

        protected virtual void Init(){
        }
    }
}