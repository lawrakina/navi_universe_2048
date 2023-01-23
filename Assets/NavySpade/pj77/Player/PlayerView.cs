using System;
using System.Collections.Generic;
using Mono.CSharp;
using NavySpade.Modules.Pooling.Runtime;
using NavySpade.pj77.Cubes;
using NavySpade.pj77.Extension;
using UnityEngine;


namespace NavySpade.pj77.Player{
    [RequireComponent(typeof(CharacterController))]
    internal class PlayerView : MonoBehaviour{
        #region Visual

        [field: SerializeField]
        public Transform Visual{ get; set; }
        [SerializeField]
        private Transform _hands;

        #endregion


        public AnimatorParameters Animator{ get; set; }
        private CharacterController CharacterController{ get; set; }

        public Action<Collider> TriggerEnter{ get; set; }
        public Action<Collider> TriggerExit{ get; set; }
        public bool IsGrounded{ get; private set; }
        public int GetCubeValue => _cube == null ? 0 : _cube.CubeValue;
        public float VerticalVelocity{ get; set; }


        #region PrivateData

        private CubeView _cube;
        // private ObjectPool<CubeView> _pool;

        #endregion


        private void Awake(){
            CharacterController = GetComponentInChildren<CharacterController>();
            Animator = new AnimatorParameters(GetComponentInChildren<Animator>());
        }

        private void FixedUpdate(){
            var boxSize = 0.2f;
            RaycastHit[] hitInfo = new RaycastHit[5];
            var hit = Physics.BoxCastNonAlloc(
                transform.position,
                new Vector3(transform.localScale.x / 2, boxSize, transform.localScale.z / 2),
                transform.rotation * Vector3.down,
                hitInfo, Quaternion.identity, boxSize,
                1 << LayerManager.GroundLayer);

            IsGrounded = hit == 0 ? false : true;
        }

        private void OnTriggerEnter(Collider other){
            TriggerEnter?.Invoke(other);
        }

        private void OnTriggerExit(Collider other){
            TriggerExit?.Invoke(other);
        }

        private void OnDestroy(){
            TriggerEnter = null;
        }

        public void Pickup(CubeView cubeView){
            Animator.SetPickup(1);
            _cube = cubeView;
            _cube.transform.SetParent(_hands);
            _cube.transform.localPosition = Vector3.zero;
        }

        public CubeView PutCube(){
            Animator.SetPickup(0);
            var result = _cube;
            // PoolCubes.Instance.Release(_cube);
            // _cube = null;
            return result;
        }

        public void ThrowAllOutOfHand(){
            PutCube();
            // var children = new List<CubeView>();
            // foreach (CubeView child in _hands) children.Add(child);
            // children.ForEach(child => PoolCubes.Instance.Release(child));
            
            var children = new List<GameObject>();
            foreach (Transform child in _hands) children.Add(child.gameObject);
            children.ForEach(child => Destroy(child));
        }

        public void CubeToPool(){
            // var children = new List<CubeView>();
            // foreach (Transform child in _hands){
                // if(child.TryGetComponent(out CubeView cube ))
                    // children.Add(cube);
            // }
            // children.ForEach(child => PoolCubes.Instance.Release(child));
            // PoolCubes.Instance.Release(_cube);
        }
    }
}