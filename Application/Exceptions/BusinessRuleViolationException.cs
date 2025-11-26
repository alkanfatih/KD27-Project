using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class BusinessRuleViolationException : Exception
    {
        public BusinessRuleViolationException(string message) : base(message)
        {
            
        }
        public BusinessRuleViolationException(string code, string message): base($"{code}: {message}")
        {
            Code = code;
        }
        public string Code { get; private set; }
    }
}
