using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Core.Entities.Customer_Model;
using OrderManagementSystem.Core.Entities.Invoice_Model;
using OrderManagementSystem.Core.Entities.Order_Model;
using OrderManagementSystem.Core.Entities.OrderItem_Model;
using OrderManagementSystem.Core.Entities.Product_Model;
using OrderManagementSystem.Core.Entities.User_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Repository._Data
{
	public class OrderManagementDbContext : DbContext
	{
        public OrderManagementDbContext(DbContextOptions<OrderManagementDbContext> options):base(options)
        {
            
        }
        public DbSet<Customer> Customers { get; set; }

		public DbSet<Order> Orders { get; set; }

		public DbSet<OrderItem> OrderItems { get; set; }

		public DbSet<Invoice> Invoices { get; set; }

		public DbSet<Product> Products { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}

	}
}
