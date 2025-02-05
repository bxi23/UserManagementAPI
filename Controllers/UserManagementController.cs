using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using UserManagementAPI_TechHiveSolutionsLab.Exceptions;

namespace UserManagementAPI_TechHiveSolutionsLab.Controllers
{
    [ApiController]
    [Route("api")]
    public class UserManagementController : ControllerBase
    {
        private static List<User> Users = new List<User>
        {
            new User { Id = 1, Name = "John Doe", Email = "john.doe@example.com" },
            new User { Id = 2, Name = "Jane Smith", Email = "jane.smith@example.com" }
        };

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return Ok(Users);
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                throw new UserNotFoundException(id);
            }
            return Ok(user);
        }

        [HttpPost]
        public ActionResult<User> AddUser(User newUser)
        {
            if (newUser == null || string.IsNullOrWhiteSpace(newUser.Name) || !IsValidEmail(newUser.Email))
            {
                return BadRequest("Invalid user data. Name cannot be empty and email must be valid.");
            }

            newUser.Id = Users.Max(u => u.Id) + 1;
            Users.Add(newUser);
            return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
        }

        private bool IsValidEmail(string email)
        {
            var emailAttribute = new EmailAddressAttribute();
            return emailAttribute.IsValid(email);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateUser(int id, User updatedUser)
        {
            var user = Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                throw new UserNotFoundException(id);
            }

            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;

            return Ok($"User with ID {id} updated successfully.");
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            var user = Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                throw new UserNotFoundException(id);
            }

            Users.Remove(user);
            return Ok($"User with ID {id} deleted successfully.");
        }
    }

    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
    }
}