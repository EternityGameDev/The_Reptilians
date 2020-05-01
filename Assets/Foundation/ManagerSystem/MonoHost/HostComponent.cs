using UnityEngine;

namespace Scaramouche.Game {
    public class HostComponent : MonoBehaviour {

        [HideInInspector] public static bool hostDestroy = false;

        private UpdateManager manager;

        public void Setup(UpdateManager _mng) {
            manager = _mng;
        }

        private void Update() {
            manager.Tick();
        }

        private void FixedUpdate() {
            manager.TickFixed();
        }
        
        private void LateUpdate() {
            manager.TickLate();
        }    

        private void OnDestroy() {
            hostDestroy = true;
        }
    }
}
