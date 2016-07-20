﻿using System;
using System.Collections.Generic;
using SportsRUsApp.Core.DataModel;

namespace SportsRUsApp.Core.Exceptions
{

    public class InUseUnableToDeleteException : Exception
    {
        public List<Entity> BlockingEntities { get; set; }

        private InUseUnableToDeleteException()
        {
            // hidden.
        }

        private InUseUnableToDeleteException(string message, Exception inner)
        {
            // hidden.
        }

        public InUseUnableToDeleteException(List<Entity> blockingEntities) 
        {
            BlockingEntities = blockingEntities;
        }

    }
}
