namespace ShoppingCart.WebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : ControllerBase
    {
        public IActionResult Index()
        {
            return new RedirectResult("~/swagger");
        }
    }
}
