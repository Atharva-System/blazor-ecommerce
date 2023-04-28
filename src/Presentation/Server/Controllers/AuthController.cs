using BlazorEcommerce.Application.Contracts.Identity;
using BlazorEcommerce.Shared.Response.Abstract;
using BlazorEcommerce.Shared.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlazorEcommerce.Server.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;
        private readonly IIdentityService _identityService;

        public AuthController(IAuthService authService, IIdentityService identityService)
		{
			_authService = authService;
			_identityService = identityService;
		}

		[HttpPost("register")]
		public async Task<ActionResult<IResponse>> Register(UserRegister request)
		{
			var response = await _authService.Register(request);

			if (!response.Success)
			{
				return BadRequest(response);
			}

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

		[HttpPost("change-password"), Authorize]
		public async Task<ActionResult<IResponse>> ChangePassword([FromBody] UserChangePassword changePassword)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var response = await _identityService.ChangePassword(userId, changePassword.CurrentPassword, changePassword.Password);

			if (!response.Success)
			{
				return BadRequest(response);
			}

			return Ok(response);
		}
	}
}
