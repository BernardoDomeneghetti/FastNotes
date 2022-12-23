using FastNotes.Common.Models;
using FastNotes.Common.Models.Responses;
using FastNotes.Domain.Interfaces;
using FastNotes.Mvc.Models;
using FastNotes.Mvc.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace FastNotes.Mvc.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserWorker _userWorker;
        private readonly LoginValidator _loginValidator;
        private readonly IConfiguration _configuration;

        public AuthenticationController(IUserWorker userWorker, LoginValidator loginValidator, IConfiguration configuration)
        {
            _userWorker = userWorker;
            _loginValidator = loginValidator;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult<GenericResponse<string>>> Login(UserDto request)
        {
            if (!_loginValidator.Validate(request).IsValid)
                return Ok(
                        new GenericResponse<User>
                        {
                            Success = false,
                            Message = "The e-mail is invalid!",
                        }
                    );

            var userResult = await _userWorker.GetByEmail(request.Email);

            if (userResult != null && RequestPasswordHashIsValid(request.Password, userResult.PasswordSalt, userResult.PasswordHash))
                return Ok(new GenericResponse<string>()
                {
                    Success = true,
                    Message = "Loged in successfully!",
                    Value = GetNewJwtToken(userResult)
                });
            else
                return Ok(new GenericResponse<string>()
                {
                    Success = false,
                    Message = "Login failed, e-mail or password is incorrect!"
                });
            
        }

        private string GetNewJwtToken(User userResult)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, userResult.Email)
            };

            var key = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(
                    _configuration.GetSection("Keys:AuthenticationToken").Value));


            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            
            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<ActionResult<GenericResponse<User>>> Register(UserDto request)
        {
            if (!_loginValidator.Validate(request).IsValid)
                return Ok(new GenericResponse<User>()
                {
                    Success = false,
                    Message = "The e-mail is invalid!"
                });

            CreatePasswordHash(request.Password, out var passwordSalt, out var passwordHash);

            var user = new User
            {
                NickName = request.NickName,
                Email = request.Email,
                PasswordSalt = passwordSalt,
                PasswordHash = passwordHash
            };

            return await _userWorker.Register(user);
        }

        private static void CreatePasswordHash(string password, out byte[] passwordSalt, out byte[] passwordHash)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private static bool RequestPasswordHashIsValid (string requestPassword, byte[] passwordSalt, byte[] passwordHash)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var encodedRequestPassword = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(requestPassword));
            return encodedRequestPassword.SequenceEqual(passwordHash);
        }
    }
}
