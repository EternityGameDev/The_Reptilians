using System.Collections.Generic;
using System;

namespace Scaramouche.Game {
    public class Toolbox : Singleton<Toolbox> {
        
        private Dictionary<Type, object> dataManager = new Dictionary<Type, object>();

        ///<summary>
        /// Метод добавления файла менеджера в библиотеку из которой
        /// он становиться доступен во время работы программы
        ///</summary>
        public static void Add(object obj) {
            var add = obj;
            var manager = obj as ManagerBase;
            
            if (manager) { add = Instantiate(manager); } 
            else return;

            Instance.dataManager.Add(obj.GetType(), add);
            StartAwake(add);
        }

        ///<summary>
        /// Метод получения менеджера из библиотеки во время работы программы
        /// по типу менеджера
        ///</summary>
        public static T GetManager<T>() {
            object value;
            Instance.dataManager.TryGetValue(typeof(T), out value);
            return (T)value;
        }

        private static void StartAwake(object obj) {
            if (obj is IAwake) { (obj as IAwake).OnAwake(); } 
            else return;
        }

        public override void OnDestroy() => base.OnDestroy();
    }
}
