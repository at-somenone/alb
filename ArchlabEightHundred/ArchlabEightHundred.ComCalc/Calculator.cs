using System;
using System.Runtime.InteropServices;
using NCalc;

namespace ArchlabEightHundred.Com
{
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [ComSourceInterfaces(typeof(CalculatorEvents))]
    [Guid("0eae5e5d-ea09-42da-a2c6-b3e2c40e0a95")] 
    public class Calculator: ICalculator
    {
        public object Calculate(string expression)
        {
            var exp = new Expression(expression);
            return exp.Evaluate();
        }
        public double Add(double n1, double n2) {
            return n1 + n2;
        }

        public double Divide(double n1, double n2) {
            return n1 / n2;
        }

        public double Multiply(double n1, double n2) {
            return n1 * n2;
        }

        public double Power(double n1, double n2) {
            return Math.Pow(n1, n2);
        }

        public double Subtract(double n1, double n2) {
            return n1 - n2;
        }
    }

    [ComVisible(true)]
    [Guid("68f135a6-b995-4ecd-9324-70b9956f9591")]
    public interface ICalculator
    {
        object Calculate(string expression);
    }

    [Guid("47c976e0-c208-4740-ac42-41212d3c34f0"),
    InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface CalculatorEvents { }
}
