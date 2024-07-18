using OrderManagementSystem.Core.Entities.Invoice_Model;
using OrderManagementSystem.Core.Repositories.Contract;
using OrderManagementSystem.Core.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Service.InvoiceService
{
	public class InvoiceService:IInvoiceService
	{
		private readonly IUnitOfWork _unitOfWork;

		public InvoiceService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<Invoice?> GetInvoiceByIdAsync(int invoiceId)
		{
			var invoice = await _unitOfWork.Repository<Invoice>().GetByIdAsync(invoiceId);
			return invoice;
		}

		public async Task<IReadOnlyList<Invoice>> GetAllInvoicesAsync()
		{
			var invoices = await _unitOfWork.Repository<Invoice>().GetAllAsync();
			return invoices.ToList();
		}

	}
}
