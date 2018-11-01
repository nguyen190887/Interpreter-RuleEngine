using System;
using System.Collections.Generic;
using System.Text;

namespace EmailFilter.Rules
{
    //todo: create CompositeRule as base class of AndRule and OrRule
    public class OrRule : IRule
    {
        private readonly List<IRule> _rules;

        public OrRule()
        {
            _rules = new List<IRule>();
        }

        public OrRule(params IRule[] rules)
        {
            _rules = new List<IRule>(rules);
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

        public void AddChildRules(params IRule[] rule)
        {
            _rules.AddRange(rule);
        }
    }
}
