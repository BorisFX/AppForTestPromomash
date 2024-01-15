using System.ComponentModel.DataAnnotations;

namespace AppForTest.Domain.Entities
{
    public class Province
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public required string Name { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}