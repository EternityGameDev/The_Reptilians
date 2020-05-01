using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Eternity.DevGame {
    [CreateAssetMenu(fileName = "List Random Events", menuName = "Setings/Create List Random Events")]
    public class ListRandomEvents : ScriptableObject {
        
        [SerializeField] private List<EventExample> eventExamples = new List<EventExample>();
        
        public List<EventExample> GetEventExamples {
            get { return eventExamples; }
        }
    }
}
