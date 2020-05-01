using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

namespace Scaramouche.Game {
    [CreateAssetMenu(fileName = "Manager_Queue_Task ", menuName = "Managers/Create Manager Queue Task")]
    public class QueueTaskManager : ManagerBase, IAwake {
        
        private ITask currentTask;
        private ITask CurrentTask {  get { return currentTask; } }
        private List<ITask> tasks = new List<ITask>();
        private bool isCurrentTask => currentTask != null;
        private static HostComponent host;

        ///<value> 
        /// Возвращает объект <c>MonoHostComponent</c>
        ///</value>
        public static HostComponent Host {
            get { return host ?? (host = GameObject.Find("[TOOLBOX]").GetComponent<HostComponent>()); }
        } 

        public void OnAwake() {
            host = GameObject.Find("[TOOLBOX]").GetComponent<HostComponent>();
        }

        ///<summary>
        /// Создает независимую от очереди задачу
        /// Задачу можно запустить вызвав публичный метод <c>Start</c> 
        /// и остановить вызвав публичный метод <c>Stop</c>
        ///</summary>
        ///<returns>
        /// Возвращает объект задачи
        ///</returns>
        public static ITask CreateStandaloneTask(IEnumerator _taskAction) {
            return Task.CreateTask(_taskAction, false);
        }

        ///<summary>
        /// Добавляет задачу в очередь выполнения
        ///</summary>
        public static void AddTask(IEnumerator _taskAction, Action _callback) {
            CreateTask(_taskAction, _callback, false);
        }

        ///<summary>
        /// Добавляет задачу в очередь выполнения
        ///</summary>
        public static void AddTask(IEnumerator _taskAction, bool _highPriority) {
            CreateTask(_taskAction, null, _highPriority);
        }

        ///<summary>
        /// Добавляет задачу в очередь выполнения
        ///</summary>
        public static void AddTask(IEnumerator _taskAction, Action _callback, bool _highPriority) {
            CreateTask(_taskAction, _callback, _highPriority);
        }

        ///<summary>
        /// Добавляет задачу в очередь выполнения
        ///</summary>
        public static void AddTask(IEnumerator _taskAction) {
            CreateTask(_taskAction, null, false);
        }

        private static void CreateTask(IEnumerator _taskAction, Action _callback, bool _highPriority) {
            var mng = Toolbox.GetManager<QueueTaskManager>();
            if (mng) {
                var newTask = Task.CreateTask(_taskAction, _highPriority).Subscribe(_callback);
                mng.AddNewTaskInList(newTask);
            }
        }

        private void AddNewTaskInList(ITask _task) {
            if (_task != null) {
                if (_task.HighPriority) {
                    if (isCurrentTask && !currentTask.HighPriority) {
                        currentTask.Stop();
                    }
                    currentTask = _task;
                    currentTask.Subscribe(TaskQueueProcessing).Start();
                }
                tasks.Add (_task);
                if (!isCurrentTask) {
                    currentTask = GetNextTask();
                    if (isCurrentTask) {
                        currentTask.Subscribe(TaskQueueProcessing).Start();
                    }
                }
            }
        }

        private void TaskQueueProcessing() {
            currentTask = GetNextTask();
            if (isCurrentTask) {
                currentTask.Subscribe(TaskQueueProcessing).Start();
            }
        }

        private ITask GetNextTask() {
            if (tasks.Count > 0) {
                var returnValue = tasks[0]; 
                tasks.RemoveAt(0);
                return returnValue;
            } else return null;
        }
    }   
}