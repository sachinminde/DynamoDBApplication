using DynamoDBApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamoDBApplication.BusinessLogic
{
    public interface IDbTransaction
    {
        Task<Product> GetProductById(string Id);
        Task<IEnumerable<Product>> GetAllProducts();
        Task AddProduct(Product product);
    }
}
