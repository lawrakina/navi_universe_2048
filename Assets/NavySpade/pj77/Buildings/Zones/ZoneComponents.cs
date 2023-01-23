using NavySpade.Meta.Runtime.Economic.Currencies;


namespace NavySpade.pj77.Buildings.Zones{
    public struct NeedPickupResource{
        public BatchResource BatchResource;
    }
    
    public struct ZoneBuildingComponent{
        public Building Building;
        public ZoneBuildingView Zone;
    }
    
    internal struct NeedCuclCountsResToTake{
        public RequiredResources[] Value;
    }
    internal struct ResourceCubeComponent{
        public int Value;
    }

    internal struct ResourceMoneyComponent{
        public Currency Currency;
    }

    internal struct NeedPickupCube{
        public BatchResource BatchResourceCube;
    }
}