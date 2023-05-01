using BlazorEcommerce.Application.Contracts.Identity;
using BlazorEcommerce.Shared.User;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
		public async Task<ActionResult<IResponse>> Register(UserRegister request)
		{
			var response = await _authService.Register(request);

			return Ok(response);
		}

		[HttpPost("login")]
		public async Task<ActionResult<IResponse>> Login(UserLogin request)
		{
			var response = await _authService.Login(request);
			if (!response.Success)
			{
				return BadRequest(response);
			}

			return Ok(response);
		}
		
	}
}
