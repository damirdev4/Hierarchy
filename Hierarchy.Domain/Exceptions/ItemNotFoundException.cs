using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hierarchy.Domain.Exceptions
{
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException()
        {
        }

        public ItemNotFoundException(string message)
            : base(message)
        {
        }

        public ItemNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
