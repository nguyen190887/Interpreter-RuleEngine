using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterPatternDemo
{
    public class AndOrDemo
    {
        //Rule: Robert and John are male
        public static IExpression GetMaleExpression()
        {
            IExpression robert = new TerminalExpression("Robert");
            IExpression john = new TerminalExpression("John");
            return new OrExpression(robert, john);
        }

        //Rule: Julie is a married women
        public static IExpression GetMarriedWomanExpression()
        {
            IExpression julie = new TerminalExpression("Julie");
            IExpression married = new TerminalExpression("Married");
            return new AndExpression(julie, married);
        }

        public static void Run()
        {
            Console.WriteLine("## AndOr Demo");

            IExpression isMale = GetMaleExpression();
            IExpression isMarriedWoman = GetMarriedWomanExpression();

            Console.WriteLine("John is male? " + isMale.Interpret("John"));
            Console.WriteLine("Julie is a married women? " + isMarriedWoman.Interpret("Married Julie"));
            Console.WriteLine("Tri is male? " + isMale.Interpret("Tri"));
        }
    }

    public interface IExpression
    {
        bool Interpret(string context);
    }

    public class TerminalExpression : IExpression
    {
        private string data;

        public TerminalExpression(string data)
        {
            this.data = data;
        }

        public bool Interpret(string context)
        {
            if (context.Contains(data))
            {
                return true;
            }
            return false;
        }
    }

    public class OrExpression : IExpression
    {
        private readonly IExpression _expr1;
        private readonly IExpression _expr2;

        public OrExpression(IExpression expr1, IExpression expr2)
        {
            _expr1 = expr1;
            _expr2 = expr2;
        }

        public bool Interpret(string context)
        {
            return _expr1.Interpret(context) || _expr2.Interpret(context);
        }
    }

    public class AndExpression : IExpression
    {
        private readonly IExpression _expr1;
        private readonly IExpression _expr2;

        public AndExpression(IExpression expr1, IExpression expr2)
        {
            _expr1 = expr1;
            _expr2 = expr2;
        }

        public bool Interpret(string context)
        {
            return _expr1.Interpret(context) && _expr2.Interpret(context);
        }
    }
}
