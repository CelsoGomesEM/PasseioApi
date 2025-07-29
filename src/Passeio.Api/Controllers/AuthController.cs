using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Passeio.Api.ViewModel;
using Passeio.Negocio.Interfaces;

namespace Passeio.Api.Controllers
{
    [Route("api")]
    public class AuthController : MainController
    {

        private readonly SignInManager<IdentityUser> _signInManager; //Auth user
        private readonly UserManager<IdentityUser> _userManager; // User auth

        public AuthController(INotificador notificador, 
                            SignInManager<IdentityUser> signInManager, 
                            UserManager<IdentityUser> userManager) : base(notificador)
        {
            _signInManager = signInManager;
            _userManager = userManager;
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
                return CustomResponse(user);
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
                return CustomResponse(user);
            }

            if (result.IsLockedOut)
            {
                NotificarErro("Usuário temporariamente bloqueado por multiplas tentativas.");
                return CustomResponse(user);
            }

            NotificarErro("Usuário ou senha incorretos.");
            return CustomResponse(user);
            
        }
    }
}
