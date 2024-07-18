using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Core.Entities.User_Model;
using OrderManagementSystem.Core.Services.Contract;
using OrderManagementSystem.Core.Utilities.LoginDtoModel;
using OrderManagementSystem.Core.Utilities.RegistorDtoModel;
using OrderManagementSystem.Core.Utilities.UserDtoModel;

namespace OrderManagementSystem.APIs.Controllers.UserController
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly IAuthService _authService;

		public UsersController(UserManager<User> userManager, SignInManager<User> signInManager, IAuthService authService)
        {
			_userManager = userManager;
			_signInManager = signInManager;
			_authService = authService;
		}
        [HttpPost("register")]
		
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<UserDto>> Register([FromBody] RegistorDto userModel)
		{

			var user = new User()
			{
				DisplayName = userModel.DisplayName,
				Email = userModel.Email,
				Role = userModel.Role,
				PasswordHash = userModel.Password,

			};

			var result = await _userManager.CreateAsync(user, userModel.Password);
			if (!result.Succeeded) { return BadRequest(result.Errors.Select(E => E.Description)); }
			await _userManager.AddToRoleAsync(user, userModel.Role);
			return Ok(new UserDto() { DisplayName = user.DisplayName, Email = user.Email, Token = await _authService.CreateTokenAsync(user, _userManager) });
		}

		// POST /api/users/login
		[HttpPost("login")]
		
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<UserDto>> Login([FromBody] LoginDtoModel model)
		{
			var User = await _userManager.FindByEmailAsync(model.Email);
			if (User is null)
			{
				return Unauthorized();
			}
			var result = await _signInManager.CheckPasswordSignInAsync(User, model.Password, false);

			if (!result.Succeeded)
			{
				return Unauthorized();
			}

			return Ok(new UserDto()
			{
				DisplayName = User.DisplayName,
				Email = User.Email,
				Token = await _authService.CreateTokenAsync(User, _userManager)
			});
			
		}
	}
}
