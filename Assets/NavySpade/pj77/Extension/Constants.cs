using UnityEngine;


namespace NavySpade.pj77.Extension{
    internal static class Constants{
        public static readonly string ZonePickupTag = $"Transporter";
    }
    public static class LayerManager{
        public static LayerMask GroundLayer = LayerMask.NameToLayer(StringManager.GROUND_LAYER);
        // public static int Default = LayerMask.NameToLayer(StringManager.DEFAULT);
        // public static int EnemyLayer = LayerMask.NameToLayer(StringManager.ENEMY_LAYER);
        // public static int PlayerLayer = LayerMask.NameToLayer(StringManager.PLAYER_LAYER);

        /// <summary>
        ///     Проверка на вхождение слоя layer в маску слоев layerMask
        /// </summary>
        /// <param name="layerMask"></param>
        /// <param name="collider"></param>
        /// <returns></returns>
        public static bool CheckForComparerLayer(LayerMask layerMask, int layer){
            return (layerMask.value & (1 << layer)) != 0;
        }

        /// <summary>
        ///     Проверка на вхождение слоя collider.gameObject в маску слоев layerMask
        /// </summary>
        /// <param name="layerMask"></param>
        /// <param name="collider"></param>
        /// <returns></returns>
        public static bool CheckForComparerLayer(LayerMask layerMask, GameObject gameObject){
            return (layerMask.value & (1 << gameObject.layer)) != 0;
        }

        /// <summary>
        ///     Проверка на вхождение слоя collider.gameObject.layer в маску слоев layerMask
        /// </summary>
        /// <param name="layerMask"></param>
        /// <param name="collider"></param>
        /// <returns></returns>
        public static bool CheckForComparerLayer(LayerMask layerMask, Collider collider){
            return (layerMask.value & (1 << collider.gameObject.layer)) != 0;
        }
    }
}