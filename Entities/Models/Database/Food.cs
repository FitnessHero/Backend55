using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Entities.Models.Database
{
    public class Food
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("name")]
        [Required(ErrorMessage = "Name field is required")]
        [StringLength(64, ErrorMessage = "Name must be 64 characters")]
        public string? Name { get; set; }

        [Column("barcode")]
        [Required(ErrorMessage = "Barcode field is required")]
        [StringLength(44, ErrorMessage = "Barcode must be 44 characters")]
        public string? Barcode { get; set; }

        [Column("calorie")]
        [Required(ErrorMessage = "Calorie field is required")]
        public int? Calorie { get; set; }

        [Column("calories")]
        [Required(ErrorMessage = "Calories field is required")]
        [ForeignKey("Calorie")]
        public int? Calories { get; set; }
    }
}
