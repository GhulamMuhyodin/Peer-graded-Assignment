using Microsoft.AspNetCore.Mvc;
using Net_Assignment.Models;
using Net_Assignment.Helpers;

namespace Net_Assignment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private static List<User> users = new List<User>();

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return Ok(users);
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public ActionResult<User> CreateUser(User user)
        {
            if (ValidationHelper.IsNullOrWhiteSpace(user.Name))
            {
                ModelState.AddModelError("Name", "Name is required.");
            }
            if (ValidationHelper.IsNullOrWhiteSpace(user.Email) || !ValidationHelper.IsValidEmail(user.Email))
            {
                ModelState.AddModelError("Email", "A valid email is required.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            user.Id = users.Count > 0 ? users.Max(u => u.Id) + 1 : 1;
            users.Add(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, User updatedUser)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();
            if (ValidationHelper.IsNullOrWhiteSpace(updatedUser.Name))
            {
                ModelState.AddModelError("Name", "Name is required.");
            }
            if (ValidationHelper.IsNullOrWhiteSpace(updatedUser.Email) || !ValidationHelper.IsValidEmail(updatedUser.Email))
            {
                ModelState.AddModelError("Email", "A valid email is required.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;
            return Ok(new { message = "User updated successfully" });
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();
            users.Remove(user);
            return Ok(new { message = $"User with id {id} is deleted" });
        }
    }
}