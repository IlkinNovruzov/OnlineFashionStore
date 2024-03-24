using Microsoft.AspNetCore.Mvc;

namespace OnlineFashionStore.ViewCompanents
{
    public class Slider:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
