using System;
using System.Collections.Generic;
using System.Text;

namespace EmailFilter.Operators
{
    public interface IOperator
    {
        bool Evaluate(object source, object target);   
    }
}
