using EmailFilter.Rules;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailFilter
{
    public class FilterRule
    {
        public IRule Rule { get; set; }
        public string Label { get; set; }
    }
}
