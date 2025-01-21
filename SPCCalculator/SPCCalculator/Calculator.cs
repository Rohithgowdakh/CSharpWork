using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPCCalculator
{
    public class Calculator
    {
        public void Calculators()
        {
            try
            {
                SimpleCalculator calculator = new SimpleCalculator();
                VaribleControlChart chart = new VaribleControlChart();
                bool isTrue = true;
                while (isTrue)
                {
                    Console.WriteLine("Select the Calculator :\n");
                    Console.WriteLine("1. Simple SPC Calculator");
                    Console.WriteLine("2. Variable Chart Calculator");
                    Console.WriteLine("3. Exit");
                    Console.WriteLine("\nEnter Your Choice :");
                    int choice;
                    if (int.TryParse(Console.ReadLine(), out choice))
                    {
                        if (choice > 0 && choice <= 3)
                        {
                            switch (choice)
                            {
                                case 1: calculator.SimpleSPCCalculator(); break;
                                case 2:
                                    chart.GetInputDataPoints();
                                    chart.VariableChartCalculation(); break;
                                case 3:
                                    Console.WriteLine("Press Any Key To Exit...");
                                    Console.ReadKey();
                                    isTrue = false;
                                    break;

                            }
                        }
                        else { Console.WriteLine("Enter the valid choise:"); }
                    }
                    else { Console.WriteLine("Invalid Input Please try again\n"); }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
