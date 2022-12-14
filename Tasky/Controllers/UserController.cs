using Microsoft.AspNetCore.Mvc;
using Tasky.Attributes;
using Tasky.Controllers.BaseControllers;
using Tasky.Dtos.Request.User;
using Tasky.Interfaces.Services;

namespace Tasky.Controllers;

[ApiController]
[CustomAuthorize("admin")]
//[Authorize(Roles = "admin")]
public class UserController : AuthorizedBaseControllerV1
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] UserModificationRequestDto UserRequestDto)
    {
        var response = await _userService.CreateUser(UserRequestDto);

        return Ok(response);
    }
}
