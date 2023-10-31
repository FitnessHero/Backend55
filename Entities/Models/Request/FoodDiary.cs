using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Request
{
	public class FoodDiary
	{
		public int? Id { get; set; }
		[RegularExpression("^breakfast|^lunch$|^dinner$|^snacks$", ErrorMessage = "Invalid Value")]
		public string? Meal { get; set; }
		public int? Grams { get; set; }
		public int? FoodId { get; set; }
		public DateTime? Date { get; set; }
		
	}
}
