using System.Collections;
using UnityEngine;
using System;

namespace Scaramouche.Game {
    public class Task : ITask {

        private MonoBehaviour host;
        private Coroutine coroutine;
        private Action feedback;
        private IEnumerator taskAction;

        private bool highPriority;
        private bool isProcessing => coroutine != null;
        private bool isFeedback => feedback != null;

        ///<value>
        /// Приоритет задачи. Если true - то приоритет первостепенный 
        ///</value>
        public bool HighPriority { 
            get { return highPriority; } 
        }

        ///<summary>
        /// Создает задачу.
        ///</summary>
        ///<returns>
        /// Возвращет экземпляр задачи
        ///</returns> 
        public static ITask CreateTask(IEnumerator _action, bool _highPriority = false) {
            return new Task(_action, _highPriority);
        }

        private Task(IEnumerator _action, bool _highPriority = false) {
            host = QueueTaskManager.Host;
            taskAction = _action;
            highPriority = _highPriority;
        }

        ///<summary>
        /// Запускает созданную задачу.
        ///</summary>
        public void Start() {
            Stop();
            coroutine = host.StartCoroutine(RunTask());
        }


        ///<summary>
        /// Останавливает существующую задачу.
        ///</summary>
        public void Stop() {
            if(isProcessing) {
                if (!HostComponent.hostDestroy) {
                    host.StopCoroutine(coroutine);
                    coroutine = null;
                }
            }
        }

        ///<summary>
        /// Действие при завершении задачи
        /// </summary>
        public ITask Subscribe(Action _feedbeck = null) {
            feedback += _feedbeck;
            return this;
        }

        private void CallSubscrible() {
            if(isFeedback) {
                feedback();
            }
        }

        private IEnumerator RunTask() {
            yield return taskAction;
            CallSubscrible();
            coroutine = null;
        }    
    }
}

