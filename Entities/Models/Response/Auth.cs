using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Response
{
    public class Auth
    {
        public required string Email { get; set; }
        public required string FirstName { get; set; }
		public required string LastName { get; set; }
		public required string Token { get; set; }
    }
}
