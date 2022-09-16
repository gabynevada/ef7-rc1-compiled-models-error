using Ef7Rc1Error.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ef7Rc1Error.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserManager<User> _userManager;

    public UsersController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> AddUser()
    {
        var user = await _userManager.CreateAsync(new User
        {
            Id = Guid.NewGuid(),
            UserName = "test@test.com",
            Email = "test@test.com",
        });
        if (!user.Succeeded)
           throw new Exception("Failed to create user");
        return Ok();
    }
}