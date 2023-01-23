using Leopotam.Ecs;


namespace NavySpade.pj77.Transporter.Points{
    internal struct NextPointEntity{
        public EcsEntity Value;
        public TransporterPointView Point;
    }

    internal struct PrevPointEntity{
        public EcsEntity Value;
    }

    internal struct PointTransport{
        public bool IsFirst;
        public TransporterPointView Body;
        public bool IsLast;
    }
}