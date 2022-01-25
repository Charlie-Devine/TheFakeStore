using Microsoft.AspNetCore.Components;
using TheFakeStore.Domain.Entities;
using TheFakeStore.Services.Abstract;

namespace TheFakeStore.Pages
{
    public class ProductPageBase : ComponentBase
    {
        [Inject]
        public IHttpService _HttpService { get; set; }

        public Product product { get; set; }


        public async void OnProductChange(ChangeEventArgs e)
        {
            try
            {
                product = await _HttpService.Get<Product>("https://fakestoreapi.com/products/" + e.Value.ToString());
                StateHasChanged();
            }
            catch 
            {
               
            }

        }






    }
}
