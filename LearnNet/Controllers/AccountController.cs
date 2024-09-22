using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using api.DTOs.Account;
namespace api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<UserModel> _userManager;
        public AccountController(UserManager<UserModel> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid data");
                    
                
                if (string.IsNullOrEmpty(registerDto.Email))
                {
                    return BadRequest("Email is required");
                }
                
                var existingEmail = await _userManager.FindByEmailAsync(registerDto.Email);   
                            
                if (existingEmail != null)
                    return BadRequest("User already exists");

                var user = new UserModel
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email
                };

                if (string.IsNullOrEmpty(registerDto.Password))
                {
                    return BadRequest("Password is required");
                }

                var createdUser = await _userManager.CreateAsync(user, registerDto.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, "User"); // Add user to role
                    if (roleResult.Succeeded)
                    {
                        return StatusCode(201, new { Message = "User created successfully" });
                    }
                    else
                        return StatusCode(500, new{Error = "User created but role not assigned"});
                }
                else
                {
                    return StatusCode(500, new { Error = createdUser.Errors });
                    // return BadRequest(createdUser.Errors);

                }

            }
            catch (Exception e)
            {
                return StatusCode(500, new { Error = e.Message });
            }
        }

    }
}