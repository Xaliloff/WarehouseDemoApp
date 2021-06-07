using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventContracts.Event
{
    public class UserCreated
    {
        public string FirstName { get; set; }
        public string Role { get; set; }
        public string Id { get; set; }
    }
}
