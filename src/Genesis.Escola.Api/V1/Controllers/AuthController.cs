using Genesis.Escola.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Genesis.Escola.Api.ViewModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using System.Text;
using Genesis.Escola.Api.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using Microsoft.Extensions.Options;
using Genesis.Escola.Api.Controllers;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace Genesis.Escola.Api.V1.Controllers
{

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class AuthController : MainController
    {
        #region Variaveis
        private readonly SignInManager<UsuarioViewModel> _signInManager;
        private readonly IUser _usuarioRepositorio;
        private readonly UserManager<UsuarioViewModel> _userManager;
        private readonly AppSettings _appSettings;
        //private readonly ILogger _logger;
        #endregion

        #region Construtor
        public AuthController(INotificador notificador,
                              SignInManager<UsuarioViewModel> signInManager,
                              UserManager<UsuarioViewModel> userManager, 
                              IOptions<AppSettings> appSettings,
                              IUser user) : base(notificador, user)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }
        #endregion

        #region Registar Usuario
        [HttpPost("novo-usuario")]
        [Authorize]
        public async Task<ActionResult> Registrar(RegisterUserViewModel registerUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new UsuarioViewModel
            {
                UserName = registerUser.Name,
                NomeCompleto = registerUser.NomeCompleto,
                Email = registerUser.Email,
                Permissao = registerUser.Permissao,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (result.Succeeded)
            {
                await _userManager.AddClaimAsync(user, new Claim("Permissao", user.Permissao));
                await _signInManager.SignInAsync(user, false);
                
                return CustomResponse(await GerarJwt(user.UserName));
            }
            foreach (var error in result.Errors)
            {
                NotificarErro(error.Description);
            }

            return CustomResponse(registerUser);
        }
        #endregion

        #region Login
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginUserViewModel loginUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(loginUser.Name, loginUser.Password, false, true);

            if (result.Succeeded)
            {
                //_logger.LogInformation("Usuario " + loginUser.Email + " logado com sucesso");
                return CustomResponse(await GerarJwt(loginUser.Name));
            }
            if (result.IsLockedOut)
            {
                NotificarErro("Usuário temporariamente bloqueado por tentativas inválidas");
                return CustomResponse(loginUser);
            }

            NotificarErro("Usuário ou Senha incorretos");
            return CustomResponse(loginUser);
        }
        #endregion

        #region Gerar Web Token
        private async Task<LoginResponseViewModel> GerarJwt(string nomeUsuario)
        {
            var user = await _userManager.FindByNameAsync(nomeUsuario);
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

            var response = new LoginResponseViewModel
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpiracaoHoras).TotalSeconds,
                UserToken = new UserTokenViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.UserName,
                    NomeCompleto = user.NomeCompleto,
                    Claims = claims.Select(c => new ClaimViewModel { Type = c.Type, Value = c.Value })
                }
            };

            return response;
        }
        #endregion

        #region Get

        [HttpGet("ObterTodos")]
        public async Task<IEnumerable<UsuarioViewModel>> ObterTodos()
        {
           return _signInManager.UserManager.Users.ToList();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<AlunoViewModel>> ObterPorId(Guid id)
        {
            var entidade = await _signInManager.UserManager.FindByIdAsync(id.ToString());
            if (entidade == null) return NotFound();
            return Ok(entidade);
        }

        //[HttpGet("{matricula}")]
        //public async Task<ActionResult<AlunoViewModel>> ObterAluno(string matricula)
        //{
        //    var entidade = _mapper.Map<AlunoViewModel>(await _alunoRepositorio.AlunoExiste(matricula));
        //    if (entidade == null) return NotFound();
        //    return Ok(entidade);
        //}

        //[HttpGet("{matricula}/{senha}")]
        //public async Task<ActionResult<AlunoViewModel>> ObterAluno(string matricula, string senha)
        //{
        //    var entidade = _mapper.Map<AlunoViewModel>(await _alunoRepositorio.AlunoExiste(matricula, senha));
        //    if (entidade == null) return NotFound();
        //    return Ok(entidade);
        //}
        #endregion

        #region Put
        [HttpPut("Atualizar/{id:guid}")]
        public async Task<ActionResult<UsuarioViewModel>> Atualizar(string id, UsuarioViewModel userViewModel)
        {
            if (id != userViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(userViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = await _userManager.FindByIdAsync(userViewModel.Id);
            user.Email = userViewModel.Email;
            user.NomeCompleto = userViewModel.NomeCompleto;
            user.UserName = userViewModel.UserName;
            var result = await _userManager.UpdateAsync(user);
            return Ok(result);

        }

        [HttpPut("AlterarSenha/{id:guid}")]
        public async Task<ActionResult<UsuarioViewModel>> AlterarSenha(string id, ResetPasswordViewModel userViewModel)
        {
            if (id != userViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(userViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var user = await _userManager.FindByIdAsync(userViewModel.Id);

            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, resetToken, userViewModel.Password);
            return Ok(result);

        }

        #endregion

        #region Delete
        [HttpDelete("{id:guid}")]
        [Authorize]
        public async Task<ActionResult<UsuarioViewModel>> Excluir(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var result = await _userManager.DeleteAsync(user);
            return CustomResponse(user);
        }
        #endregion

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

    }
}