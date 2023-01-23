using UnityEngine;
using UnityEngine.UI;


namespace NavySpade.pj77.Buildings.Store{
    public class RequiredCubeImage : MonoBehaviour{
        [field: SerializeField]
        public EnumNumberStore NumberStore{ get; set; }
        [field: SerializeField]
        public Image Image{ get; set; }
    }
}