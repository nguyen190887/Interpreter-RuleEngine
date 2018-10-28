using System;
using System.Collections.Generic;
using System.Text;

namespace EmailFilter.Rules
{
    public class AndRule : IRule
    {
        private readonly IRule[] _rules;

        public AndRule(params IRule[] rules)
        {
            _rules = rules;
        }

        public bool IsMatched(Email email)
        {
            bool initial = true;
            foreach (IRule rule in _rules)
            {
                initial = initial && rule.IsMatched(email);
                if (!initial)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
