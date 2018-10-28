using System;
using System.Collections.Generic;
using System.Text;

namespace EmailFilter.Operators
{
    public class OperatorFactory
    {
        public static IOperator Create(string opValue)
        {
            switch (opValue)
            {
                case "equals":
                    return new EqualsOperator();
                case "contains":
                    return new ContainsOperator();
                case "startsWith":
                    return new StartsWithOperator();
                default:
                    throw new NotSupportedException($"Operator {opValue} is not supported");
            }
        }
    }
}
