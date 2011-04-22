using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Omega.Web.Services
{
    public interface ITestService {}

    public class TestService : ITestService
    {
        public string GetSomething()
        {
            return "Hello";
        }
    }
}