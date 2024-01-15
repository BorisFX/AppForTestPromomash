namespace AppForTest.Application.DTOs
{
    public class ProvinceDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int CountryId { get; set; }
    }
}
