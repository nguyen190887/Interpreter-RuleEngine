using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace EmailFilter
{
    public class EmailLabeller
    {
        private IList<FilterRule> _ruleSet;

        public EmailLabeller(string ruleSettings)
        {
            _ruleSet = LoadRules(ruleSettings);
        }

        public string GetLabel(Email email)
        {
            var foundEntry = _ruleSet.FirstOrDefault(x => x.Rule.IsMatched(email));
            return foundEntry == null ? "None" : foundEntry.Label;
        }

        private IList<FilterRule> LoadRules(string ruleSettings)
        {
            var parser = new RuleParser();
            var ruleEntries = JsonConvert.DeserializeObject<List<RuleSetting>>(ruleSettings);

            return ruleEntries.Select(x => new FilterRule
            {
                Rule = parser.Parse(x.Statement),
                Label = x.Label
            }).ToList();
        }

        public class RuleSetting
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Statement { get; set; }
            public string Label { get; set; }
        }
    }
}
