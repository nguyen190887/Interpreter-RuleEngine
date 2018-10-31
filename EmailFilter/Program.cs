using System;

namespace EmailFilter
{
    class Program
    {
        static void Main(string[] args)
        {
            string expression = "AND(Rule1, OR(Rule2, Rule3), AND(Rule4, OR(Rule5, Rule6)))";
            var parser = new ExpressionParser();
            var root = parser.Parse(expression);
        }
    }
}
