using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.APIs.Controllers.BaseApi;
using OrderManagementSystem.Core.Entities.Customer_Model;
using OrderManagementSystem.Core.Entities.Invoice_Model;
using OrderManagementSystem.Core.Entities.Order_Model;
using OrderManagementSystem.Core.Repositories.Contract;
using OrderManagementSystem.Core.Services.Contract;
using OrderManagementSystem.Core.Utilities;
using OrderManagementSystem.Core.Utilities.CreateOrderDtoModel;


namespace OrderManagementSystem.APIs.Controllers.OrderController
{

	public class ordersController : BaseApiController
	{
		private readonly IOrderService _orderService;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IEmailSender _emailSender;
		private readonly IInvoiceService _invoiceService;

		public ordersController(IOrderService orderService, IUnitOfWork unitOfWork, OrderManagementSystem.Core.Services.Contract.IEmailSender emailSender,IInvoiceService invoiceService)
		{
			_orderService = orderService;
			_unitOfWork = unitOfWork;
			_emailSender = emailSender;
			_invoiceService = invoiceService;
		}
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<Invoice>> CreateOrder(CreateOrderDto createOrderDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}


			var order = await _orderService.CreateOrderAsync(createOrderDto.BuyerEmail,
																createOrderDto.PaymentMethod,
																createOrderDto.Items,
																createOrderDto.PaymentIntent);
			if (order == null)
			{
				return BadRequest("Failed to create order");
			}

			var invoice= new Invoice() { InvoiceDate = DateTime.Now,OrderId=order.OrderId,TotalAmount=order.TotalAmount };
			
			return Ok(invoice);


		}


		[HttpGet("{orderId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<Order>> GetOrder(int orderId)
		{
			var order = await _unitOfWork.Repository<Order>().GetByIdAsync(orderId);
			if (order == null) { return NotFound(); }
			return Ok(order);
		}

		// GET /api/orders
		[Authorize(Roles = "Admin")]
		[HttpGet]

		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IReadOnlyList<Order>>> GetAllOrders()
		{
			var orders = await _unitOfWork.Repository<Order>().GetAllAsync();
			if (orders == null) { return NotFound(); }
			return Ok(orders);
		}

		// PUT /api/orders/{orderId}/status
		[Authorize(Roles = "Admin")]
		[HttpPut("{orderId}/status")]

		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<Order>> UpdateOrderStatus(int orderId, [FromBody] OrderStatus status)
		{
			var order = await _unitOfWork.Repository<Order>().GetByIdAsync(orderId);
			if (order == null) { return NotFound(); }
			order.Status = status;
			_unitOfWork.Repository<Order>().Update(order);
		var result=	await _unitOfWork.CompleteAsync();
			if (result>=0) { 
				var customer= await _unitOfWork.Repository<Customer>().FirstOrDefaultAsync(c=>c.CustomerId==order.CustomerId);
				_emailSender.SendEmailAsync(customer.Email, "order update", "your order updated");
				return Ok(order); 
			
			}
			else { return BadRequest(); }
		}
	}
}
