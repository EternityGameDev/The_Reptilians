using System.Collections.Generic;
using UnityEngine;

namespace Scaramouche.Game {
    public class Starter : MonoBehaviour {
        [SerializeField] private List<ManagerBase> managers = new List<ManagerBase>();

        private void Awake() {
            var host = new GameObject("[TOOLBOX]");
            host.AddComponent<HostComponent>();
            foreach (var concretManager in managers) {
                if (concretManager) {
                    Toolbox.Add(concretManager);
                }
            }
            
            //уничтожаем starter, больше он нам не нужен
            Destroy(this.gameObject);
        }
    }
}