using OrderManagementSystem.Core.Entities.Customer_Model;
using OrderManagementSystem.Core.Entities.Invoice_Model;
using OrderManagementSystem.Core.Entities.OrderItem_Model;
using OrderManagementSystem.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Core.Entities.Order_Model
{
	public class Order
	{
		public Order(decimal totalAmount, string paymentMethod, ICollection<OrderItem> orderItems, string paymentIntentId, Customer customer)
		{
			TotalAmount = totalAmount;
			PaymentMethod = paymentMethod;
			Customer = customer;
			OrderItems = orderItems;
			PaymentIntentId = paymentIntentId;
		}

		public int OrderId { get; set; }
		public DateTime OrderDate { get; set; }
		public decimal TotalAmount { get; set; }
		public string PaymentMethod { get; set; }
		public OrderStatus Status { get; set; }

		// Foreign key for Customer
		public int CustomerId { get; set; }

		// Navigation property
		public Customer Customer { get; set; }

		// Navigation property
		public ICollection<OrderItem> OrderItems { get; set; }

		// Navigation property
		public Invoice Invoice { get; set; }

		public string PaymentIntentId { get; set; }
	}
}
