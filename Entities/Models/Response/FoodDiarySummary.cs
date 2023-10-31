using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Response
{
	public class Food
	{
		public required int Id { get; set; }
		public required string Name { get; set; }
		public required string Brand { get; set; }
		public required float ProteinsForOneGram { get; set; }
		public required float CarbohydratesForOneGram { get; set; }
		public required float FatsForOneGram { get; set; }
		public required float Grams { get; set; }
	}
	public class FoodDiarySummary
	{
		public required Dictionary<string /*Meal*/, List<Food>> Meals { get; set; }
	}
}
