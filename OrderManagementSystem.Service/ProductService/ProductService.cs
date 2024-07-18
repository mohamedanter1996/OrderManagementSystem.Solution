using OrderManagementSystem.Core.Entities.Product_Model;
using OrderManagementSystem.Core.Repositories.Contract;
using OrderManagementSystem.Core.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Service.ProductService
{
	public class ProductService : IProductService
	{
		private readonly IUnitOfWork _unitOfWork;

		public ProductService(IUnitOfWork unitOfWork)
        {
			_unitOfWork = unitOfWork;
		}
        public async Task<Product> AddProductAsync(Product product)
		{
			_unitOfWork.Repository<Product>().Add(product);

			await _unitOfWork.CompleteAsync();
			return product;

		}

		public async Task<IReadOnlyList<Product>> GetAllProductsAsync()
		{
			var products = await _unitOfWork.Repository<Product>().GetAllAsync();
			if (products == null) return null;
			return products;
		}

		public async Task<Product?> GetProductByIdAsync(int productId)
		{
			var product = await _unitOfWork.Repository<Product>().GetByIdAsync(productId);
			if(product == null) return null;
			return product;
		}

		public async Task<Product?> UpdateProductAsync( Product updatedProduct)
		{
			var existingProduct = await _unitOfWork.Repository<Product>().GetByIdAsync(updatedProduct.ProductId);
			if (existingProduct == null)
			{
				return null;
			}

			existingProduct.Name = updatedProduct.Name;
			existingProduct.Price = updatedProduct.Price;
			existingProduct.Stock = updatedProduct.Stock;

			_unitOfWork.Repository<Product>().Update(existingProduct);
			await _unitOfWork.CompleteAsync();

			return existingProduct;

		}
	}
}
