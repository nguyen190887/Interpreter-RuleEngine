using EmailFilter.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EmailFilter
{
    public class RuleParser
    {
        public IRule Parse(string expression)
        {
            /*
             * Sample:
             *   AND(Rule1, OR(Rule2, Rule3), AND(Rule4, OR(Rule5, Rule6)))
             *
             * Rule details:
             *   Subject[contains='prod']
             */

            Stack<RuleNode> ruleStack = new Stack<RuleNode>();

            int level = 0;
            StringBuilder accumulator = new StringBuilder();
            for (int i = 0; i < expression.Length; i++)
            {
                char current = expression[i];
                
                if (current == '(')
                {
                    if (accumulator.Length > 0)
                    {
                        ruleStack.Push(InitEntry(accumulator, level));
                        level++;
                        accumulator.Clear();
                    }
                }
                else if (current == ',')
                {
                    if (accumulator.Length > 0)
                    {
                        ruleStack.Push(InitEntry(accumulator, level));
                        accumulator.Clear();
                    }
                }
                else if (current == ')')
                {
                    var childRules = new List<RuleNode>();
                    if (accumulator.Length > 0)
                    {
                        childRules.Add(InitEntry(accumulator, level));
                        accumulator.Clear();
                    }

                    // Pop until meeting parent node
                    RuleNode top;
                    while ((top = ruleStack.Pop()).Level == level)
                    {
                        childRules.Add(top);
                    }

                    // Compose rule and update stack
                    RuleNode compositeRule = top;
                    compositeRule.Rule.AddChildRules(childRules.Select(x => x.Rule).ToArray());
                    ruleStack.Push(compositeRule);
                    level--;
                }
                else
                {
                    accumulator.Append(current);
                }
            }

            return ruleStack.Any() ? ruleStack.Pop().Rule : null;
        }

        private IRule InitRule(string ruleText)
        {
            switch (ruleText)
            {
                case "AND":
                    return new AndRule();
                case "OR":
                    return new OrRule();
                default:
                    const string rulePattern = "(\\w+)\\[([a-z]+)='(.+)'\\]";
                    var regex = new Regex(rulePattern, RegexOptions.IgnoreCase);
                    var match = regex.Match(ruleText);

                    if (!match.Success || match.Groups.Count < 4)
                    {
                        return null;
                    }

                    return new CompareRule(match.Groups[1].Value, match.Groups[2].Value, match.Groups[3].Value);
            }
        }

        private RuleNode InitEntry(StringBuilder accumulator, int level)
        {
            string ruleText = accumulator.ToString().Trim();
            return new RuleNode
            {
                Text = ruleText,
                Rule = InitRule(ruleText),
                Level = level
            };
        }
    }

    public struct RuleNode
    {
        public IRule Rule { get; set; }
        public string Text { get; set; }
        public int Level { get; set; }
    }

    public static class RuleStackExtensions
    {
        public static void PushRule(this Stack<RuleNode> stack, StringBuilder accumulator, int level)
        {
            if (accumulator.Length > 0)
            {
                stack.Push(new RuleNode
                {
                    Text = accumulator.ToString(),
                    Level = level
                });
            }
        }
    }
}
