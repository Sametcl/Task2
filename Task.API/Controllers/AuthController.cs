using MediatR;
using Microsoft.AspNetCore.Mvc;
using Task.Application.Features.Auth.Commands.Register;

namespace Task.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommandRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send(request);

            if (!result.Succeeded)
            {
                // Hataları listele
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new { Errors = errors });
            }

            return Ok(new { Message = "Kullanıcı başarıyla kaydedildi." });
        }
    }
}
