﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQDemo
{
    public class LinqConcepts
    {
        //Distinct numbers from list
        public static void DistinctNum()
        {
            var numbers= new List<int>() { 1,2,3,4,4,5,2,2,1,6,7,3};
            var distinctNum = numbers.Distinct().ToList();
        }
        public static void Filtering()
        {
            var nums = new List<int>() { 10, 1, 23, 2, 7, 8, 12, 3, 5, 11 };
            var evenNum=nums.Where(x=>x%2==0).ToList();
            var oddNum = nums.Where(x => x % 2 == 1).ToList();
        }
        public static void Sorting()
        {
            var nums = new List<int>() { 10, 1, 23, 2, 7, 8, 12, 3, 5, 11 };
            var ascendingNums=nums.OrderBy(x=>x).ToList();
            var descendingNums=nums.OrderByDescending(x=>x).ToList();
        }
        public static void GetFirstOrLastEle()
        {
            var numbers=new List<int>() { 1,2,3,4,5,6,7,89,9};
            var numbers2 = new List<int> {};
            var firstNum=numbers.First();
            //var firstNumInEmptySet=numbers2.First();
            var firstNum1=numbers.FirstOrDefault();
            var firstNumInEmptySet=numbers2.FirstOrDefault();

        }
        public static void TakeOrSkipLimitedItems()
        {
            var numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 89, 9 };
            var firstThree=numbers.Take(3).ToList();
            var lastThree = numbers.Skip(numbers.Count - 3).ToList();//.Where(x=>x>80).ToList();
            var test = numbers.Select(x => x * x).ToList();
        }
        public static void Main(string[] args)
        {
            DistinctNum();
            Filtering();
            Sorting();
            GetFirstOrLastEle();
            TakeOrSkipLimitedItems();
           
        }
    }
}
