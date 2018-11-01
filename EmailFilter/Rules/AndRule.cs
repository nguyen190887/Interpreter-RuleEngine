using System;
using System.Collections.Generic;
using System.Text;

namespace EmailFilter.Rules
{
    public class AndRule : IRule
    {
        private readonly List<IRule> _rules;

        public AndRule()
        {
            _rules = new List<IRule>();
        }

        public AndRule(params IRule[] rules)
        {
            _rules = new List<IRule>(rules);
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

        public void AddChildRules(params IRule[] rules)
        {
            _rules.AddRange(rules);
        }
    }
}
