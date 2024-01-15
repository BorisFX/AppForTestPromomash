using System.ComponentModel.DataAnnotations;

namespace AppForTest.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        [EmailAddress]
        [MaxLength(256)]
        public required string Email { get; set; }

        [MaxLength(256)]
        public required string PasswordHash { get; set; }
    }
}