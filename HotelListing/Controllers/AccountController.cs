using AutoMapper;
using HotelListing.Models;
using HotelListing.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApiUser> _userManager;
        //private readonly RoleManager<ApiUser> _roleManager;
        //private readonly SignInManager<ApiUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;

        public AccountController(UserManager<ApiUser> userManager, /*RoleManager<ApiUser> roleManager,*/ /*SignInManager<ApiUser> signInManager,*/ ILogger<AccountController> logger, IMapper mapper)
        {
            _userManager = userManager;
            //_roleManager = roleManager;
            //_signInManager = signInManager;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            _logger.LogInformation($"registration attempt for{userDTO.Email}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = _mapper.Map<ApiUser>(userDTO);
                user.UserName = userDTO.Email;
                var result = await _userManager.CreateAsync(user, userDTO.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }

                 await _userManager.AddToRolesAsync(user, userDTO.Roles);
                 return Accepted();
            } 
            
            catch (Exception ex)
            {
                _logger.LogError(ex, $"something went wrong in the{nameof(Register)}");
                return Problem($"something went wrong in the{nameof(Register)}", statusCode: 500);
                
            }

        }

        //[HttpPost]
        //[Route("Login")]
        //public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        //{
        //    _logger.LogInformation($"login attement for {loginDTO.Email}");
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    try
        //    {
        //        var result = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, false, false);
        //        if (!result.Succeeded)
        //        {
        //            return Unauthorized(loginDTO);
        //        }

        //        return Accepted();
        //    }
        //    catch (Exception ex)
        //    {

        //        _logger.LogError(ex, $"something went wrong{nameof(Login)}");
        //        return StatusCode(500, "Login failed, Please try again");
        //    }
        //}
    }
}
