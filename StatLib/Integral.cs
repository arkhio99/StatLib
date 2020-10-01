using System;
using System.Collections.Generic;
using System.Text;

namespace StatLib
{
    public static class IntegralCalculation
    {
        public static double CalculateIntegralByDifferentTrapeze(Func<double, double> function, double b, double a, double width)
        {
            double widthOfLastTrapeze = (b - a) % width;
            int its = (int)((b - a - widthOfLastTrapeze) / width);
            double leftEdge = a;
            double result = 0;
            for (int i = 0; i < its; i++)
            {
                result += (function(leftEdge) + function(leftEdge + width)) / 2 * width;
                leftEdge += width;
            }

            result += (function(leftEdge) + function(leftEdge + widthOfLastTrapeze)) / 2 * widthOfLastTrapeze;

            return result;
        }
    }
}
