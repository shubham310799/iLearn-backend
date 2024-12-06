using System.ComponentModel.DataAnnotations;

namespace iLearn.Data.Models
{
    public class Role
    {
        public Guid Id { get; set; }
        public string RoleName { get; set; } = "User";
    }
}
