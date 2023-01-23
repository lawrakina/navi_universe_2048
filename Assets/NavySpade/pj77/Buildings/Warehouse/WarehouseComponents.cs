using NavySpade.pj77.Player;


namespace NavySpade.pj77.Buildings.Warehouse{
    internal struct SlotFill{
    }

    public struct SlotEmpty{
    }
    
    internal struct WarehouseComponent{
        public WarehouseView View;
    }

    internal struct PlayerEnterToWarehouse{
        public PlayerView Player;
    }
}