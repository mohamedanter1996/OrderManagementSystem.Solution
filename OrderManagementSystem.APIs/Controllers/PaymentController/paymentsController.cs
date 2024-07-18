using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.APIs.Controllers.BaseApi;
using OrderManagementSystem.Core.Entities.Order_Model;
using OrderManagementSystem.Core.Repositories.Contract;
using OrderManagementSystem.Core.Services.Contract;
using Stripe;

namespace OrderManagementSystem.APIs.Controllers.PaymentController
{

	public class paymentsController : BaseApiController
	{
		private readonly IPaymentService _paymentService;
		private readonly IOrderService _orderService;
		private readonly IUnitOfWork _unitOfWork;

		private const string endpointSecret = "sk_test_51PPPrGRuTzI0mF0ZbImcUD5lpMhVYCH3BtPOG99Ws5wDsgBDa2z7higT1qN8UnHhdwwuW7cQ6X2h2kXRk9kQBmpU00WQXb155D";
		public paymentsController(IPaymentService paymentService, IOrderService orderService, IUnitOfWork unitOfWork)
		{
			_paymentService = paymentService;
			_orderService = orderService;
			_unitOfWork = unitOfWork;
		}
		[Authorize]
		[ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]

		[HttpPost("{orderId}")]
		public async Task<ActionResult<Order>> CreateOrUpdatePaymentIntent(string orderId)
		{

			var order = await _unitOfWork.Repository<Order>().GetByIdAsync(int.Parse(orderId));

			var PaymentIntent = await _paymentService.CreateOrUpdatePaymentIntent(order.PaymentIntentId, order.TotalAmount);
			if (PaymentIntent is null) return BadRequest();
			order.PaymentIntentId = PaymentIntent.Id;
			return Ok(order);
		}

		[HttpPost("webhook")]

		public async Task<IActionResult> WebHook()
		{
			var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

			var stripeEvent = EventUtility.ConstructEvent(json,
				Request.Headers["Stripe-Signature"], endpointSecret);

			var paymenyIntent = (PaymentIntent)stripeEvent.Data.Object;

			if (stripeEvent.Type == Events.PaymentIntentSucceeded)
			{
				await _paymentService.UpdateOrderStatus(paymenyIntent.Id, true);
			}
			else if (stripeEvent.Type == Events.PaymentIntentPaymentFailed)
			{
				await _paymentService.UpdateOrderStatus(paymenyIntent.Id, false);
			}
			return Ok();
		}

	}
}
