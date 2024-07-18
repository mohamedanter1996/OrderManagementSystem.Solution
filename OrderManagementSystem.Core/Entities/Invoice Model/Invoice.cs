using OrderManagementSystem.Core.Entities.Order_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Core.Entities.Invoice_Model
{
	public class Invoice
	{
		public int InvoiceId { get; set; }
		public DateTime InvoiceDate { get; set; }
		public decimal TotalAmount { get; set; }

		// Foreign key for Order
		public int OrderId { get; set; }

		// Navigation property
		public Order Order { get; set; }
	}
}
