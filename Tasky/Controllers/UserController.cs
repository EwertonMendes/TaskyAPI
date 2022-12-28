﻿using Microsoft.AspNetCore.Mvc;
using Tasky.Dtos.Request;
using Tasky.Interfaces.Services;

namespace Tasky.Controllers;

[ApiController]
public class UserController : TaskyApiBaseControllerV1
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] UserRequestDto UserRequestDto)
    {
        var response = await _userService.CreateUser(UserRequestDto);

        return Ok(response);
    }
}
