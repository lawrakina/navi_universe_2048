using System.Collections.Generic;
using Leopotam.Ecs;
using NavySpade.pj77.Transporter.Points;
using UnityEngine;


namespace NavySpade.pj77.Transporter{
    internal struct TransporterComponent{
        public List<(bool empty, TransporterPointView point)> Points;
        public EcsEntity ZonePickup;
    }

    internal struct ExitZoneTransporter{
    }

    internal struct EnterZoneTransporter{
    }

    internal struct IsFirstCube{
        public int Value;
    }
}