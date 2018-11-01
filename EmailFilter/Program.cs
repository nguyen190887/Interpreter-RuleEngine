using System;

namespace EmailFilter
{
    class Program
    {
        static void Main(string[] args)
        {
            //string expression = "AND(Rule1, OR(Rule2, Rule3), AND(Rule4, OR(Rule5, Rule6)))";
            string expression = "AND(Subject[contains='production build'], From[equals='prod@mail.com'], OR(To[contains='user1'], To[startsWith='user2']))";
            var parser = new ExpressionParser();
            var root = parser.Parse(expression);
        }
    }
}
