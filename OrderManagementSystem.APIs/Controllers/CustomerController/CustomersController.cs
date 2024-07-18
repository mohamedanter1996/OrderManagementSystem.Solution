using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.APIs.Controllers.BaseApi;
using OrderManagementSystem.Core.Entities.Customer_Model;
using OrderManagementSystem.Core.Entities.Order_Model;
using OrderManagementSystem.Core.Entities.User_Model;
using OrderManagementSystem.Core.Repositories.Contract;
using OrderManagementSystem.Core.Services.Contract;
using OrderManagementSystem.Core.Utilities.CustomerDtoModel;
using OrderManagementSystem.Core.Utilities.UserDtoModel;

namespace OrderManagementSystem.APIs.Controllers.CustomerController
{
	
	public class CustomersController : BaseApiController
	{
		private readonly UserManager<User> _userManager;
		private readonly IAuthService _authService;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IOrderService _orderService;

		public CustomersController(UserManager<User> userManager, IAuthService authService,IUnitOfWork unitOfWork,IOrderService orderService)
        {
			_userManager = userManager;
			_authService = authService;
			_unitOfWork = unitOfWork;
			_orderService = orderService;
		}

        // POST /api/customers
        [HttpPost]
		
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<UserDto>> CreateCustomer([FromBody] CustomerDto customerDto)
		{
			var customer = new Customer()
			{
				Name = customerDto.name,
				Email = customerDto.Email,

			};

			_unitOfWork.Repository<Customer>().Add(customer);
			await _unitOfWork.CompleteAsync();
			var user = new User()
			{
				DisplayName = customer.Name,
				Email = customer.Email,
				Role = "Customer",
				PasswordHash = customerDto.Password,

			};

			var result = await _userManager.CreateAsync(user, customerDto.Password);
			if (!result.Succeeded) { return BadRequest(result.Errors.Select(E => E.Description)); }
			await _userManager.AddToRoleAsync(user, "Customer");
			return Ok(new UserDto() { DisplayName = user.DisplayName, Email = user.Email, Token = await _authService.CreateTokenAsync(user, _userManager) });
			
		}

		// GET /api/customers/{customerId}/orders
		[HttpGet("{customerId}/orders")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IReadOnlyList<Order>>> GetCustomerOrders(int customerId)
		{
			var orders = await _unitOfWork.Repository<Order>().GetAllWithFiltersAsync(o => o.CustomerId == customerId);
			if(orders is null) return NotFound();
			return Ok(orders);
		}
	}
}
