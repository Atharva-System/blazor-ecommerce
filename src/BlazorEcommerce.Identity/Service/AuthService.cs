using BlazorEcommerce.Application.Contracts.Identity;
using BlazorEcommerce.Application.Model;
using BlazorEcommerce.Shared.Constant;
using BlazorEcommerce.Shared.Response.Abstract;
using BlazorEcommerce.Shared.Response.Concrete;
using BlazorEcommerce.Shared.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlazorEcommerce.Identity.Service
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;

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
                return new DataResponse<string>(null,HttpStatusCodes.NotFound, $"User with {request.Email} not found.");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (result.Succeeded == false)
            {
                return new DataResponse<string>(null, HttpStatusCodes.NotFound, Messages.UserNameOrPasswordIsIncorrect);
            }
            else
            {
                JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

               string jwtToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

                return new DataResponse<string>(jwtToken,HttpStatusCodes.Accepted);
            }
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

    }
}
