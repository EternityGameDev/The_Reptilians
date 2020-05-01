using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Scaramouche.Game {
    [CreateAssetMenu(fileName = "Mediator_Manager ", menuName = "Managers/Create Mediator Manager")]
    public class MediatorManager : ManagerBase, IAwake {

        private static MediatorManager mediator;
        private Dictionary<Type, Delegate> subscribers = new Dictionary<Type, Delegate>();

        public void OnAwake() {
            mediator = Toolbox.GetManager<MediatorManager>();
        }
        
        public static void AddSubscribe<T>(MediatorCallback<T> _message) where T : BaseMessage {
            if (mediator) {
                if (_message == null) return;
                    var t = typeof(T);
                if (mediator.subscribers.ContainsKey(t)) {
                    mediator.subscribers[t] = Delegate.Combine(mediator.subscribers[t], _message); 
                } else {
                    mediator.subscribers.Add(t, _message);
                }
            }
        }

        public static void RemoveSubscribe<T>(MediatorCallback<T> _message) where T : BaseMessage {
            if (mediator) {
                if (_message == null) return;
                var t = typeof(T);
                if (mediator.subscribers.ContainsKey(t)) {
                    var temp = mediator.subscribers[t];
                    temp = System.Delegate.Remove(temp, _message);
                    if (temp == null) { mediator.subscribers.Remove(t); }
                    else { mediator.subscribers[t] = temp; }
                }
            }
        }

        public static void SendMessage<T>(T _message) where T : BaseMessage {
            if (mediator) {
                var t = typeof(T);
                if (mediator.subscribers.ContainsKey(t)) {
                    mediator.subscribers[t].DynamicInvoke(_message);
                }
            }
        }

    }
}
