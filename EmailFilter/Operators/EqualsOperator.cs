using System;
using System.Collections.Generic;
using System.Text;

namespace EmailFilter.Operators
{
    class EqualsOperator : IOperator
    {
        public bool Evaluate(object source, object target)
        {
            if (source is string && target is string)
            {
                return string.Equals((string)source, (string)target, StringComparison.OrdinalIgnoreCase);
            }

            return source == target;
        }
    }
}
