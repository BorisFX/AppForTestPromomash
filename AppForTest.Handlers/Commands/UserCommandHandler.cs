using AppForTest.Application.Commands;
using AppForTest.Application.Exceptions;
using AppForTest.Domain.Entities;
using AppForTest.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AppForTest.Handlers.Commands
{

    public class UserCommandHandler(AppForTestDbContext context, IPasswordHasher<User> passwordHasher) : IRequestHandler<CreateUserCommand, Guid>, IRequestHandler<SelectUserCountryCommand, Unit>
    {
        private readonly AppForTestDbContext _context = context;
        private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);
            if (existingUser != null)
            {
                throw new CreateUserCommandException("User with the provided email already exists.");
            }

            var user = new User
            {
                Email = request.Email,
                PasswordHash = string.Empty,
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password); // Hash the password

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            return user.Id;
        }

        public async Task<Unit> Handle(SelectUserCountryCommand request, CancellationToken cancellationToken)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Id == request.UserId, cancellationToken);
            if (!userExists)
            {
                throw new SelectUserCountryCommandException("User not found.");
            }

            var countryExists = await _context.Countries.AnyAsync(c => c.Id == request.CountryId, cancellationToken);
            if (!countryExists)
            {
                throw new SelectUserCountryCommandException("Country not found.");
            }

            var provinceExists = await _context.Provinces.AnyAsync(c => c.Id == request.ProvinceId, cancellationToken);
            if (!provinceExists)
            {
                throw new SelectUserCountryCommandException("Province not found.");
            }

            var selection = new UserCountrySelection
            {
                UserId = request.UserId,
                CountryId = request.CountryId,
                ProvinceId = request.ProvinceId
            };

            _context.UserCountrySelections.Add(selection);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}