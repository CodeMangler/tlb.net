using System;
using NAnt.NUnit2.Types;

namespace TLBNETTasks
{
    public class TestLoadBalancer : ITestLoadBalancer
    {
        private readonly string _host;
        private readonly int _port;

        public TestLoadBalancer(string host, int port)
        {
            _host = host;
            _port = port;
        }

        public NUnit2TestCollection Filter(NUnit2TestCollection tests)
        {
            return tests;
        }
    }
}
