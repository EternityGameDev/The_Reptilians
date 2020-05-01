using System.Collections.Generic;
using UnityEngine;

namespace Scaramouche.Game {
    [CreateAssetMenu(fileName = "Manager_Update ", menuName = "Managers/Create Managers Update")]
    public class UpdateManager : ManagerBase, IAwake {
        
        private List<ITick> ticks = new List<ITick>();
        private List<ITickFixed> ticksFixed = new List<ITickFixed>();
        private List<ITickLate> ticksLate = new List<ITickLate>();

        public void OnAwake() {
            GameObject.Find("[TOOLBOX]").GetComponent<HostComponent>().Setup(this);
        }

        ///<summary>
        /// Добовляет объект реализующий интерфейсы <c>ITick, ITickLate, ITickFixed</c>
        /// в список обновления
        ///</summary>
        public static void AddTo(object updateObj) {
            var mng = Toolbox.GetManager<UpdateManager>();
            if(mng) {
                if(updateObj is ITick) {
                    mng.ticks.Add(updateObj as ITick);
                }
                if(updateObj is ITickFixed) {
                    mng.ticksFixed.Add(updateObj as ITickFixed);
                }
                if(updateObj is ITickLate) {
                    mng.ticksLate.Add(updateObj as ITickLate);
                }
            }
        }

        ///<summary>
        /// Elfkztn объект реализующий интерфейсы <c>ITick, ITickLate, ITickFixed</c>
        /// из списка обновления
        ///</summary>
        public static void RemoveFrom(object updateObj) {
            var mng = Toolbox.GetManager<UpdateManager>();
            if(mng){
                if(updateObj is ITick) {
                    mng.ticks.Remove(updateObj as ITick);
                }
                if(updateObj is ITickFixed) {
                    mng.ticksFixed.Remove(updateObj as ITickFixed);
                }
                if(updateObj is ITickLate) {
                    mng.ticksLate.Remove(updateObj as ITickLate);
                }
            }
        }    

        public void Tick() {
            for (int i = 0; i < ticks.Count; i++) {
                ticks[i].Tick();
            }
        }

        public void TickFixed() {
            for (int i = 0; i < ticksFixed.Count; i++) {
                ticksFixed[i].TickFixed();
            }
        }

        public void TickLate() {
            for (int i = 0; i < ticksLate.Count; i++) {
                ticksLate[i].TickLate();
            }
        }
    }
}
