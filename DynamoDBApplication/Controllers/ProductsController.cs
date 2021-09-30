using Amazon.DynamoDBv2.DataModel;
using DynamoDBApplication.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DynamoDBApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IDynamoDBContext _dynamoDbContext;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger, IDynamoDBContext dynamoDbContext)  
        {
            _dynamoDbContext = dynamoDbContext ?? throw new ArgumentNullException(nameof(dynamoDbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET: api/<ProductsController>
        [HttpGet]
        [Route("{Id}", Name = "GetProduct")]
        public async Task<ActionResult<Product>> GetProductById(string Id)
        {
            var product = await _dynamoDbContext.LoadAsync<Product>(Id);

            if (product == null)
            {
                _logger.LogError($"Product with id: {Id}, not found.");
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<ActionResult<Product>> GetAllProducts()
        {
            var condition = new List<ScanCondition>();
            var products = await _dynamoDbContext.ScanAsync<Product>(condition).GetRemainingAsync();

            if (products == null)
            {
                _logger.LogError($"Products not found.");
                return NotFound();
            }
            return Ok(products);
        }

        [HttpPost]
        [Route("AddProduct")]
        public async Task AddProduct([FromBody] Product product)
        {
            await _dynamoDbContext.SaveAsync(product);
        }

    }
}
