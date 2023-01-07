using Delivery_Report_System.Models;
using Delivery_Report_System.Models.Authentication;
using Delivery_Report_System.Models.Data;
using Delivery_Report_System.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Delivery_Report_System.Controllers;

[ApiController]
[Route("accounts")]
public class AccountController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JwtService _service;

    public AccountController(UserManager<ApplicationUser> userManager,
        JwtService service)
    {
        _userManager = userManager;
        _service = service;
    }

    [HttpPost]
    [Route("register")]
    public async Task<ActionResult<User>> Register([FromBody] User user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var newUser = await _userManager.CreateAsync(
                new ApplicationUser()
                {
                  UserName = user.Username,
                  Email = user.Email,
                  Role = user.Role
                },
                user.Password
            );

            if (!newUser.Succeeded)
            {
                return BadRequest("User registration attempt failed");
            }

            return Ok($"New user {user.Username} created");
        }
        catch (Exception ex)
        {
            return Problem($"Registration failed for {nameof(Register)}", statusCode: 500);
        }

    }


    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<Response>> Login([FromBody] Request loginRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Bad credentials");
        }

        var user = await _userManager.FindByNameAsync(loginRequest.Username);
        if (user == null)
        {
            return NotFound($"{loginRequest.Username} not found");
        }

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
        if (!isPasswordValid)
        {
            return NotFound($"{loginRequest.Username} not found");
        }

        var token = _service.CreateToken(user);
        return Ok(token);
    }
    
    
}