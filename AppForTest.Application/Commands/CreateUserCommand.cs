using MediatR;

namespace AppForTest.Application.Commands
{

    /// <summary>
    /// Create user command
    /// </summary>
    public class CreateUserCommand : IRequest<Guid>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}