using OrderManagementSystem.Core.Entities.Order_Model;
using OrderManagementSystem.Core.Entities.OrderItem_Model;
using OrderManagementSystem.Core.Repositories.Contract;
using OrderManagementSystem.Core.Services.Contract;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Service.OrderService
{
	public class OrderService : IOrderService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IPaymentService _paymentService;

		public OrderService(IUnitOfWork unitOfWork,IPaymentService paymentService)
        {
			_unitOfWork = unitOfWork;
			_paymentService = paymentService;
		}
        public async Task<Order?> CreateOrderAsync(string buyerEmail, string? paymentMethodId, List<OrderItem> orderItems, PaymentIntent paymentIntent)
		{
			var Customer = await _unitOfWork.Repository<OrderManagementSystem.Core.Entities.Customer_Model.Customer>().FirstOrDefaultAsync(C => C.Email == buyerEmail);
			var actualOrderItems = new List<OrderItem>();
			foreach (var item in orderItems)
			{
				if (item.Product.Stock >= item.Quantity)
				{
					actualOrderItems.Add(item);
				}
			}

			// 2. Calculate subtotal
			var subtotal = actualOrderItems.Sum(item => item.UnitPrice * item.Quantity);
			if (subtotal > 100)
			{
				
				subtotal = actualOrderItems.Sum(item => item.UnitPrice * 0.95m * item.Quantity);
			}
			if(subtotal > 200)
			{
				subtotal = actualOrderItems.Sum(item => item.UnitPrice * 0.9m * item.Quantity);
			}
			// 4. Create payment intent
			var orderpaymentIntent = await _paymentService.CreateOrUpdatePaymentIntent(paymentIntent.Id, subtotal);
			if (orderpaymentIntent is null) return null;

			// 5. Create order


			var order = new Order
			(
				subtotal,
				paymentMethodId,
				actualOrderItems,
				paymentIntent.Id,
				Customer
			);

			_unitOfWork.Repository<Order>().Add(order);

			// 6. Save to database
			var result = await _unitOfWork.CompleteAsync();
			if (result <= 0) return null;

			return order;
		}



		public async Task<Order?> GetOrderByIdForUserAsync(string buyerEmail, int orderId)
		{
			var Customer = await _unitOfWork.Repository<OrderManagementSystem.Core.Entities.Customer_Model.Customer>().FirstOrDefaultAsync(C => C.Email == buyerEmail);
			var order = await _unitOfWork.Repository<Order>().FirstOrDefaultAsync(O => O.OrderId == orderId && O.CustomerId == Customer.CustomerId);
			if(order is null) return null;
			return order;
		}

		public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
		{
			var Customer = await _unitOfWork.Repository<OrderManagementSystem.Core.Entities.Customer_Model.Customer>().FirstOrDefaultAsync(C => C.Email == buyerEmail);
			var orders = await _unitOfWork.Repository<Order>().GetAllWithFiltersAsync(O => O.CustomerId == Customer.CustomerId);
			if (orders is null) return null;
			return orders;
		}
	}
}
