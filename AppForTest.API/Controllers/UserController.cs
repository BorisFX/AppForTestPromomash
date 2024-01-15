using AppForTest.Application.Commands;
using AppForTest.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppForTest.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;

        // POST: /User/Register
        [HttpPost("Register")]
        [ProducesResponseType(typeof(Guid), 200)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Register([FromBody] CreateUserCommand command)
        {
            try
            {
                var userId = await _mediator.Send(command);
                return Ok(new { UserId = userId });
            }
            catch (CreateUserCommandException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception)
            {

                return BadRequest("An error occurred while creating the user.");
            }
        }

        // POST: /User/SelectCountry
        [HttpPost("SelectCountry")]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> SelectCountry([FromBody] SelectUserCountryCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return Ok();
            }
            catch (SelectUserCountryCommandException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return BadRequest("An error occurred while selecting the country.");
            }
        }
    }
}