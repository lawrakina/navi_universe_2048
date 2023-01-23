using Leopotam.Ecs;
using NaughtyAttributes;
using UnityEngine;


namespace NavySpade.pj77.Buildings.Zones{
    public abstract class RequiredResources : MonoBehaviour{
        public abstract ResourceTypeEnum ResourceTypeEnum{ get; set; }
        public abstract EcsEntity Entity{ get; set; }
        public abstract int RequireCount{ get; set; }
        public abstract void Init(int save);
    }

    public enum ResourceTypeEnum{
        Money, Cube
    }
}