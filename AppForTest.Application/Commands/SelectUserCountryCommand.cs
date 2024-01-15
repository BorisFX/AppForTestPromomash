using MediatR;

namespace AppForTest.Application.Commands
{

    public class SelectUserCountryCommand : IRequest<Unit>
    {
        public Guid UserId { get; set; }
        public int CountryId { get; set; }
        public int ProvinceId { get; set; }
    }
}