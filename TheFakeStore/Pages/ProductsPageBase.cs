using Microsoft.AspNetCore.Components;
using TheFakeStore.Domain.Entities;
using TheFakeStore.Services.Abstract;

namespace TheFakeStore.Pages
{
    public class ProductsPageBase : ComponentBase
    {
        public List<Product> Products { get; set; }
        [Inject]
        public IHttpService _HttpService{ get; set; }


        protected override async Task OnInitializedAsync()
        {
            
            Products = await _HttpService.Get<List<Product>>(@"https://fakestoreapi.com/products");


            
        }


    }
}
