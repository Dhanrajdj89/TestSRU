using System;

namespace SportsRUsApp.Core.Events
{
    public abstract class MVCForumEventArgs : EventArgs
    {
        public bool Cancel { get; set; }
    }
}
