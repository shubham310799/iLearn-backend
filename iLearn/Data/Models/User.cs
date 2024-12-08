using System.ComponentModel.DataAnnotations;

namespace iLearn.Data.Models
{
    public class User
    {
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreateDt { get; set; } = DateTime.Now;
        public DateTime UpdateDt { get; set; } = DateTime.Now;
        public bool IsVerified { get; set; } = false;
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
