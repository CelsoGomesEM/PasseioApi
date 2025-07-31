using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Passeio.Api.Extensions;
using Passeio.Api.ViewModel;
using Passeio.Negocio.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Passeio.Api.Controllers
{
    [Route("api")]
    public class AuthController : MainController
    {

        private readonly SignInManager<IdentityUser> _signInManager; //Auth user
        private readonly UserManager<IdentityUser> _userManager; // User auth
        private readonly AppSettingsJWT _appSettings;

        public AuthController(INotificador notificador, 
                            SignInManager<IdentityUser> signInManager, 
                            UserManager<IdentityUser> userManager,
                            IOptions<AppSettingsJWT> appSettings) : base(notificador)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        [HttpPost("nova-conta")]
        public async Task<ActionResult> Registrar(RegisterUserViewModel user)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var newUser = new IdentityUser()
            {
                UserName = user.Email,
                Email = user.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(newUser, user.Password);

            //Log user in SigIn Manager
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(newUser, false);
                return CustomResponse(await GerarJwt(user.Email));
            }
            foreach (var error in result.Errors)
            {
                NotificarErro(error.Description);
            }

            return CustomResponse(user);
        }

        [HttpPost("entrar")]
        public async Task<ActionResult> Login(LoginUserViewModel user)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, false, true);

            if (result.Succeeded)
            {
                return CustomResponse(await GerarJwt(user.Email));
            }

            if (result.IsLockedOut)
            {
                NotificarErro("Usuário temporariamente bloqueado por multiplas tentativas.");
                return CustomResponse(user);
            }

            NotificarErro("Usuário ou senha incorretos.");
            return CustomResponse(user);
            
        }

        private async Task<string> GerarJwt(string email)
        {
            //Buscar as claims do usuário para verificar quais rotas ele tem acesso
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            return encodedToken;
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

    }
}
