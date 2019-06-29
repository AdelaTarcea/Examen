using Examen.Models;
using Examen.Service;
using Examen.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examen.Controllers
{
    // [Authorize]

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// LOG IN USERS
        /// </summary>
        /// <param name="loggin"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]LogginPostModel loggin)
        {
            var user = _userService.Authenticate(loggin.Username, loggin.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }
        /// <summary>
        /// POST : CREATE A NEW USER
        /// </summary>
        /// <param name="registerModel">registerModel the user to create</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]RegisterPostModel registerModel)
        {

            registerModel.RegistrationDate = DateTime.Now;
        
            var user = _userService.Register(registerModel);

            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            return Ok(user);
        }
        /// <summary>
        /// GET ALL USERS
        /// </summary>
        /// <returns>a list of users</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
    }
}