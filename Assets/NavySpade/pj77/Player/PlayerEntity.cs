using Leopotam.Ecs;
using NavySpade.pj77.Cubes;
using NavySpade.pj77.Extension;
using NavySpade.pj77.Transporter;
using UnityEngine;


namespace NavySpade.pj77.Player{
    internal class PlayerEntity{
        private readonly PlayerView _playerValue;
        private readonly EcsEntity _entity;
        private readonly PlayerSettings _playerSettings;

        public PlayerEntity(PlayerView playerValue, EcsEntity entity, PlayerSettings playerSettings){
            _playerValue = playerValue;
            _entity = entity;
            _playerSettings = playerSettings;

            _playerValue.TriggerEnter += TriggerEnter;
            _playerValue.TriggerExit += TriggerExit;
        }

        private void TriggerEnter(Collider obj){
            if (obj.CompareTag(Constants.ZonePickupTag) &&
                obj.transform.parent.TryGetComponent(out TransporterView transporterView)){
                _entity.Get<TriggerPickupCube>().Entity = transporterView.EntityLastPoint;
                _entity.Get<TriggerPickupCube>().Value = transporterView;
                _entity.Get<EnterZoneTransporter>();
                _entity.Del<ExitZoneTransporter>();
            }
        }

        private void TriggerExit(Collider obj){
            if (obj.CompareTag(Constants.ZonePickupTag) &&
                obj.transform.parent.TryGetComponent(out TransporterView transporterView)){
                _entity.Del<EnterZoneTransporter>();
                _entity.Get<ExitZoneTransporter>();
            }
        }
    }
}