using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scaramouche.Game;

namespace Eternity.DevGame {
    [CreateAssetMenu(fileName = "Generator_Event_Manager", menuName = "Managers/Create Generator Event Manager")]
    public class GeneratorEventManager : ManagerBase, IAwake {
        
        [SerializeField] private ListRandomEvents events;
        private static GeneratorEventManager generatorEvent;
        private CanvasHandler mainCanvas;

        public void OnAwake() {
            generatorEvent = Toolbox.GetManager<GeneratorEventManager>();
            mainCanvas = FindObjectOfType<CanvasHandler>();
        }

        public void CallEvent() {
            int i = Random.Range(0, (events.GetEventExamples.Count - 1));
            Generate(events.GetEventExamples[i]);
        }

        public static void Generate(EventExample _event) {
            if (generatorEvent) {
                if (!(_event == null)) {
                    generatorEvent.mainCanvas.eventText.text = _event.GetEventMessage;
                    
                } 
            }
        }

    }
}
