using System.ComponentModel.DataAnnotations;

namespace iLearn.Data.Models
{
    public class UserRole
    {
        [Key]
        public Guid UserRoleId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }

    }
}
