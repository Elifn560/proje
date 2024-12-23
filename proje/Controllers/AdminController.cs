using Data.Context;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace proje.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            using (Db db = new Db())
            {
                List<Admin> allAdmins = db . Admin.ToList();
                return PartialView(allAdmins);

            }
        }
    }
}
