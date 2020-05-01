using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scaramouche.Game;

namespace Eternity.DevGame {
    [System.Serializable]
    public class EventExample {
        [SerializeField] private string eventMessage;
        [SerializeField] private Reaction[] buttonReaction;

        public string GetEventMessage {
            get { return eventMessage; }
        }
    }
}
