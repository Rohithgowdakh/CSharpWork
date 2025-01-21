using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPCCalculator
{
    public class VaribleControlChart
    {
        List<double[]> _subgroups=new List<double[]>();
        List<double> _subgroupMean = new List<double>();
        List<double> _subgroupRange = new List<double>();
        int SubgroupSize = 0;
        double A2=0.0, D3=0.0, D4=0.0;
        public void GetInputDataPoints()
        {
            try
            {
                Console.WriteLine("Enter the number of subgroups :");
                int numberOfSubgroups = int.Parse(Console.ReadLine());
                if (numberOfSubgroups <= 1 && numberOfSubgroups > 5) { Console.WriteLine("Error: Number of subgroups must be between 2 and 5."); return; }
                Console.WriteLine("Enter the size of each subgroup :");
                SubgroupSize = int.Parse(Console.ReadLine());
                if (SubgroupSize <= 1 && SubgroupSize > 6) { Console.WriteLine("Error: Subgroup size must be between 2 and 6."); return; }
                for (int i = 0; i < numberOfSubgroups; i++)
                {
                    Console.WriteLine($"\nEnter {SubgroupSize} data points for Subgroup {i + 1}, separated by spaces:");
                    //Convert the string into double array
                    double[] subgroup = Console.ReadLine()
                                        .Split(' ')
                                        .Select(double.Parse)
                                        .ToArray();
                    if (subgroup.Length != SubgroupSize)
                    {
                        Console.WriteLine($"Error: Subgroup {i + 1} must have exactly {SubgroupSize} data points.");
                        return;
                    }
                    _subgroups.Add(subgroup);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        public void SubgroupSizeConstants()
        {
            //Constants Initialization Based On Subgroup Size
            try
            {
                switch (SubgroupSize)
                {
                    case 2: A2 = 1.88; D3 = 0.0; D4 = 3.27; break;
                    case 3: A2 = 1.02; D3 = 0.0; D4 = 2.57; break;
                    case 4: A2 = 0.73; D3 = 0.0; D4 = 2.28; break;
                    case 5: A2 = 0.58; D3 = 0.0; D4 = 2.11; break;
                    case 6: A2 = 0.48; D3 = 0.0; D4 = 2.00; break;
                    default:
                        Console.WriteLine("Error: Subgroup size must be between 2 and 6.");
                        return;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void VariableChartCalculation()
        {
            try
            {
                for (int i = 0; i < _subgroups.Count; i++)
                {
                    double mean = _subgroups[i].Average();
                    double range = _subgroups[i].Max() - _subgroups[i].Min();

                    _subgroupMean.Add(mean);
                    _subgroupRange.Add(range);
                }
                //Calculate grand Mean and R-Bar
                double grandMean = _subgroupMean.Average();
                double rBar = _subgroupRange.Average();

                //For Getting Constant Values
                SubgroupSizeConstants();

                //Calculate limits for X-Bar
                double xBarUCL = grandMean + A2 * rBar;
                double xBarLCL = grandMean - A2 * rBar;

                //Calculate limits for R-Bar 
                double rBarUCL = rBar * D4;
                double rBarLCL = rBar * D3;

                //Final Output
                Console.WriteLine("\n X-Bar Chart :");
                Console.WriteLine($"X - bar : {grandMean:F2}");
                Console.WriteLine($"X - bar UCL : {xBarUCL:F2}");
                Console.WriteLine($"X - bar LCL : {xBarLCL:F2}");

                Console.WriteLine("\n R-Bar Chart :");
                Console.WriteLine($"R - bar : {rBar:F2}");
                Console.WriteLine($"R - bar UCL : {rBarUCL:F2}");
                Console.WriteLine($"R - bar LCL : {rBarLCL:F2}");

                Console.WriteLine("\nFinal Control Chart Status : ");
                for (int i = 0; i < _subgroups.Count; i++)
                {
                    Console.WriteLine($"\nSubgroup {i + 1}:");
                    Console.WriteLine($"Mean : {_subgroupMean[i]:F2} {((_subgroupMean[i] > xBarUCL || _subgroupMean[i] < xBarLCL) ? " OUT of control" : " within control")}");
                    Console.WriteLine($"Range : {_subgroupRange[i]:F2} {((_subgroupRange[i] > rBarUCL || _subgroupRange[i] < rBarLCL) ? " OUT of control" : " within control")}");
                }
                Console.WriteLine("----------------------------------------------------------------\n");
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}
