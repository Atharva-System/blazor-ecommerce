using BlazorEcommerce.Application.Contracts.Identity;
using BlazorEcommerce.Shared.Auth;
using BlazorEcommerce.Shared.Response.Concrete;
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
			else
			{
				var responseCast = (DataResponse<AuthResponseDto>)response;

				if (responseCast != null)
				{
                    setTokenCookie(responseCast.Data.RefreshToken);
                }

            }

			return Ok(response);
		}

        [HttpPost("refreshtoken")]
        public async Task<ActionResult<IResponse>> RefreshToken(RefreshTokenRequest request)
        {
            request.RefreshToken = Request.Cookies["refreshToken"];

            var response = await _authService.RefreshToken(request);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            else
            {
                var responseCast = (DataResponse<AuthResponseDto>)response;

                if (responseCast != null)
                {
                    setTokenCookie(responseCast.Data.RefreshToken);
                }

            }

            return Ok(response);
        }


        #region private method
        private void setTokenCookie(string token)
		{
			// append cookie with refresh token to the http response
			var cookieOptions = new CookieOptions
			{
				HttpOnly = true,
				Expires = DateTime.UtcNow.AddDays(7)
			};
			Response.Cookies.Append("refreshToken", token, cookieOptions);
		} 
		#endregion

	}
}
