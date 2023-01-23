using NavySpade.pj77.Player;
using UnityEngine;
using UnityEngine.AI;


namespace NavySpade.pj77.Buildings.House.UselessUnits{
    [RequireComponent(typeof(NavMeshAgent))]
    public class UnitView : MonoBehaviour{
        #region Fields

        private NavMeshAgent _agent;
        private AnimatorParameters _animator;

        #endregion


        #region Properties

        public float MoveSpeed{
            set => _agent.speed = value;
        }
        public bool IsTargetComplete => _agent.remainingDistance < 1f;

        #endregion


        private void Awake(){
            _agent = GetComponent<NavMeshAgent>();
            _animator = new AnimatorParameters(GetComponentInChildren<Animator>());
        }

        public void StartMove(Vector3 position){
            _agent.SetDestination(position);
        }

        private void Update(){
            _animator.SetRun(_agent.velocity.magnitude);
        }
    }
}