using System;
using System.Collections.Generic;
using System.Text;

namespace SampleApp.Application.Common.Interfaces
{
    public interface ICurrentUser
    {
        Guid UID { get; }
        string Name { get; }
    }
}
