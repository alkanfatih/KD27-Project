using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class BusinessRuleException : DomainException
    {
        public string RuleName { get; }
        public BusinessRuleException(string ruleName, string message) : base($"[Rule: {ruleName}] Message: {message}")
        {
            RuleName = ruleName;
        }
    }
}
