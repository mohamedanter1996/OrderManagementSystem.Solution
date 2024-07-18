using OrderManagementSystem.Core.Entities.Order_Model;
using OrderManagementSystem.Core.Entities.Product_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Core.Entities.OrderItem_Model
{
	public class OrderItem
	{
		public int OrderItemId { get; set; }
		public int Quantity { get; set; }
		public decimal UnitPrice { get; set; }
		public decimal Discount { get; set; }

		// Foreign key for Order
		public int OrderId { get; set; }

		// Navigation property
		public Order Order { get; set; }

		// Foreign key for Product
		public int ProductId { get; set; }

		// Navigation property
		public Product Product { get; set; }
	}
}
