using System;

namespace Health_App.Common
{
    public interface IEntity
    {
        Guid id { get; set; }
        string name { get; set; }
    }
}
