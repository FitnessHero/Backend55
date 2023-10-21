using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models.Database
{
    [Table("user")]
    public class User
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("email")]
        [Required(ErrorMessage = "Email is required")]
        [StringLength(128, ErrorMessage = "Email can't be longer than 128 characters")]
        public string? Email { get; set; }

        [Column("username")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(24, ErrorMessage = "Name can't be longer than 24 characters")]
        public string? UserName { get; set; }

        [Column("password")]
        [Required(ErrorMessage = "Password is required")]
        [StringLength(128, ErrorMessage = "Password can't be longer than 128 characters")]
        public byte[]? Password { get; set; }

        [Column("password_salt")]
        [Required(ErrorMessage = "PasswordSalt is required")]
        [StringLength(128, ErrorMessage = "PasswordSalt can't be longer than 128 characters")]
        public byte[]? PasswordSalt { get; set; }

        [Column("created_at")]
        [Required(ErrorMessage = "CreatedAt is required")]
        public DateTime? CreatedAt { get; set; }
        
        [Column("auth_token")]
        [StringLength(255, ErrorMessage = "AuthToken can't be longer than 255 characters")]
        public string? AuthToken { get; set; }
    }
}
