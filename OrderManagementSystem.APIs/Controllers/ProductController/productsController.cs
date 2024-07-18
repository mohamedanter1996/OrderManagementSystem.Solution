using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Core.Entities.Product_Model;
using OrderManagementSystem.Core.Repositories.Contract;
using OrderManagementSystem.Core.Services.Contract;

namespace OrderManagementSystem.APIs.Controllers.ProductController
{
	[Route("api/[controller]")]
	[ApiController]
	public class productsController : ControllerBase
	{
		private readonly IProductService _productService;
		private readonly IUnitOfWork _unitOfWork;

		public productsController(IProductService productService,IUnitOfWork unitOfWork)
        {
			_productService = productService;
			_unitOfWork = unitOfWork;
		}

        [HttpGet]
		
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IReadOnlyList<Product>>> GetAllProducts()
		{
			var products = await _productService.GetAllProductsAsync();
			if (products == null) { return NotFound(); }
			return Ok(products);
		}

		// GET /api/products/{productId}
		[HttpGet("{productId}")]
		
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<Product>> GetProduct(int productId)
		{
			var product = await _productService.GetProductByIdAsync(productId);
			if (product == null) { return NotFound(); }
			return Ok(product);
		}

		[Authorize(Roles = "Admin")]
		// POST /api/products
		[HttpPost]
		
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
		{
	var productAdded =	await _productService.AddProductAsync(product);
			if (productAdded == null) { return BadRequest(); }
			return Ok(productAdded);
		}
		[HttpPut("{productId}")]
		[Authorize(Roles = "Admin")]
		public async Task<ActionResult<Product>> UpdateProduct(int productId, Product updatedProduct)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			var product = await _productService.GetProductByIdAsync(productId);
			if (product == null) { return NotFound(); }
			product = updatedProduct;
		var productFinalApdate = await _productService.UpdateProductAsync(product);
			if (productFinalApdate is null)
				return NotFound();

			await _unitOfWork.CompleteAsync();

			return Ok(productFinalApdate);
		}
	}
}
