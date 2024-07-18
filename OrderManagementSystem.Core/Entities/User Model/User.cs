using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Core.Entities.User_Model
{
	public class User:IdentityUser
	{
		public int UserId { get; set; }
	public string DisplayName { get; set; }
		public string PasswordHash { get; set; }
		public string Role { get; set; } // Admin, Customer
	}
}
