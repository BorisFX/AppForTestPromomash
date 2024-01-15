using AppForTest.Application.DTOs;
using MediatR;

namespace AppForTest.Application.Queries
{
    /// <summary>
    /// Get all countries
    /// </summary>
    public class GetAllCountriesQuery : IRequest<CountryDto[]>
    {

    }
}