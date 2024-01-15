namespace AppForTest.Domain.Entities
{

    public class UserCountrySelection
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public int ProvinceId { get; set; }
        public Province Province { get; set; }
    }
}