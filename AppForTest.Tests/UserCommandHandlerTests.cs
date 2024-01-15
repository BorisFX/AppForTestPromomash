using AppForTest.Application.Commands;
using AppForTest.Application.Exceptions;
using AppForTest.Domain.Entities;
using AppForTest.Handlers.Commands;
using AppForTest.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace AppForTest.Tests
{
    [TestFixture]
    public class UserCommandHandlerTests
    {
        private AppForTestDbContext _context;
        private UserCommandHandler _handler;
        private Mock<IPasswordHasher<User>> _mockPasswordHasher;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppForTestDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            _context = new AppForTestDbContext(options);

            _context.Users.Add(new User { Id = Guid.NewGuid(), Email = "test@example.com", PasswordHash = "hashedpassword" });
            _context.Countries.Add(new Country { Id = 1, Name = "TestCountry" });
            _context.Provinces.Add(new Province { Id = 1, Name = "TestProvince", CountryId = 1 });
            _context.SaveChanges();

            _mockPasswordHasher = new Mock<IPasswordHasher<User>>();
            _handler = new UserCommandHandler(_context, _mockPasswordHasher.Object);

            _mockPasswordHasher.Setup(ph => ph.HashPassword(It.IsAny<User>(), It.IsAny<string>()))
                               .Returns("hashedpassword");
        }

        [Test]
        public async Task Handle_NewUser_ShouldCreateUser()
        {
            var command = new CreateUserCommand { Email = "test_not_exist@example.com", Password = "Password123" };

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.That(result, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void Handle_ExistingUser_ShouldThrowException()
        {
            var command = new CreateUserCommand { Email = "test@example.com", Password = "Password123" };

            var ex = Assert.ThrowsAsync<CreateUserCommandException>(async () => await _handler.Handle(command, CancellationToken.None));
            Assert.That(ex.Message, Is.EqualTo("User with the provided email already exists."));
        }

        [Test]
        public void Handle_NonExistentUser_ShouldThrowException()
        {
            var request = new SelectUserCountryCommand
            {
                UserId = Guid.NewGuid(),
                CountryId = _context.Countries.First().Id,
                ProvinceId = _context.Provinces.First().Id
            };

            var ex = Assert.ThrowsAsync<SelectUserCountryCommandException>(async () => await _handler.Handle(request, CancellationToken.None));
            Assert.That(ex.Message, Is.EqualTo("User not found."));
        }

        [Test]
        public void Handle_NonExistentCountry_ShouldThrowException()
        {
            var nonExistentCountryId = 999; 
            var request = new SelectUserCountryCommand
            {
                UserId = _context.Users.First().Id,
                CountryId = nonExistentCountryId,
                ProvinceId = _context.Provinces.First().Id
            };

            var ex = Assert.ThrowsAsync<SelectUserCountryCommandException>(async () => await _handler.Handle(request, CancellationToken.None));
            Assert.That(ex.Message, Is.EqualTo("Country not found."));
        }

        [Test]
        public async Task Handle_ValidRequest_ShouldAddUserCountrySelection()
        {
            var request = new SelectUserCountryCommand
            {
                UserId = _context.Users.First().Id,
                CountryId = _context.Countries.First().Id,
                ProvinceId = _context.Provinces.First().Id
            };

            await _handler.Handle(request, CancellationToken.None);

            var selectionExists = _context.UserCountrySelections.Any(ucs =>
                ucs.UserId == request.UserId &&
                ucs.CountryId == request.CountryId &&
                ucs.ProvinceId == request.ProvinceId);

            Assert.IsTrue(selectionExists);
        }

        [Test]
        public void HandleNonExistentProvinceShouldThrowException()
        {
            var nonExistentProvinceId = 999;
            var request = new SelectUserCountryCommand
            {
                UserId = _context.Users.First().Id,
                CountryId = _context.Countries.First().Id,
                ProvinceId = nonExistentProvinceId
            };
            var ex = Assert.ThrowsAsync<SelectUserCountryCommandException>(async () => await _handler.Handle(request, CancellationToken.None));
            Assert.That(ex.Message, Is.EqualTo("Province not found."));
        }
    }
}
