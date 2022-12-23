using FastNotes.Api.DataTransferModels;
using FastNotes.Api.Validators;
using FastNotes.Common.Models;
using FastNotes.Common.Models.Responses;
using FastNotes.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace FastNotes.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserWorker _userWorker;
        private readonly LoginValidator _loginValidator;
        private readonly IConfiguration _configuration;
        private readonly UserValidator _userValidator;

        public AuthenticationController(IUserWorker userWorker, LoginValidator loginValidator, IConfiguration configuration, UserValidator userValidator)
        {
            _userWorker = userWorker;
            _loginValidator = loginValidator;
            _configuration = configuration;
            _userValidator = userValidator;
        }

        [HttpPost("/Authentication/Login")]
        public async Task<ActionResult<GenericResponse<LoginResponse>>> Login(LoginDto request)
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
                return Ok(new GenericResponse<LoginResponse>()
                {
                    Success = true,
                    Message = "Loged in successfully!",
                    Value = new LoginResponse() 
                        { 
                            UserNickname = userResult.NickName,
                            JwtToken = GetNewJwtToken(userResult),
                            UserId = userResult.Id
                        }
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

        [HttpPost("/Authentication/Register")]
        public async Task<ActionResult<GenericResponse<User>>> Register(UserDto request)
        {
            if (!_userValidator.Validate(request).IsValid)
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

        private static bool RequestPasswordHashIsValid(string requestPassword, byte[] passwordSalt, byte[] passwordHash)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var encodedRequestPassword = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(requestPassword));
            return encodedRequestPassword.SequenceEqual(passwordHash);
        }
    }

}
