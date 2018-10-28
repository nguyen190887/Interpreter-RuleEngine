using EmailFilter.Rules;
using System;
using Xunit;

namespace EmailFilter.Tests
{
    public class OrRuleTests
    {
        [Fact]
        public void IsMatched_ShouldBeTrue_WhenFieldsMatchPredefinedRules()
        {
            var ruleSet = new IRule[]
            {
                new CompareRule("subject", "contains", "production"),
                new CompareRule("from", "equals", "prod@mail.com")
            };

            var andRule = new AndRule(ruleSet);

            var mockEmail = new Email
            {
                Subject = "Production build today",
                From = "prod@mail.com",
                To = "user@mail.com",
                Body = "Build was successfully"
            };

            Assert.True(andRule.IsMatched(mockEmail));
        }

        [Fact]
        public void IsMatched_ShouldBeFalse_WhenFieldsNotMatchPredefinedRules()
        {
            var ruleSet = new IRule[]
            {
                new CompareRule("subject", "contains", "production"),
                new CompareRule("from", "equals", "prod@mail.com")
            };

            var andRule = new AndRule(ruleSet);

            var mockEmail = new Email
            {
                Subject = "Production build today",
                From = "prod1@mail.com",
                To = "user@mail.com",
                Body = "Build was successfully"
            };

            Assert.False(andRule.IsMatched(mockEmail));
        }
    }
}
