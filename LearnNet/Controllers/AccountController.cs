using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using api.DTOs.Account;
using api.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<UserModel> _signInManager;
        public AccountController(UserManager<UserModel> userManager, ITokenService tokenService, SignInManager<UserModel> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login( [FromBody] LoginDto loginDto)
        {
            if(!ModelState.IsValid)
                return BadRequest("Invalid data");
            
            var user = await _userManager.Users.FirstOrDefaultAsync (x => x.UserName == loginDto.Username);

            if (user == null)
                return Unauthorized("Invalid username");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if(!result.Succeeded)
                return Unauthorized("Invalid password");

            return Ok(
                new{
                UserName = user.UserName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
                }
            );

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
                
                if (string.IsNullOrEmpty(registerDto.Username))
                {
                    return BadRequest("Username is required");
                }

                var existingUser = await _userManager.FindByNameAsync(registerDto.Username);
                if (existingUser != null)
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
                        return StatusCode(201, new { 
                                                        Message = "User created successfully",
                                                        User = new NewUserDto
                                                        {
                                                            UserName = user.UserName ?? string.Empty,
                                                            Email = user.Email,
                                                            Token = _tokenService.CreateToken(user)
                                                        }
                                                    }
                                                    );
                    
                    return StatusCode(500, new{Error = "User created but role not assigned"});
                }
                
                return StatusCode(500, new { Error = createdUser.Errors });

            }
            catch (Exception e)
            {
                return StatusCode(500, new { Error = e.Message });
            }
        }

    }
}