using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.APIs.Controllers.BaseApi;
using OrderManagementSystem.Core.Entities.Invoice_Model;
using OrderManagementSystem.Core.Repositories.Contract;
using OrderManagementSystem.Core.Services.Contract;

namespace OrderManagementSystem.APIs.Controllers.InvoiceController
{

	public class InvoicesController : BaseApiController
	{
		private readonly IInvoiceService _invoiceService;
		private readonly IUnitOfWork _unitOfWork;

		public InvoicesController(IInvoiceService invoiceService, IUnitOfWork unitOfWork)
		{
			_invoiceService = invoiceService;
			_unitOfWork = unitOfWork;
		}

		[Authorize(Roles ="Admin")]
		[HttpGet("{invoiceId}")]
		
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<Invoice>> GetInvoice(int invoiceId)
		{
			var invoice = await _invoiceService.GetInvoiceByIdAsync(invoiceId);
			if (invoice == null)
				return NotFound();

			return Ok(invoice);
		}

		[Authorize(Roles = "Admin")]
		// GET /api/invoices
		[HttpGet]
		
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IReadOnlyList<Invoice>>> GetAllInvoices()
		{
			var invoices = await _invoiceService.GetAllInvoicesAsync();
			if (invoices is null)
				return NotFound();
			return Ok(invoices);
		}
	}
}
