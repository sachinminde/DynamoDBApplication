using Newtonsoft.Json;
using ProductInfoWebApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProductInfoWebApp
{
    public partial class _Default : Page
    {

        IEnumerable<Product> products = null;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            GetAllRecords();
        }

        protected void AddProduct_Click(object sender, EventArgs e)
        {
            Product product = new Product();
            product.Id = ProductId.Text;
            product.Name = ProductName.Text;

            var json = JsonConvert.SerializeObject(product);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:61192/api/Products/");
            var getProductsApi = hc.PostAsync("AddProduct", stringContent);
            getProductsApi.Wait();

            GetAllRecords();
        }

        protected void GetProduct_Click(object sender, EventArgs e)
        {
            string productId = ProductId.Text;

            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:61192/api/Products/");
            var getProductsApi =  hc.GetAsync(string.Format("{0}", productId));
            getProductsApi.Wait();

            var result = getProductsApi.Result;

            if (result.IsSuccessStatusCode)
            {
                var product = new Product();
                var records = result.Content.ReadAsAsync<Product>();
                records.Wait();

                ProductName.Text = records.Result.Name;
                ErrorMsg.Visible = false;
            }
            else
            {
                ProductName.Text = String.Empty;
                ErrorMsg.Visible = true;
                ErrorMsg.Text = "Record Not Found";
            }

        }

         public void GetAllRecords()
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:61192/api/Products/");
            var getProductsApi = hc.GetAsync("GetAllProducts");
            getProductsApi.Wait();

            var result = getProductsApi.Result;

            if (result.IsSuccessStatusCode)
            {
                var records = result.Content.ReadAsAsync<IList<Product>>();
                records.Wait();
                products = records.Result;
                ProductsGridView.DataSource = products;
                ProductsGridView.DataBind();
            }
        }
    }
}