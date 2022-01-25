using Microsoft.AspNetCore.Components;
using TheFakeStore.Domain.Entities;
using TheFakeStore.Services.Abstract;


namespace TheFakeStore.Pages
{
    public class EditproductPageBase : ComponentBase
    {
        [Inject]
        public IHttpService _HttpService { get; set; }

        [Parameter]
        public int ProductId { get; set; }

        public Product product { get; set; } = new Product();

        protected override async Task OnInitializedAsync()
        {
            product = await _HttpService.Get<Product>("https://fakestoreapi.com/products/" + ProductId);
        }

        public async void HandleValidSubmit()
        {
            await _HttpService.Put<Product>("https://fakestoreapi.com/products/" + ProductId, product );
    
        }


    }
}
