using System;
using System.Collections.Generic;
using Leopotam.Ecs;
using NavySpade.pj77.Buildings;
using NavySpade.pj77.Cubes;
using NavySpade.pj77.Tests;
using NavySpade.pj77.Transporter.Points;
using UnityEngine;


namespace NavySpade.pj77.Transporter{
    public class TransporterView : FactoryView{
        public EcsEntity EntityLastPoint{ get; set; }
        [field: SerializeField]
        public TransporterPointView[] Points{ get; set; }

        public void ReturnCube(CubeView cubeView){
            cubeView.transform.SetParent(EntityLastPoint.Get<PointTransport>().Body.transform);
            cubeView.gameObject.SetActive(true);
            cubeView.transform.localPosition = Vector3.zero;
            cubeView.transform.localRotation = Quaternion.identity;
        }

        protected override void Init(){
            base.Init();
            
            var entity = World.NewEntity();
            //parent pransporter
            ref var transporter = ref entity.Get<TransporterComponent>();
            //points
            transporter.Points = new List<(bool empty, TransporterPointView point)>();

            var prevEntity = EcsEntity.Null;
            //for each point create midle point
            for (var index = 0; index < this.Points.Length; index++){
                var viewPoint = this.Points[index];
                transporter.Points.Add((true, viewPoint));
                //create new entity
                var entityPoint = World.NewEntity();
                ref var value = ref entityPoint.Get<PointTransport>();

                //for tutor
                if (this.Points[index].IsFirstCubeSet){
                    entityPoint.Get<IsFirstCube>().Value = this.Points[index].IsFirstCubeValue;
                }

                //is first?
                value.IsFirst = index == 0;
                value.Body = viewPoint;
                value.Body.MyEntity = entityPoint;
                if (!value.IsFirst)
                    entityPoint.Get<PrevPointEntity>().Value = prevEntity;
                //is last?
                if (this.Points.Length > index + 1){
                    entityPoint.Get<NextPointEntity>().Point = this.Points[index + 1];
                } else{
                    value.IsLast = true;
                    transporter.ZonePickup = entityPoint;
                    this.EntityLastPoint = entityPoint;
                }

                if (prevEntity != EcsEntity.Null)
                    prevEntity.Get<NextPointEntity>().Value = entityPoint;

                prevEntity = entityPoint;
            }

            CubesSettings.TransportersCount++;
        }
    }
}