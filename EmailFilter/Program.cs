using System;
using System.IO;

namespace EmailFilter
{
    class Program
    {
        static void Main(string[] args)
        {
            string ruleSettings = File.ReadAllText("FilterRules.json");
            var labeller = new EmailLabeller(ruleSettings);

            // test 1
            var email1 = new Email
            {
                Subject = "Production Build",
                From = "prod@mail.com",
                To = "dummy@mail.com",
                Body = "body",
                Date = new DateTime(2018, 1, 1)
            };
            Console.WriteLine("Label of email 1: {0}", labeller.GetLabel(email1)); // 'Production'

            // test 2
            var email2 = new Email
            {
                Subject = "Production Build",
                From = "teamA@mail.com",
                To = "dummy@mail.com",
                Body = "body",
                Date = new DateTime(2018, 1, 1)
            };
            Console.WriteLine("Label of email 2: {0}", labeller.GetLabel(email2)); // 'None'

            // test 3
            var email3 = new Email
            {
                Subject = "Subject",
                From = "teamA@mail.com",
                To = "24x7support@mail.com",
                Body = "body",
                Date = new DateTime(2018, 1, 1)
            };
            Console.WriteLine("Label of email 3: {0}", labeller.GetLabel(email3)); // 'Urgent'
        }
    }
}
