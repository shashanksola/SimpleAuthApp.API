using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleAuthApp.Services.Services;
using SimpleAuthApp.Services.Validators;
using SimpleAuthApp.Utility.DTOs;

[ApiController]
[Route("[controller]")]


public class UserController : ControllerBase
{
    private readonly UserService _userService;
    private readonly UserValidator _userValidator;

    public UserController(UserService userService, UserValidator userValidator)
    {
        _userService = userService;
        _userValidator = userValidator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRequest userRequest)
    {
        var validationError = _userValidator.ValidateRegistration(userRequest);
        if (validationError != null)
            return BadRequest("Validation Error: " + validationError);

        var result = await _userService.RegisterUser(userRequest);
        if (result != "User registered successfully.")
            return BadRequest("Registration Error: " + result);

        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] SimpleAuthApp.Utility.DTOs.LoginRequest loginRequest)
    {
        var token = await _userService.LoginUser(loginRequest.Username, loginRequest.Password);
        if (token == null)
            return Unauthorized("Invalid username or password.");

        return Ok(new { Token = token });
    }
}
