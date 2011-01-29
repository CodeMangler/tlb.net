using System;
using Moq;
using NAnt.Core;
using NAnt.NUnit2.Types;
using NUnit.Framework;
using TLBNETTasks;

namespace TLB.NET
{
    [TestFixture]
    public class TLBNunit2TaskTest
    {
        private Mock<ITestLoadBalancer> _mockBalancer;
        private TLBNunit2Task _task;

        [SetUp]
        public void SetUp()
        {
            _mockBalancer = new Mock<ITestLoadBalancer>();
            _task = new TLBNunit2Task(_mockBalancer.Object) {Project = new Project("Test.build", Level.Debug, 1)};
        }

        [Test]
        public void TaskUsesBalancerToFilterOutTests()
        {
            NUnit2TestCollection sampleTests = SampleTests(10);
            Assert.AreEqual(10, sampleTests.Count);

            _mockBalancer.Setup(balancer => balancer.Filter(sampleTests)).Returns(SampleTests(4));

            NUnit2TestCollection balancedTests = _task.Balance(sampleTests);
            Assert.AreEqual(4, balancedTests.Count);
        }

        [Test]
        public void TaskExecutionInvokesBalancer()
        {
            _mockBalancer.Setup(loadBalancer => loadBalancer.Filter(It.IsAny<NUnit2TestCollection>())).Returns((NUnit2TestCollection tests) => tests);
            _task.Execute();
            _mockBalancer.Verify(balancer => balancer.Filter(It.IsAny<NUnit2TestCollection>()), Times.Exactly(1));
        }

        [Test]
        public void TestResultsArePostedToBalancerOnTaskCompletion()
        {
            Assert.Fail();
        }

        private NUnit2TestCollection SampleTests(int count)
        {
            NUnit2TestCollection result = new NUnit2TestCollection();
            for (int i = 0; i < count; i++)
                result.Add(new NUnit2Test());
            return result;
        }
    }
}
