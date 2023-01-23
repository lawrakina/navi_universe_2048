using NavySpade.pj77.Cubes;
using NavySpade.pj77.Player;


namespace NavySpade.pj77.Buildings.Store{
    internal struct RequireCube{
        public CubeInfo Value;
    }

    internal struct NeedGenerateRandomCube{
    }

    internal struct StoreComponent{
        public StoreView View;
        public IntMinMax CubeSettings;
    }
    
    internal struct CubeSaleComponent{
        public int Value;
        public PlayerView Receiver;
    }
}