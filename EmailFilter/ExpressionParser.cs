using EmailFilter.Rules;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailFilter
{
    public class ExpressionParser
    {
        public RuleNode Parse(string expression)
        {
            /*
             * Sample:
             *   AND(Rule1, OR(Rule2, Rule3))
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

                if (current == ' ')
                {
                    continue;
                }
                else if (current == '(')
                {
                    ruleStack.PushRule(accumulator, level);
                    level++;
                    accumulator.Clear();
                }
                else if (current == ',')
                {
                    ruleStack.PushRule(accumulator, level);
                    accumulator.Clear();
                }
                else if (current == ')')
                {
                    var childRules = new List<RuleNode>();
                    if (accumulator.Length > 0)
                    {
                        childRules.Add(InitEntry(accumulator, level));
                        accumulator.Clear();
                    }

                    RuleNode topNode;
                    while ((topNode = ruleStack.Pop()).Level == level)
                    {
                        childRules.Add(topNode);
                    }

                    // Compose rule and update stack
                    RuleNode compositeRule = topNode;
                    compositeRule.Children = childRules;
                    ruleStack.Push(compositeRule);
                    level--;
                }
                else
                {
                    accumulator.Append(current);
                }
            }
            
            return ruleStack.Pop();
        }

        private RuleNode InitEntry(StringBuilder accumulator, int level)
        {
            return new RuleNode
            {
                Text = accumulator.ToString(),
                Level = level
            };
        }
    }

    public struct RuleNode
    {
        public IRule Rule { get; set; }
        public string Text { get; set; }
        public int Level { get; set; }
        public IEnumerable<RuleNode> Children { get; set; }
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
