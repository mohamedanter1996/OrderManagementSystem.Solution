using Microsoft.AspNetCore.Identity;
using OrderManagementSystem.Core.Entities.User_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Core.Services.Contract
{
	public interface IAuthService
	{
		Task<string> CreateTokenAsync(User user, UserManager<User> userManager);
	}
}
