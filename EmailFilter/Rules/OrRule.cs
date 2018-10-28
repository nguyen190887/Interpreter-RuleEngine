using System;
using System.Collections.Generic;
using System.Text;

namespace EmailFilter.Rules
{
    public class OrRule : IRule
    {
        private readonly IRule[] _rules;

        public OrRule(params IRule[] rules)
        {
            _rules = rules;
        }

        public bool IsMatched(Email email)
        {
            bool initial = false;
            foreach (IRule rule in _rules)
            {
                initial = initial || rule.IsMatched(email);
                if (initial)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
