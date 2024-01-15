using AppForTest.Application.DTOs;
using AppForTest.Application.Queries;
using AppForTest.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AppForTest.Handlers.Queries
{
    public class CountriesQueryHandler(AppForTestDbContext context) : IRequestHandler<GetAllCountriesQuery, CountryDto[]>,
        IRequestHandler<GetProvincesQuery, ProvinceDto[]>
    {
        private readonly AppForTestDbContext _context = context;

        public async Task<CountryDto[]> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
        {
            var countries = await _context.Countries.ToListAsync(cancellationToken);
            return countries.Select(c => new CountryDto { Id = c.Id, Name = c.Name }).ToArray();
        }

        public async Task<ProvinceDto[]> Handle(GetProvincesQuery request, CancellationToken cancellationToken)
        {
            var provinces = await _context.Provinces
             .Where(p => p.CountryId == request.CountryId)
             .ToListAsync(cancellationToken);

            return provinces.Select(p => new ProvinceDto { Id = p.Id, Name = p.Name }).ToArray();
        }
    }
}
