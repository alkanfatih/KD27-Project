using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class ApplicationException : Exception
    {
        public ApplicationException(string messsage) : base(messsage)
        {
            
        }

        public ApplicationException(string messsage, Exception inner): base(messsage, inner)
        {
            
        }
    }
}
