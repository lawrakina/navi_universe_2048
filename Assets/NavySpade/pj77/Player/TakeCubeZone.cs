using System;
using NavySpade.pj77.Tests;
using NavySpade.pj77.Transporter;
using UnityEngine;


namespace NavySpade.pj77.Player{
    internal class TakeCubeZone: MonoBehaviour{
        [field: SerializeField]
        public TransporterView transporterView{ get; set; }
    }
}