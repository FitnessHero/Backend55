using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Backend.Controllers.Auth
{
	[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
	[ApiController]
	[Route("[controller]")]
	public class FoodDiaryController : ControllerBase
	{
		private IRepositoryWrapper _repository;
		private IConfiguration _iconfiguration;

		public FoodDiaryController(IConfiguration iconfiguration, IRepositoryWrapper repository)
		{
			_repository = repository;
			_iconfiguration = iconfiguration;
		}

		[HttpGet]
		[Route("/api/food/diary/{date}")]
		[Produces("application/json")]
		public IActionResult Get(DateTime date)
		{
			var meals = new Entities.Models.Response.FoodDiarySummary
			{
				Meals = new()
			};
			var foodDiary = _repository.FoodDiary.FindByCondition(counter => counter.Date == date.Date && counter.UserId == 1/*TODO use real id*/).ToList();
			if (foodDiary.Count == 0)
				return NotFound();

			foreach (Entities.Models.Database.FoodDiary item in foodDiary)
			{
				Entities.Models.Database.Food food = _repository.Food.FindByCondition(f => f.Id == item.FoodId).First();
				if (food == null)
					continue;
				var nutrients = _repository.Nutrient.FindByCondition(nutrient => nutrient.Id == food.NutrientsId);
				if (nutrients.Count() != 1 || nutrients.First() == null)
					continue;
				if (!meals.Meals.ContainsKey(item.Meal))
				{
					var foods = new List<Entities.Models.Response.Food>
					{
						new Entities.Models.Response.Food
						{
							Id = food.Id,
							Name = food.Name ?? "Unknown name",
							Brand = food.Brand ?? "Unknown brand",
							ProteinsForOneGram = nutrients.First().Proteins ?? 0,
							CarbohydratesForOneGram = nutrients.First().Carbohydrates ?? 0,
							FatsForOneGram = nutrients.First().Fats ?? 0,
							Grams = item.Grams ?? 0
						}
					};
					meals.Meals.Add(item.Meal, foods);
				}
				else
				{
					meals.Meals[item.Meal].Add(new Entities.Models.Response.Food
					{
						Id = food.Id,
						Name = food.Name ?? "Unknown name",
						Brand = food.Brand ?? "Unknown brand",
						ProteinsForOneGram = nutrients.First().Proteins ?? 0,
						CarbohydratesForOneGram = nutrients.First().Carbohydrates ?? 0,
						FatsForOneGram = nutrients.First().Fats ?? 0,
						Grams = item.Grams ?? 0
					});
				}
			}

			return Ok(new JsonResult(meals).Value);
		}

		[HttpPatch]
		[Route("/api/food/diary/{date}")]
		[Produces("application/json")]
		public IActionResult Patch(DateTime date, [FromBody] Entities.Models.Request.FoodDiary request)
		{
			if (request.Id == null)
				return CreateFoodDiary(date, request);
			return UpdateFoodDiary(date, request);
		}

		private IActionResult UpdateFoodDiary(DateTime date, Entities.Models.Request.FoodDiary request)
		{
			var entt = _repository.FoodDiary.FindByCondition(c => c.Id == request.Id && c.UserId == 3 /*TODO use real id*/).First();
			if (entt == null)
				return NotFound();
			if (request.Date != null)
				entt.Date = request.Date.Value;
			if (request.FoodId != null)
				entt.FoodId = request.FoodId.Value;
			if (request.Grams != null)
				entt.Grams = request.Grams.Value;
			if (request.Meal != null)
				entt.Meal = request.Meal;
			_repository.FoodDiary.Update(entt);
			_repository.Save();
			return Ok(new JsonResult(entt).Value);
		}

		private IActionResult CreateFoodDiary(DateTime date, Entities.Models.Request.FoodDiary request)
		{
			Entities.Models.Database.FoodDiary foodDiaryDb = new Entities.Models.Database.FoodDiary
			{
				Date = date.Date,
				FoodId = request.FoodId,
				Grams = request.Grams,
				Meal = request.Meal,
				UserId = 1/*TODO use real id*/
			};

			var entt = _repository.FoodDiary.Create(foodDiaryDb);
			_repository.Save();
			return Ok(new JsonResult(entt.Entity).Value);
		}
	}
}
