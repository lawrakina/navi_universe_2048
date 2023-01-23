using Leopotam.Ecs;
using NavySpade.pj77.Player;
using NavySpade.pj77.Tutorial;
using TMPro;
using UnityEngine;


namespace NavySpade.pj77.Input{
    internal class InputControlSystem : IEcsInitSystem, IEcsRunSystem{
        private EcsWorld _world;

        private readonly InputSettings _settings;

        private EcsFilter<JoystickInput>.Exclude<Disable> _joystick;
        private EcsFilter<PlayerComponent, TargetControl> _player;

        public void Init(){
            var joystickGo = Object.FindObjectOfType<Joystick>();
            if (joystickGo == null){
                joystickGo = Object.Instantiate(_settings.Joystick, Gui.Instance.Canvas.transform);
            }

            var entity = _world.NewEntity();
            entity.Get<JoystickInput>().Value = joystickGo;
        }

        public void Run(){
            foreach (var i in _joystick){
                ref var entity = ref _joystick.GetEntity(i);
                ref var joystick = ref _joystick.Get1(i);

                foreach (var j in _player){
                    ref var playerEntity = ref _player.GetEntity(i);
                    playerEntity.Get<InputAction>().Value
                        = new Vector3(
                            joystick.Value.Direction.x,
                            0f,
                            joystick.Value.Direction.y);
                }
            }
        }
    }
}