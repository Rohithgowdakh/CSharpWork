using System;
using System.Security.Cryptography.X509Certificates;
namespace SPC
{
    class Calculator
    {
        static void Main(string[] args)
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
            double SD = CalculateSD(varience);
            Console.WriteLine($"\nStandard Deviation :{SD}");

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

            Console.WriteLine($"\n\nRange : {dataPoints.Max() - dataPoints.Min()}");

            //Calculate Upper Control Limit(UCL) & Lower Control Limit (LCL)
            double UCL = Math.Truncate(mean + (3 * SD));
            double LCL = Math.Truncate(mean - (3 * SD));
            Console.WriteLine($"\nUpper Control Limit :{UCL}");
            Console.WriteLine($"\nLower Control Limit :{LCL}");

            for (int i = 0; i < dataPoints.Length; i++)
            {
                if (dataPoints[i] > UCL || dataPoints[i] < LCL)
                {
                    Console.WriteLine($"\nData point {dataPoints[i]} Out of control ");
                }

            }
            Console.WriteLine("\nSPC Calculation Complete. Press any key to exit.");
            Console.ReadKey();
        }
        public static double CalculateMean(double[] dataPoints)
        {
            var mean = dataPoints.Average();
            return Math.Round(mean, 2);
        }

        public static double CalculateVarience(double[] dataPoints, double mean)
        {
            var varience = dataPoints.Select(eachData => Math.Pow(eachData - mean, 2)).Average();
            return Math.Round(varience, 2);
        }

        public static double CalculateSD(double varience)
        {
            var SD = Math.Sqrt(varience);
            return Math.Round(SD, 2);
        }

        public static double CalculateMedian(double[] dataPoints)
        {
            var sortedNum = dataPoints.OrderBy(x => x).ToArray();
            int mid = sortedNum.Length / 2;
            if (sortedNum.Length == 1)
            {
                return Math.Round(sortedNum[mid],2);
            }
            else
            {
                return Math.Round((sortedNum[mid + 1] + sortedNum[mid - 1]) / 2,2);
            }
        }

        public static double[] CalculateMode(double[] dataPoints)
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
            var orderedMode=mode.OrderByDescending(x => x.Value).ToArray();
            int highestFrequency = orderedMode[0].Value;
            var modes=orderedMode.Where(x=>x.Value==highestFrequency).Select(x=>x.Key).ToArray();   
            if(modes.Length == 1)
            {
                return [modes[0]];
            }
            else
            {
                return modes;
            }
            
        }
    }
}