using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetUser(int userid)
        {
            if (userid != 1)
            {
                return BadRequest("User not found");
            }

            return new UserDto()
            {
                Id = 1,
                Name = "Juan Camba"

            };
        }
    }
}
