using AppForTest.Application.DTOs;
using MediatR;

namespace AppForTest.Application.Queries;

/// <summary>
/// Get provices by Country Id query
/// </summary>
public class GetProvincesQuery : IRequest<ProvinceDto[]>
{
    public int CountryId { get; set; }
}