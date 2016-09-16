using System;
using System.Collections.Generic;

namespace Bridge_App
{
    public class RandomNumberCreate
    {
        public RandomNumberCreate()
        {
        }

        private long RandomNumbers(int expectedRunTime)
        {
            Random rnd = new Random();
            long number = rnd.Next(2, expectedRunTime);
           
            return number;
        }

        
        public List<long> FillListRandomNumber(long peopleNumber, int expectedRunTime)
        {
            List<long> peopleList = new List<long>();
            peopleList.Add(1);
            long a = 0;
            long i = 1;
            for (; i < peopleNumber + 1; i++)
            {
                a = RandomNumbers(expectedRunTime);
                if (a != 0 || a != 1)
                {
                    if (peopleList.Contains(a))
                    {
                        int b = peopleList.IndexOf(a);
                        peopleList.RemoveAt(b);
                        i = b;
                    }
                    else
                    {
                        peopleList.Add(a);
                    }
                }
                else
                {
                    a = RandomNumbers(expectedRunTime);
                }
            }
            peopleList.Sort();
            return peopleList;
        }
    }
}