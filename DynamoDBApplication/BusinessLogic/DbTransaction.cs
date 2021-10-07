using DynamoDBApplication.Entities;
using DynamoDBApplication.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamoDBApplication.BusinessLogic
{
    
    public class DbTransaction : IDbTransaction
    {
        private IProductRepository productRepository;
        public DbTransaction(IProductRepository _productRepository)
        {
            productRepository = _productRepository;
        }

        public async Task<Product> GetProductById(string Id)
        {
            var result = await productRepository.GetProductById(Id);
            
            return result;
        }


        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var result = await productRepository.GetAllProducts();

            return result;
        }

        public async Task AddProduct( Product product)
        {
            await productRepository.AddProduct(product);
                       
        }

    }
}
