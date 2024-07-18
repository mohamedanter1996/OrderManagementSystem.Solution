using OrderManagementSystem.Core.Entities.Order_Model;
using OrderManagementSystem.Core.Entities.OrderItem_Model;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Core.Services.Contract
{
	public interface IOrderService
	{
		public Task<Order?> CreateOrderAsync(string buyerEmail, string? paymentMethodId,List<OrderItem> orderItems,PaymentIntent paymentIntent);

		public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);

		public Task<Order?> GetOrderByIdForUserAsync(string buyerEmail, int orderId);


	}
}
