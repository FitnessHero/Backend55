using Contracts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.Food
{
	[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
	[ApiController]
	[Route("[controller]")]
	public class FoodController : ControllerBase
	{
		private IRepositoryWrapper _repository;
		private IConfiguration _iconfiguration;
		public FoodController(IConfiguration iconfiguration, IRepositoryWrapper repository)
		{
			_repository = repository;
			_iconfiguration = iconfiguration;
		}

		[HttpGet]
		[Route("/api/food/{id}")]
		[Produces("application/json")]
		public IActionResult Get(int foodId)
		{
			var food = _repository.Food.FindByCondition(f => f.Id == foodId);
			return Ok(new JsonResult(food));
		}

		[HttpPost]
		[Route("/api/food")]
		[Consumes("application/json")]
		[Produces("application/json")]
		public IActionResult Post([FromBody] Entities.Models.Request.Food food)
		{
			float proteinsInOneGram = food.Proteins / food.Grams;
			float carbohydratesInOneGram = food.Carbohydrates / food.Grams;
			float fatsInOneGram = food.Fats / food.Grams;
			var nutrientEntt = _repository.Nutrient.Create(new Entities.Models.Database.Nutrient
			{
				Carbohydrates = carbohydratesInOneGram,
				Fats = fatsInOneGram,
				Proteins = proteinsInOneGram,
			});
			_repository.Save();
			var foodEntt =_repository.Food.Create(new Entities.Models.Database.Food
			{
				Name = food.Name,
				Brand = food.Brand,
				Barcode = food.BarCode,
				NutrientsId = nutrientEntt.Entity.Id
			});
			_repository.Save();
			return Ok(new JsonResult(foodEntt.Entity).Value);
		}
	}
}
