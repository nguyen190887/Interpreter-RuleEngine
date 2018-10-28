using EmailFilter.Operators;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailFilter.Rules
{
    public class CompareRule : IRule
    {
        private readonly IDictionary<string, Func<Email, object>> _fieldMapping =
            new Dictionary<string, Func<Email, object>>
            {
                {"subject", email => email.Subject },
                { "from", email => email.From},
                { "to", email => email.To },
                { "date", email => email.Date }
            };

        private readonly Func<Email, object> _fieldGetter;

        private readonly IOperator _operator;

        private readonly string _target;

        public CompareRule(string field, string opValue, string target)
        {
            _fieldGetter = _fieldMapping[field];
            _operator = OperatorFactory.Create(opValue);
            _target = target;
        }

        public bool IsMatched(Email email)
        {
            return _operator.Evaluate(_fieldGetter(email), _target);
        }
    }
}
