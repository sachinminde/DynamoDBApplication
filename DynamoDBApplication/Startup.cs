using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using DynamoDBApplication.BusinessLogic;
using DynamoDBApplication.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamoDBApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddAWSService<IAmazonDynamoDB>();
            //services.AddAWSService<IAmazonS3>();
            services.AddSingleton<IDynamoDBContext, DynamoDBContext>();
            services.AddTransient<IDbTransaction, DbTransaction>();
            services.AddTransient<IProductRepository, ProductRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DynamoDBApplication", Version = "v1" });
            });


            //string accessKey = Configuration.GetValue<string>("ServiceConfiguration:AccessKey");
            //string secretAccessKey = Configuration.GetValue<string>("ServiceConfiguration:SecretAccessKey");

            //var credentials = new BasicAWSCredentials(accessKey, secretAccessKey);
            //var config = new AmazonDynamoDBConfig()
            //{
            //    RegionEndpoint = Amazon.RegionEndpoint.APSouth1
            //};

            //var client = new AmazonDynamoDBClient(credentials, config);

            //services.AddSingleton<IAmazonDynamoDB>(client);

            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DynamoDBApplication v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
