using NavySpade.Meta.Runtime.Economic.Currencies;


namespace NavySpade.pj77.Buildings.Zones{
    public class BatchResource{
        public RequiredResources RequiredResource{ get; }
        public int value{ get; }

        public BatchResource(RequiredResources requiredResource, int value){
            this.RequiredResource = requiredResource;
            this.value = value;
        }
    }
}