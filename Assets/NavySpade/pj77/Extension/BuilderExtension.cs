using UnityEngine;


namespace NavySpade.pj77.Extension{
    public static class BuilderExtension{
        public static T AddCode<T>(this GameObject gameObject) where T : Component
        {
            var component = gameObject.GetOrAddComponent<T>();
            return component;
        }

        private static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            var result = gameObject.GetComponent<T>();
            if (!result) result = gameObject.AddComponent<T>();

            return result;
        }
    }
}