using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Request
{
	public class Food
	{
		public required string Name { get; set; }
		public required string Brand { get; set; }
		public string? BarCode { get; set; }
		public required int Grams { get; set; }
		public required float Proteins { get; set; }
		public required float Carbohydrates { get; set; }
		public required float Fats { get; set; }
	}
}
