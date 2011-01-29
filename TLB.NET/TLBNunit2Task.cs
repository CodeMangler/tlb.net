using NAnt.Core.Attributes;
using NAnt.NUnit2.Tasks;
using NAnt.NUnit2.Types;

namespace TLBNETTasks
{
    [TaskName("tlbnunit2")]
    public class TLBNunit2Task : NUnit2Task
    {
        private ITestLoadBalancer _balancer;

        public string Host { get; set; }
        
        public int Port { get; set; }

        public TLBNunit2Task()
        {
            _balancer = new TestLoadBalancer(Host, Port);
        }

        public TLBNunit2Task(ITestLoadBalancer balancer)
        {
            _balancer = balancer;
        }

        protected override void ExecuteTask()
        {
            FilterTests();
            base.ExecuteTask();
        }

        private void FilterTests()
        {
            NUnit2TestCollection filteredTests = Balance(Tests);
            Tests.Clear();
            filteredTests.AddRange(filteredTests);
        }

        public NUnit2TestCollection Balance(NUnit2TestCollection tests)
        {
            return _balancer.Filter(tests);
        }
    }
}
