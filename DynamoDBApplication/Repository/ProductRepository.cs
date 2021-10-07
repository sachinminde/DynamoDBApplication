using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using DynamoDBApplication.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamoDBApplication.Repository
{
    public class ProductRepository : IProductRepository
    {
        private DynamoDBContext dynamoDBContext;
        private AmazonDynamoDBClient dynamoDBClient;
        private IConfiguration configuration;
        private string accessKey, secretAccessKey;

        public ProductRepository(IDynamoDBContext _dynamoDBContext, IConfiguration _configuration)
        {
            configuration = _configuration;
            accessKey = configuration.GetValue<string>("ServiceConfiguration:AccessKey");
            secretAccessKey = configuration.GetValue<string>("ServiceConfiguration:SecretAccessKey");
            dynamoDBClient = new AmazonDynamoDBClient(accessKey, secretAccessKey, RegionEndpoint.APSouth1);
            dynamoDBContext = new DynamoDBContext(dynamoDBClient);
        }

        // get product by id
        public async Task<Product> GetProductById(string Id)
        {
            try
            {
                var product = await dynamoDBContext.LoadAsync<Product>(Id);

                return product;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        // get all products
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            try
            {
                var condition = new List<ScanCondition>();

                var products = await dynamoDBContext.ScanAsync<Product>(condition).GetRemainingAsync();

                return products;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        // add product
        public async Task AddProduct(Product product)
        {
            try
            {
                await dynamoDBContext.SaveAsync(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                
            }
        }
    }
}
