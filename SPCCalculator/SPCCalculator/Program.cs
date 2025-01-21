using SPCCalculator;
using System;
using System.Security.Cryptography.X509Certificates;
namespace SPC
{

    class Program
    {
        public static void Main(string[] args)
        {
            Calculator calculator = new Calculator();
            calculator.Calculators();
        }
    }
}