using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreProject.BLL.Infrastructure
{
    public class OperationDetails
    {
        public OperationDetails(bool succeded, string message, string prop)
        {
            Succeded = succeded;
            Message = message;
            Property = prop;
        }
        public bool Succeded { get; private set; }
        public string Message { get; private set; }
        public string Property { get; private set; }
    }
}
