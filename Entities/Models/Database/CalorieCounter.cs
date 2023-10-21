using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models.Database
{
    public class CalorieCounter
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

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
