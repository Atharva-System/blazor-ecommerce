using BlazorEcommerce.Application.Contracts.Identity;
using BlazorEcommerce.Application.Model;
using BlazorEcommerce.Shared.Auth;
using BlazorEcommerce.Shared.Constant;
using BlazorEcommerce.Shared.Response.Abstract;
using BlazorEcommerce.Shared.Response.Concrete;
using BlazorEcommerce.Shared.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BlazorEcommerce.Identity.Service
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;
        private const string AuthenticatorStoreLoginProvider = "[AuthenticatorStore]";
        public string AuthenticatorKeyTokenName = "AuthenticatorKey";
        public AuthService(UserManager<ApplicationUser> userManager,
            IOptions<JwtSettings> jwtSettings,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _signInManager = signInManager;
        }

        public async Task<IResponse> Login(UserLogin request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return new DataResponse<AuthResponseDto>(new AuthResponseDto(), HttpStatusCodes.NotFound, $"User with {request.Email} not found.", false);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (result.Succeeded == false)
            {
                return new DataResponse<AuthResponseDto>(new AuthResponseDto(), HttpStatusCodes.NotFound, Messages.UserNameOrPasswordIsIncorrect, false);
            }
            else
            {
                JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

                string jwtToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

                string refreshToken = CreateRefreshToken();

                await _userManager.RemoveLoginAsync(user, AuthenticatorStoreLoginProvider, refreshToken);

                await _userManager.AddLoginAsync(user, new UserLoginInfo(AuthenticatorStoreLoginProvider, refreshToken, user.UserName));

                await _userManager.SetAuthenticationTokenAsync(user, AuthenticatorStoreLoginProvider, AuthenticatorKeyTokenName, jwtToken);

                var authResponseDto = new AuthResponseDto() { RefreshToken = refreshToken, Token = jwtToken };

                return new DataResponse<AuthResponseDto>(authResponseDto, HttpStatusCodes.Accepted);
            }
        }

        public async Task<IResponse> RefreshToken(RefreshTokenRequest request)
        {
            if (string.IsNullOrEmpty(request.RefreshToken))
            {
                return new DataResponse<AuthResponseDto>(new AuthResponseDto(), HttpStatusCodes.NotFound, String.Format(Messages.NotFound, "Refresh Token"), false);
            }

            var user = await _userManager.FindByLoginAsync(AuthenticatorStoreLoginProvider, request.RefreshToken);

            if (user == null)
            {
                return new DataResponse<AuthResponseDto>(new AuthResponseDto(), HttpStatusCodes.NotFound, $"No User found with token.", false);
            }

            if (ValidateToken(request.CurrentToken))
            {
                return new DataResponse<AuthResponseDto>(new AuthResponseDto(), HttpStatusCodes.NotFound, Messages.TokenNotExpired, false);
            }

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

            string jwtToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            string refreshToken = CreateRefreshToken();

            await _userManager.RemoveLoginAsync(user, AuthenticatorStoreLoginProvider, refreshToken);

            await _userManager.AddLoginAsync(user, new UserLoginInfo(AuthenticatorStoreLoginProvider, refreshToken, user.UserName));

            await _userManager.SetAuthenticationTokenAsync(user, AuthenticatorStoreLoginProvider, AuthenticatorKeyTokenName, jwtToken);

            var authResponseDto = new AuthResponseDto() { RefreshToken = refreshToken, Token = jwtToken };

            return new DataResponse<AuthResponseDto>(authResponseDto, HttpStatusCodes.Accepted);
        }

        public async Task<IResponse> Register(UserRegister request)
        {
            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                return new DataResponse<string>(user.Id, HttpStatusCodes.Accepted, Messages.RegisteredSuccesfully);
            }
            else
            {
                List<string> str = new List<string>();
                foreach (var err in result.Errors)
                {
                    str.Add(err.Description);
                }

                return new DataResponse<string?>(null, HttpStatusCodes.BadRequest, str, false);
            }
        }

        #region private methods
        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Email),
            }
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
               issuer: _jwtSettings.Issuer,
               audience: _jwtSettings.Audience,
               claims: claims,
               expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),
               signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }

        private string CreateRefreshToken()
        {
            var numberByte = new byte[32];
            using var rnd = RandomNumberGenerator.Create();
            rnd.GetBytes(numberByte);
            return Convert.ToBase64String(numberByte);
        }

        private bool ValidateToken(string token)
        {
            JwtSecurityToken jwtToken = new JwtSecurityToken(token);
            return (jwtToken.ValidFrom <= DateTime.UtcNow && jwtToken.ValidTo >= DateTime.UtcNow);
        }
        #endregion

    }
}
