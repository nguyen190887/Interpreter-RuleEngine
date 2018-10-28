using System;
using System.Collections.Generic;
using System.Text;

namespace EmailFilter.Operators
{
    public class ContainsOperator : IOperator
    {
        public bool Evaluate(object source, object target)
        {
            if (!(source is string && target is string))
            {
                throw new ArgumentException("This operator only supports string");
            }

            if (source == null)
            {
                throw new ArgumentNullException(nameof(source), "Source cannot be null");
            }

            return ((string)source).Contains((string)target, StringComparison.OrdinalIgnoreCase);
        }
    }
}
