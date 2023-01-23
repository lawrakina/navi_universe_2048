using Leopotam.Ecs;
using NavySpade.pj77.Transporter.Points;
using UnityEngine;


namespace NavySpade.pj77.Cubes{
    internal class MovingCubesOnTransporterSystem : IEcsRunSystem{
        private CubesSettings _cubesSettings;
        
        private EcsFilter<PointTransport, NextPointEntity, CubeComponent> _points;

        public void Run(){
            foreach (var i in _points){
                ref var entity = ref _points.GetEntity(i);
                ref var point = ref _points.Get1(i);
                ref var cube = ref _points.Get3(i);
                ref var nextPointEntity = ref _points.Get2(i);
                ref var nextPoint = ref nextPointEntity.Value.Get<PointTransport>();

                if (point.IsLast) continue;
                if(nextPointEntity.Value.Has<CubeComponent>()) continue;

                var distance = Vector3.Distance(cube.Value.transform.position, nextPoint.Body.transform.position);
                if (distance > 0.2f){
                    cube.Value.MoveTo((nextPoint.Body.transform.position - point.Body.transform.position) *
                                (Time.deltaTime * _cubesSettings.SpeedMovingCubes));
                } else{
                    cube.Value.transform.SetParent(nextPoint.Body.transform);
                    nextPointEntity.Value.Get<CubeComponent>().Value = cube.Value;
                    entity.Del<CubeComponent>();
                }
            }
        }
    }
}