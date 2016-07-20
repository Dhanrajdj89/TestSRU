using System;

namespace SportsRUsApp.Core.DataModel.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class IdAttribute : Attribute
    {
        public Guid Id { get; set; }

        public IdAttribute(string guid)
        {
            Id = new Guid(guid);
        }
    }
}
