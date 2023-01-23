using System.Collections.Generic;
using Leopotam.Ecs;
using NavySpade.pj77.Transporter.Points;
using UnityEngine;


namespace NavySpade.pj77.Transporter{
    internal class CreateTransportersSystem : IEcsInitSystem, IEcsRunSystem{
        private EcsWorld _world;

        public void Init(){
            foreach (var transporterView in Object.FindObjectsOfType<TransporterView>()){
                var entity = _world.NewEntity();
                //parent pransporter
                ref var transporter = ref entity.Get<TransporterComponent>();
                //points
                transporter.Points = new List<(bool empty, TransporterPointView point)>();

                var prevEntity = EcsEntity.Null;
                //for each point create midle point
                for (var index = 0; index < transporterView.Points.Length; index++){
                    var viewPoint = transporterView.Points[index];
                    transporter.Points.Add((true, viewPoint));
                    //create new entity
                    var entityPoint = _world.NewEntity();
                    ref var value = ref entityPoint.Get<PointTransport>();

                    //for tutor
                    if (transporterView.Points[index].IsFirstCubeSet){
                        entityPoint.Get<IsFirstCube>().Value = transporterView.Points[index].IsFirstCubeValue;
                    }

                    //is first?
                    value.IsFirst = index == 0;
                    value.Body = viewPoint;
                    value.Body.MyEntity = entityPoint;
                    if (!value.IsFirst)
                        entityPoint.Get<PrevPointEntity>().Value = prevEntity;
                    //is last?
                    if (transporterView.Points.Length > index + 1){
                        entityPoint.Get<NextPointEntity>().Point = transporterView.Points[index + 1];
                    } else{
                        value.IsLast = true;
                        transporter.ZonePickup = entityPoint;
                        transporterView.EntityLastPoint = entityPoint;
                    }

                    if (prevEntity != EcsEntity.Null)
                        prevEntity.Get<NextPointEntity>().Value = entityPoint;

                    prevEntity = entityPoint;
                }
            }
        }

        public void Run(){
            
        }
    }
}