using DemoLab.Core;

namespace DemoLab.Examples
{
    public class Calculator : ICalculator
    {
        public int Sum(int x, int y)
        {
            return x + y;
        }

        public int Divide(int x, int y)
        {
            return x / y;
        }

        public int Subst(int x, int y)
        {
            return x - y;
        }
    }
}