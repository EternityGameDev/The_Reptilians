using UnityEngine;

namespace Scaramouche.Game {
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour{

        private static T instance;
        private static System.Object locked = new System.Object();
        public static bool isSingletonDestroy = false;

        public static T Instance {
            get {
                lock (locked) {
                    if (!instance) {
                        instance = FindObjectOfType<T>();
                        if (!instance) {
                            var singl = GameObject.Find("[TOOLBOX]");
                            instance = singl.AddComponent<T>();
                        }
                    }
                }
                return instance;
            }
        }

        public virtual void OnDestroy() {
            isSingletonDestroy = true;
        }
    }
}
