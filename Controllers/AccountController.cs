using Microsoft.AspNetCore.Mvc;

namespace P_Utilizacion_de_Software.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
