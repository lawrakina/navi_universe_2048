using UnityEngine;


namespace NavySpade.pj77.Input{
    [CreateAssetMenu(fileName = nameof(InputSettings), menuName = "Settings/" + nameof(InputSettings))]
    internal class InputSettings : ScriptableObject{
        [field: SerializeField]
        public float JoystickSensitivity{ get; set; } = 0.1f;
        [field: SerializeField]
        public Joystick Joystick{ get; set; }
    }
}