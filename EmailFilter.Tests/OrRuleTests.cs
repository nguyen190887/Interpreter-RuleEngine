using EmailFilter.Rules;
using System;
using Xunit;

namespace EmailFilter.Tests
{
    public class AndRuleTests
    {
        [Fact]
        public void IsMatched_ShouldBeTrue_WhenFieldsMatchPredefinedRules()
        {
            var ruleSet = new IRule[]
            {
                new CompareRule("subject", "contains", "issue"),
                new CompareRule("to", "startsWith", "24x7support")
            };

            var orRule = new OrRule(ruleSet);

            var mockEmail = new Email
            {
                Subject = "Concerning about the build",
                From = "client@mail.com",
                To = "24x7support-team@mail.com",
                Body = "Hey - I got an issue"
            };

            Assert.True(orRule.IsMatched(mockEmail));
        }

        [Fact]
        public void IsMatched_ShouldBeFalse_WhenFieldsNotMatchPredefinedRules()
        {
            var ruleSet = new IRule[]
            {
                new CompareRule("subject", "contains", "issues"),
                new CompareRule("to", "startsWith", "24x7support")
            };

            var orRule = new OrRule(ruleSet);

            var mockEmail = new Email
            {
                Subject = "Issue about the build",
                From = "client@mail.com",
                To = "24x7-support-team@mail.com",
                Body = "Hey - I got an issue"
            };

            Assert.False(orRule.IsMatched(mockEmail));
        }
    }
}
