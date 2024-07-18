using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Core.Utilities.RegistorDtoModel
{
	public class RegistorDto
	{
		public string DisplayName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }

		public string Role { get; set; } // Admin, Customer
	}
}
