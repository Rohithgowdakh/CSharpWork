//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SPCCalculator
//{
//    public class AttributeControlChart
//    {
//        public void AttributeCharts()
//        {
//            Console.WriteLine("Attribute Charts:");
//            Console.WriteLine("1. C-Chart");
//            Console.WriteLine("2. NP-Chart");
//            Console.WriteLine("3. P-Chart");
//            Console.WriteLine("4. U-Chart");
//            Console.WriteLine("5. Exit to main menu");
//            Console.WriteLine("Select a chart:");
//            int choice;
//            if (int.TryParse(Console.ReadLine(), out choice))
//            {
//                if (choice > 0 && choice < 6)
//                {
//                    switch (choice)
//                    {
//                        case 1: Console.WriteLine("You selected the C-Chart. Used for defect counts in a fixed sample size.");
//                                CalculateCChart();break;
//                        case 2: Console.WriteLine("You selected the NP-Chart. Used for the number of defectives in a fixed sample size.");
//                                CalculateNpChart();break;
//                        case 3: Console.WriteLine("You selected the P-Chart. Used for the proportion of defectives in a varying sample size.");
//                                CalculatePChart();break;
//                        case 4: Console.WriteLine("You selected the U-Chart. Used for defects per unit in a varying sample size.");
//                                CalculateUChart();break;
//                        case 5:Console.WriteLine("Returned to main menu");return;
//                        default:Console.WriteLine("Enter the choice in between 1 to 5 only");return;
//                    }
//                }
//                else { Console.WriteLine("Enter the valid choise:"); }
//            }
//            else { Console.WriteLine("Invalid Input Please try again\n"); }
//        }
//        public void CalculateCChart()
//        {
//            Console.WriteLine("Enter the Subgroup Size :");
//            int subgroupSize;
//            if (int.TryParse(Console.ReadLine(), out subgroupSize))
//            {
//                if (subgroupSize > 1 && subgroupSize < 32768)
//                {
//                    Console.WriteLine("Enter the number of inspection unit :");
//                    int inspectionUnit;
//                    if(int.TryParse(Console.ReadLine(),out inspectionUnit))
//                    {
//                        Console.WriteLine(""
//                    }

//                }
//                else { Console.WriteLine("Subgroup size must be between 1 and 32767"); }
//            }
//            else { Console.WriteLine("Invalid Input Please try again\n"); }

//        }
//        public void CalculateNpChart()
//        {

//        }
//        public void CalculatePChart()
//        {

//        }
//        public void CalculateUChart()
//        {

//        }
//    }
//}
