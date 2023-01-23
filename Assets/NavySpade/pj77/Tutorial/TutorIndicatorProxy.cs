using UnityEngine;


namespace NavySpade.pj77.Tutorial{
    public class TutorIndicatorProxy : MonoBehaviour
    {
        public void SetTutorTextOnIndicator(string message){
            TutorTextPointer.Instance.SetTutorText(message);
        }

        public void SetActiveIndicator(bool state){
            TutorTextPointer.Instance.gameObject.SetActive(state);
        }
    }
}
