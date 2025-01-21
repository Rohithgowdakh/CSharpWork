using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPCCalculator
{
    public class SimpleCalculator
    {
        public void SimpleSPCCalculator()
        {
            try
            {
                Console.WriteLine("Statistical Process Control (SPC) Calculator");
                Console.WriteLine("=============================================");

                //Data Points
                Console.WriteLine("Enter the data points separated by comma(e.g, 2.4,4.5,4.4,66.3...) :");
                string input = Console.ReadLine();
                double[] dataPoints = input.Split(',').Select(double.Parse).ToArray();

                //Calculate Mean
                double mean = CalculateMean(dataPoints);
                Console.WriteLine($"\nMean (Average) :{mean}");

                //Calculate Varience
                double varience = CalculateVarience(dataPoints, mean);
                Console.WriteLine($"\nVarience :{varience}");

                //Calculate Standard Deviation
                double stdDeviation = CalculateSD(varience);
                Console.WriteLine($"\nStandard Deviation :{stdDeviation}");

                //Calculate Median
                double median = CalculateMedian(dataPoints);
                Console.WriteLine($"\nMedian :{median}");

                //Calculate Mode
                double[] mode = CalculateMode(dataPoints);
                Console.Write("\nMode(s) : ");
                foreach (var item in mode)
                {
                    Console.Write(item + ",");
                }


                //calculate Range
                double range = Math.Round(dataPoints.Max() - dataPoints.Min(), 2);
                Console.WriteLine($"\n\nRange : {range}");

                //Calculate Upper Control Limit(UCL) & Lower Control Limit (LCL)
                double ucl = Math.Truncate(mean + (3 * stdDeviation));
                double lcl = Math.Truncate(mean - (3 * stdDeviation));
                Console.WriteLine($"\nUpper Control Limit :{ucl}");
                Console.WriteLine($"\nLower Control Limit :{lcl}");

                for (int i = 0; i < dataPoints.Length; i++)
                {
                    if (dataPoints[i] > ucl || dataPoints[i] < lcl)
                    {
                        Console.WriteLine($"\nData point {dataPoints[i]} Out of control ");
                    }

                }
                Console.WriteLine("----------------------------------------------------------------\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public double CalculateMean(double[] dataPoints)
        {
            try
            {
                var mean = dataPoints.Average();
                return Math.Round(mean, 2);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public double CalculateVarience(double[] dataPoints, double mean)
        {
            try
            {
                var varience = dataPoints.Select(eachData => Math.Pow(eachData - mean, 2)).Average();
                return Math.Round(varience, 2);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public double CalculateSD(double varience)
        {
            try
            {

                var SD = Math.Sqrt(varience);
                return Math.Round(SD, 2);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public double CalculateMedian(double[] dataPoints)
        {
            try
            {
                var sortedNum = dataPoints.OrderBy(x => x).ToArray();
                int mid = sortedNum.Length / 2;
                if (sortedNum.Length == 1)
                {
                    return Math.Round(sortedNum[mid], 2);
                }
                else
                {
                    return Math.Round((sortedNum[mid + 1] + sortedNum[mid - 1]) / 2, 2);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public double[] CalculateMode(double[] dataPoints)
        {
            try
            {
                Dictionary<double, int> mode = new Dictionary<double, int>();
                for (int i = 0; i < dataPoints.Length; i++)
                {
                    if (!mode.ContainsKey(dataPoints[i]))
                    {
                        mode.Add(dataPoints[i], 1);
                    }
                    else
                    {
                        mode[dataPoints[i]]++;
                    }
                }
                var orderedMode = mode.OrderByDescending(x => x.Value).ToArray();
                int highestFrequency = orderedMode[0].Value;
                var modes = orderedMode.Where(x => x.Value == highestFrequency).Select(x => x.Key).ToArray();
                if (modes.Length == 1)
                {
                    return [modes[0]];
                }
                else
                {
                    return modes;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
