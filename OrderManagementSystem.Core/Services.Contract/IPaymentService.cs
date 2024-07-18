using OrderManagementSystem.Core.Entities.Order_Model;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Core.Services.Contract
{
	public interface IPaymentService
	{
		Task<PaymentIntent?> CreateOrUpdatePaymentIntent(string paymentIntentId, decimal amount);
		Task<Order?> UpdateOrderStatus(string paymentIntentId, bool isPaid);


	}
}
