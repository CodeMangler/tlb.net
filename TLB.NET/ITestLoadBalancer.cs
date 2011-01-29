using NAnt.NUnit2.Types;

namespace TLBNETTasks
{
    public interface ITestLoadBalancer
    {
        NUnit2TestCollection Filter(NUnit2TestCollection tests);
    }
}
