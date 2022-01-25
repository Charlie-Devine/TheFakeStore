using Microsoft.AspNetCore.Components;
using TheFakeStore.Domain.Entities;
using TheFakeStore.Services.Abstract;

namespace TheFakeStore.Pages
{
    public class AddProductPageBase : ComponentBase
    {

        [Inject]
        public IHttpService _HttpService { get; set; }

        public Product product { get; set; } = new Product();

        public async void HandleValidSubmit()
        {
            await _HttpService.Post<Product>("https://fakestoreapi.com/products", product);
        }


        public void AddPrice(ChangeEventArgs e)
        {
            var toDecimal = System.Convert.ToDecimal(e.Value);
            product.Price = toDecimal;
        }



    }
}
