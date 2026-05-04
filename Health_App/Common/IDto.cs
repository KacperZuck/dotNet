using System;

namespace Health_App.Common
{
    public interface IDto
    {
        Guid id { get; set; }
        string name { get; set; }
    }
}
