﻿using System;

namespace Scaramouche.Game {
    public interface ITask {
        bool HighPriority { get; }
        ITask Subscribe(Action _feedBack = null);
        void Start();
        void Stop();
    }
}
