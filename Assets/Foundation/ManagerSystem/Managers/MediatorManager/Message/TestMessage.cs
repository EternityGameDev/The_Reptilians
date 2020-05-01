using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scaramouche.Game {
    public class TestMessage : BaseMessage {
        public bool t;
        public TestMessage(bool _t) {
            t = _t;
        } 
    }

    public class MessageColor : BaseMessage {
        public Color color;
        public MessageColor(Color _color) {
            color = _color;
        }
    }
}
