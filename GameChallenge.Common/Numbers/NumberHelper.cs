using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameChallenge.Common.Numbers
{
    public class NumberHelper : INumberHelper
    {
        private static readonly Random getrandom = new Random();
        public int GenerateRandomNumber(int min, int max)
        {
            lock (getrandom)
            {
                return getrandom.Next(min, max);
            }
        }
    }
}
