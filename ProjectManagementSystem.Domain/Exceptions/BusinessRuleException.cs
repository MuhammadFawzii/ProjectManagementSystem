using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Domain.Exceptions
{
    public class BusinessRuleException:Exception
    {
        public int StatusCode { get; }
        public BusinessRuleException(string message ,int statusCode):base(message)
        {
            StatusCode = statusCode; 
        }

    }
}
