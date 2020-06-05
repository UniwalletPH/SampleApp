using SampleApp.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleApp.CommandTests.Common
{
    public class TestUser : ICurrentUser
    {
        public Guid UID => throw new NotImplementedException();

        public string Name => throw new NotImplementedException();
    }
}
