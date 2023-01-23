using Leopotam.Ecs;
using NavySpade.pj77.Transporter;


namespace NavySpade.pj77.Cubes{
    internal struct TriggerPickupCube{
        public EcsEntity Entity;
        public TransporterView Value;
    }
    
    internal struct CubeWasTaken{
    }
    internal struct ActionToClearCubeWasTaken{
    }
    
    internal struct CubeComponent{
        public CubeView Value;
    }
}