using OrderManagementSystem.Core.Entities.Product_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Core.Services.Contract
{
	public interface IProductService
	{
		Task<IReadOnlyList<Product>> GetAllProductsAsync();
		Task<Product?> GetProductByIdAsync(int productId);
		Task<Product> AddProductAsync(Product product);
		Task<Product?> UpdateProductAsync(Product updatedProduct);
	}
}
