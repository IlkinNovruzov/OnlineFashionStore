﻿using Microsoft.AspNetCore.Mvc;

namespace OnlineFashionStore.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Page not found.";
                    break;
            }

            return View("NotFound"); 
        }
    }
}
