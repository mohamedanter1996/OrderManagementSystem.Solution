using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagementSystem.Core.Entities.Invoice_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Repository._Data.Config
{
	internal class InvoiceConfigurations : IEntityTypeConfiguration<Invoice>
	{
		public void Configure(EntityTypeBuilder<Invoice> builder)
		{
			builder.HasKey(i => i.InvoiceId);
			builder.HasOne(i => i.Order)
			.WithOne(o => o.Invoice).HasForeignKey<Invoice>(i => i.InvoiceId);
	

		}
	}
}
