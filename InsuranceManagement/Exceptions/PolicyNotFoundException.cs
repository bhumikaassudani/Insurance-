using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace InsuranceManagement.Exception
{
    public class PolicyNotFoundException : SystemException
    {

        public PolicyNotFoundException(string message) : base(message)
        {
        }
    }
}

