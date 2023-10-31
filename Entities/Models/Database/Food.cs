using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Entities.Models.Database
{
	[Table("food")]
	public class Food
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("name")]
        [Required(ErrorMessage = "Name field is required")]
        [StringLength(64, ErrorMessage = "Name must be 64 characters")]
        public string? Name { get; set; }

		[Column("brand")]
		[Required(ErrorMessage = "Brand field is required")]
		[StringLength(64, ErrorMessage = "Brand must be 64 characters")]
		public string? Brand { get; set; }

		[Column("barcode")]
        [Required(ErrorMessage = "Barcode field is required")]
        [StringLength(44, ErrorMessage = "Barcode must be 44 characters")]
        public string? Barcode { get; set; }

        [Column("nutrient_id")]
        [Required(ErrorMessage = "nutrient_id field is required")]
        [ForeignKey("Nutrient")]
        public int NutrientsId { get; set; }
    }
}
