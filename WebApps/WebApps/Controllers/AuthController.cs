using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApps.Domain.Models;
using WebApps.Domain.Services;
using WebApps.Resources;
using WebApps.Extensions;
using WebApps.Domain.Enum;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using WebApps.Helpers;

namespace WebApps.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private UrlSettings _urlSettings;

        public AuthController(IAuthService authService, IMapper mapper, IConfiguration configuration)
        {
            _authService = authService;
            _mapper = mapper;
            _configuration = configuration;
            _urlSettings = configuration.GetSection("UrlSettings").Get<UrlSettings>();
        }

        [HttpGet("types")]
        public async Task<IActionResult> GetAllAsync()
        {
            var url = await _authService.ListUrlAsync();
            var urlResource = _mapper.Map< IEnumerable<Domain.Models.Type>, IEnumerable<TypeResource>>(url);
            return Ok(urlResource);
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetRolesAsync()
        {
            var role = await _authService.ListRoleAsync();
            var roleResource = _mapper.Map<IEnumerable<Role>, IEnumerable<RoleResource>>(role);
            return Ok(roleResource  );
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateUserAsync([FromBody] SaveUserResource userCredentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserResource user = new UserResource
            {
                Email = userCredentials.Email,
                UserName = userCredentials.Username,
                Password = userCredentials.Password
            };

            var userMapper = _mapper.Map<UserResource, User>(user);

            EType[] enumsData = { EType.Scale };

            userMapper.Url = userCredentials.TypeUser[0].ToString() == enumsData[0].ToString() ? _urlSettings.Scale : _urlSettings.Energy;
            userMapper.Created = DateTime.Now;
            userMapper.LastModified = DateTime.Now;

            var response = await _authService.CreateUserAsync(userMapper, userCredentials.RoleUser, userCredentials.TypeUser);
            if (!response.Success)
            {
                return BadRequest(new { message = response.Message });
            }

            var userResource = _mapper.Map<User, UserResource>(response.User);

            return Ok(userResource);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody]UserCredentialsResource userParam)
        {
            var userAuth = await _authService.Authenticate(userParam.Email, userParam.Password);

            if (userAuth == null)
                return BadRequest(new { message = "Email or password is incorrect" });

            var userResource = _mapper.Map<User, UserResource>(userAuth);

            return Ok(userResource);
        }

        [HttpPut("changed")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody]ResetPasswordResource userParam)
        {
            var checkEmail = await _authService.FindByEmailAsync(userParam.Email);

            if(checkEmail == null)
                return BadRequest(new { message = "Email not found" });

            var userAuth = await _authService.ChangePasswordAsync(userParam.Email, userParam.Password, userParam.PasswordReset);

            if (userAuth == null)
                return BadRequest(new { message = "Password is incorrect" });

            var userResource = _mapper.Map<User, UserResource>(userAuth.User);

            return Ok(userResource);
        }
    }
}
