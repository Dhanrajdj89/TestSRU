using System;

namespace SportsRUsApp.Core.DataModel.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class FilePathAttribute : Attribute
    {
        public string FilePath { get; set; }

        public FilePathAttribute(string filePath)
        {
            FilePath = filePath;
        }
    }
}
