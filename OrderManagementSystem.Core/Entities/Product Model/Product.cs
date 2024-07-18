using OrderManagementSystem.Core.Entities.OrderItem_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Core.Entities.Product_Model
{
	public class Product
	{
		public int ProductId { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public int Stock { get; set; }

		// Navigation property
		public ICollection<OrderItem> OrderItems { get; set; }
	}
}
