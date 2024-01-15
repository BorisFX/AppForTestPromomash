using System.ComponentModel.DataAnnotations;

namespace AppForTest.Domain.Entities
{
    public class Country
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public required string Name { get; set; }
        public IEnumerable<Province> Provinces { get; set; }
    }
}