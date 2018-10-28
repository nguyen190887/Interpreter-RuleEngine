using System;
using System.Collections.Generic;
using System.Text;

namespace EmailFilter.Operators
{
    class EqualsOperator : IOperator
    {
        public bool Evaluate(object source, object target)
        {
            return source == target;
        }
    }
}
