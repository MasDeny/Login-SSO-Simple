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

namespace WebApps.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AuthController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
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

            userMapper.Url = userCredentials.TypeUser[0].ToString() == enumsData[0].ToString() ? "http://192.168.71.170:5001/login" : "www.xvideos.com";
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
                return BadRequest(new { message = "Username or password is incorrect" });

            var userResource = _mapper.Map<User, UserResource>(userAuth);

            return Ok(userResource);
        }
    }
}
