using CensusDemographic.Domain.Constants;
using CensusDemographic.Domain.Interfaces.Services;
using CensusDemographic.Domain.Models;
using CensusDemographic.Domain.Models.Auth;
using CensusDemographic.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CensusDemographic.WebApi.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public async Task<ActionResult> Authenticated() => Ok(JsonSerializer.Serialize(String.Format("Hello {0}, Welcome!", User?.Identity?.Name)));

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<Token>> Authenticate([FromBody] LoginVM model)
        {
            var token = await _userService.AuthenticateAsync(model);
            return Ok(token);
        }

        [HttpPost]
        [Route("create")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Create([FromBody] User model)
        {
            await _userService.Add(model);
            return Ok(JsonSerializer.Serialize("Account created with success!"));
        }

        [HttpGet]
        [Route("GetAll")]
        [Authorize(Roles = Roles.Adm)]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }

        //[HttpPost]
        //[Route("updatePassword/{id}")]
        //[AllowAnonymous]
        //public async Task<ActionResult> UpdatePassword([FromRoute] Guid id, [FromBody] string password)
        //{
        //    await _userService.UpdatePassword(id, password);
        //    return Ok(JsonSerializer.Serialize("Password update with success!\nLog in with new password"));
        //}
    }
}
