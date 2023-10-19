using System.ComponentModel.DataAnnotations;

namespace FM.Domain.Models
{
    public class UserEntity: EntityBase
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Avatar { get; set; }
    }
}
