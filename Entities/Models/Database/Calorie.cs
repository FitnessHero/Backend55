using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Database
{
    public class Calorie
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("name")]
        [Required(ErrorMessage = "Name field is required")]
        [StringLength(64, ErrorMessage = "Name must be 64 characters")]
        public string? Name { get; set; }

        [Column("protein")]
        [Required(ErrorMessage = "Protein field is required")]
        public float? Protein { get; set; }

        [Column("carbohydrates")]
        [Required(ErrorMessage = "Carbohydrates field is required")]
        public float? Carbohydrates { get; set; }

        [Column("fat")]
        [Required(ErrorMessage = "Fat field is required")]
        public float? Fat { get; set; }
    }
}
