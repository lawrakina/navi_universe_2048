using Leopotam.Ecs;
using NavySpade.pj77.Input;
using UnityEngine;


namespace NavySpade.pj77.Player{
    internal class MovingPlayerSystem : IEcsRunSystem{
        private InputSettings _inputSettings;
        private PlayerSettings _playerSettings;

        private EcsFilter<PlayerComponent, InputAction> _moving;

        public void Run(){
            foreach (var i in _moving){
                ref var entity = ref _moving.GetEntity(i);
                ref var player = ref _moving.Get1(i);
                ref var input = ref _moving.Get2(i);

                player.Value.VerticalVelocity += player.Value.IsGrounded ? 0 : Physics.gravity.y * Time.deltaTime;
                player.CharController.Move(
                    new Vector3(
                        input.Value.x,
                        player.Value.VerticalVelocity,
                        input.Value.z)
                    * (Time.deltaTime * _playerSettings.MoveSpeed));
                player.Value.Visual.transform.LookAt(player.Value.transform.position + input.Value);

                player.Value.Animator.SetRun(Vector3.Magnitude(input.Value));
                entity.Del<InputAction>();
            }
        }
    }
}