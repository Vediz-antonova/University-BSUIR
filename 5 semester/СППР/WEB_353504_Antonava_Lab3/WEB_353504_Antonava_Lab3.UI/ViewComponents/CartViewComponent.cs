using Microsoft.AspNetCore.Mvc;

namespace WEB_353504_Antonava_Lab3.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cartInfo = new CartInfo
            {
                Total = 0.0m,
                Count = 0
            };

            return View(cartInfo);
        }
    }
}

public class CartInfo
{
    public decimal Total { get; set; }
    public int Count { get; set; }
}
