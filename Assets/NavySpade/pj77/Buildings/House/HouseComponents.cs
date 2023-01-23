using NavySpade.pj77.Player;


namespace NavySpade.pj77.Buildings.House{
    public struct AwardLeftTime{
        public float Value;
    }

    internal struct NeedSendStorageMoneyToOther{
        public PlayerView Other;
        public int SumMoney;
    }

    public struct LeftTime{
        public float Value;
    }

    public struct HouseComponent{
        public HouseView View;
        public int Units;
        public int UpdateLevel;
    }
}