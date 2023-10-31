using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models.Database
{
	[Table("food_diary")]
	public class FoodDiary
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

		[Column("meal")]
		[Required(ErrorMessage = "Meal field is required")]
		[StringLength(64, ErrorMessage = "Meal must be 64 characters")]
		public string? Meal { get; set; }

		[Column("grams")]
		[Required(ErrorMessage = "Grams field is required")]
		public int? Grams { get; set; }

		[Column("user_id")]
        [Required(ErrorMessage = "UserId field is required")]
        [ForeignKey("User")]
        public int? UserId { get; set; }

        [Column("food_id")]
        [Required(ErrorMessage = "FoodId field is required")]
        [ForeignKey("Food")]
        public int? FoodId { get; set; }

        [Column("date")]
        [Required(ErrorMessage = "Date field is required")]
        public DateTime? Date { get; set; }
    }
}
