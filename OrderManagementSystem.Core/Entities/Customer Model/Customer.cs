using OrderManagementSystem.Core.Entities.Order_Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Core.Entities.Customer_Model
{
	public class Customer
	{
		public int CustomerId { get; set; }
		public string Name { get; set; }

		[EmailAddress]
		public string Email { get; set; }

		// Navigation property
		public ICollection<Order> Orders { get; set; }
	}
}
