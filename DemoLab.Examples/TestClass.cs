using DemoLab.Core;

namespace DemoLab.Examples
{
    public class TestClass : IRunnable
    {
        public object Run()
        {
            return new Calculator();
        }
    }
}