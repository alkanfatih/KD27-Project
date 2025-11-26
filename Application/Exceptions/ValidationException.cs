using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public IDictionary<string, string[]> Errors { get; }

        public ValidationException(IDictionary<string, string[]> errors) : base("One or more validation failures have occured.")
        {
            Errors = errors;
        }

        public ValidationException() : base("One or more validation failures have occured.")
        {
            Errors = new Dictionary<string, string[]>();
        }
    }
}
