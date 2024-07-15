using JazzClub.Models.DataLayer;
using JazzClub.Models.DomainModels;
using Microsoft.AspNetCore.Mvc;


namespace JazzClub.Controllers
{
	public class HomeController : Controller
	{

		private Repository<Day> days { get; set; }

		public HomeController(JazzClubContext ctx)
		{
			
			days = new Repository<Day>(ctx);
        }

		public IActionResult Index(int id)
		{
            
			
			return View();
		}


	}
}
