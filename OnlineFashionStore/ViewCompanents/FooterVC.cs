using Microsoft.AspNetCore.Mvc;

namespace OnlineFashionStore.ViewCompanents
{
    public class FooterVC: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
