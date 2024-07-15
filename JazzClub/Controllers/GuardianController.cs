using Azure;
using JazzClub.Models.DataLayer;
using JazzClub.Models.DomainModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JazzClub.Controllers
{
	public class GuardianController : Controller
	{
		private readonly JazzClubContext _context;
		public GuardianController(JazzClubContext ctx) => _context = ctx;
		public async Task<IActionResult> Index()
		{
			var options = new QueryOptions<Guardian>
			{
				OrderBy = t => t.Name,
				Where = s => s.status == 0
			};

			var list = await _context.Guardians
				.Where(s => s.status == 0)
				.OrderBy(t => t.Name)
				.ToListAsync();

			return View(list);
		}

		[HttpGet]
		public ViewResult Add()
		{
			ViewBag.Operation = "Add";
			return View("Add", new Guardian());
		}

		[HttpGet]
		public async Task<ViewResult> Edit(int id)
		{
			this.LoadViewBag("Edit");
			var c = await this.GetGuardian(id);
			return View("Add", c);

		}

		[HttpPost]
		public async Task<IActionResult> Add(Guardian guardian)
		{
			bool isAdd = guardian.GuardianId == 0;

			if (ModelState.IsValid)
			{
				if (isAdd)
					_context.Guardians.Add(guardian);
				else
					_context.Guardians.Update(guardian);

				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			else
			{
				string operation = (isAdd) ? "Add" : "Edit";
				ViewBag.Operation = operation;
				return View("Add", guardian);
			}
		}

		private async Task<Guardian> GetGuardian(int id)
		{
			

			var guardian = await _context.Guardians
				.Where(d => d.GuardianId == id)
				.FirstAsync();


			return guardian ?? new Guardian();
		}

		private void LoadViewBag(string operation)
		{


			ViewBag.Operation = operation;
		}

		[HttpGet]
		public async Task<ViewResult> Delete(int id)
		{
			var c = await this.GetGuardian(id);
			return View(c);
		}

		[HttpPost]
		public async Task<RedirectToActionResult> Delete(Guardian guardian)
		{
			_context.Guardians.Remove(guardian);
			await _context.SaveChangesAsync();
			return RedirectToAction("Index");
		}

	}
}
