using System;
using System.Collections.Generic;
using System.Text;

namespace EmailFilter.Rules
{
    public interface IRule
    {
        bool IsMatched(Email email);
        void AddChildRules(params IRule[] rule);
    }
}
