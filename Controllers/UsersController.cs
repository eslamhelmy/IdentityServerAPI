using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserIdentity.Records;
using UserIdentity.Entities;
using UserIdentity.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace UserIdentity.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> Get()
        {
            var users = userManager.Users
                .ToList()
                .Select( (user) => {
                    var userDto= user.AsDto();
                    userDto.UserType = 
                                      userManager.IsInRoleAsync(user,"student").Result?
                                    "student":
                                      userManager.IsInRoleAsync(user,"supervisor").Result?
                                     "supervisor":
                                      "admin";//GetRoles(user.Id).FirstOrDefault();
                    return userDto;
                }).Where(x=> x.UserType == "student").ToList();

            return Ok(users);
        }


       
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetByIdAsync(Guid id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                return NotFound();
            }

            return user.AsDto();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                return NotFound();
            }

            user.IsApproved = !user.IsApproved;

            await userManager.UpdateAsync(user);

            return NoContent();
        }

        [HttpGet("isApproved")]
        public async Task<bool> IsApproved(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            return user.IsApproved;
        }
        

    }
}