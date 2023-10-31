using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Database
{
	[Table("nutrient")]
	public class Nutrient
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("proteins")]
        [Required(ErrorMessage = "Protein field is required")]
        public float? Proteins { get; set; }

        [Column("carbohydrates")]
        [Required(ErrorMessage = "Carbohydrates field is required")]
        public float? Carbohydrates { get; set; }

        [Column("fats")]
        [Required(ErrorMessage = "Fat field is required")]
        public float? Fats { get; set; }
    }
}
