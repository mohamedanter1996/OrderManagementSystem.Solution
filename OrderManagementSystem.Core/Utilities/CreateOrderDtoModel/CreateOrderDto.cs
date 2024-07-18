using OrderManagementSystem.Core.Entities.OrderItem_Model;
using Stripe;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Core.Utilities.CreateOrderDtoModel
{
	public class CreateOrderDto
	{
		[Required]
		public string BuyerEmail { get; set; }

		[Required]
		public string PaymentMethod { get; set; }

		[Required]
		public List<OrderItem> Items { get; set; }

		public PaymentIntent PaymentIntent { get; set; }
	}
}
