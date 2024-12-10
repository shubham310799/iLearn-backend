using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace iLearn.Data.DTOs
{
    public class UserRegistrationDTO
    {
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }
        [JsonPropertyName("last_name")] 
        public string LastName { get; set; }
        [EmailAddress]
        [JsonPropertyName("email")] 
        public string Email { get; set; }
        [JsonPropertyName("password")] 
        public string Password { get; set; }
        [JsonPropertyName("role")] 
        public Common.Enums.Roles Role { get; set; }
    }
}
