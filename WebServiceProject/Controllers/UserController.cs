using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebServiceProject.Data;
using WebServiceProject.Interfaces;
using WebServiceProject.Models;
using WebServiceProject.Services;
//using Serialization;


namespace WebServiceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly DataContext _context;
        private readonly TokenService _tokenService;

        public UserController(IUserRepository userRepository, DataContext context, TokenService tokenService)
        {
            _userRepository = userRepository;
            _context = context;
            _tokenService = tokenService;
        }

        // Méthode pour l'authentification et la génération du token JWT
        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public IActionResult Login([FromBody] WebServiceProject.Models.LoginRequest loginRequest)
        {
            // Remplace cette vérification par une validation réelle de l'utilisateur
            var user = _userRepository.GetUsers()
                .FirstOrDefault(u => u.Pseudo == loginRequest.UserName && u.Password == loginRequest.Password);

            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            var token = _tokenService.GenerateToken(user);
            return Ok(new { Token = token });
        }

        // GET: api/User - Accès limité aux utilisateurs authentifiés
        [Authorize]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsers()
        {
            var users = _userRepository.GetUsers();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(users);
        }

        // GET: api/User/{id} - Accès limité aux utilisateurs authentifiés
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(404)]
        public IActionResult GetUserById(int id)
        {
            var user = _userRepository.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(user);
        }

        // POST: api/User - Accès limité aux utilisateurs authentifiés
        [Authorize]
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User data is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _userRepository.CreateUser(user);

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        // PUT: api/User/{id} - Accès limité aux utilisateurs authentifiés
        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            if (updatedUser == null || id != updatedUser.Id)
            {
                return BadRequest("User data is invalid.");
            }

            var existingUser = _userRepository.GetUserById(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            _userRepository.UpdateUser(updatedUser);

            return NoContent();
        }

        // DELETE: api/User/{id} - Accès limité aux utilisateurs authentifiés
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUser(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            _userRepository.DeleteUser(user);

            return NoContent();
        }
    }
}
