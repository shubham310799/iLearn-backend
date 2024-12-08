using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace iLearn.Data.Models
{
    public class Role
    {
        [Required]
        public Guid Id { get; set; } = new Guid();
        [Required]
        public string RoleName { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
